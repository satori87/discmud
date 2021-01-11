package com.bg.discmud.core;

import java.sql.ResultSet;
import java.util.HashMap;
import java.util.concurrent.ConcurrentLinkedQueue;
import org.javacord.api.DiscordApi;
import org.javacord.api.DiscordApiBuilder;
import org.javacord.api.entity.channel.TextChannel;
import org.javacord.api.event.message.MessageCreateEvent;
import com.bg.discmud.database.MySQL;
import com.bg.discmud.database.Model;
import com.bg.discmud.item.ItemSheet;
import com.bg.discworld.mobile.MonsterSheet;
import com.bg.discworld.player.GameCharacter;
import com.bg.discworld.player.Player;
import com.bg.discworld.player.QueryPlayerResult;
import com.bg.discworld.player.QueryPlayerRunnable;
import com.bg.discworld.utility.Log;
import com.bg.discworld.utility.TextParser;
import com.bg.discworld.utility.Util;
import com.bg.discworld.world.Area;
import com.bg.discworld.world.World;

public class MUD {

	public static MUD mud;

	public static final String token = "NzkzNjEzNjU2MjY0MTQ2OTc1.X-u0UA.vn9Ba-ZmiJKYKwyMjIhLBXpvri0";

	public static final int MONSTER_ACTION_DELAY = 500;
	public static final int PLAYER_TURN_TIME = 400;
	public static final int PLAYER_TURN_TIME_RUSHED = 3000;

	public static final long ACTIVE_IDLE_TIME = 3600;
	public static final long PLAYING_IDLE_TIME = 600;
	public static final long PART_TIME = 10;

	public static final long LOGGING_CHANNEL = 795377451895226371L;

	public static HashMap<String, String> messages = new HashMap<String, String>();
	public static HashMap<String, CommandData> commands = new HashMap<String, CommandData>();

	public World world;
	public static HashMap<Long, Player> players = new HashMap<Long, Player>();
	public static HashMap<String, Object> generalScriptFields = new HashMap<String, Object>();

	boolean shutdown = false;

	public static ConcurrentLinkedQueue<MessageCreateEvent> incoming = new ConcurrentLinkedQueue<MessageCreateEvent>();
	public static DiscordApi api;

	public static ConcurrentLinkedQueue<QueryPlayerResult> queryPlayerResults = new ConcurrentLinkedQueue<QueryPlayerResult>();

	public static long tick = System.currentTimeMillis();

	void addGeneralScriptFields() {
		generalScriptFields.put("mud", this);
		generalScriptFields.put("world", world);
	}

	public MUD() {
		mud = this;
	}

	public void start() {
		MySQL.connectSQL();
		setupWorld();
		setupBot();
		addGeneralScriptFields();
		mainLoop();
	}

	void setupWorld() {
		world = new World(this);
		fetch();
		Log.info("World loaded.");
	}

	void setupBot() {
		api = new DiscordApiBuilder().setToken(token).login().join();

		api.addMessageCreateListener(event -> {
			if (event.isPrivateMessage() && !event.getMessageAuthor().isBotUser()) {
				incoming.add(event);
			}
		});
		Log.info("Connected to Discord");
	}

	void processIncomingMessages() {
		long id = 0;
		Player player;
		MessageCreateEvent event;
		TextChannel channel;
		while (!incoming.isEmpty()) {
			event = incoming.poll();
			channel = event.getChannel();
			id = event.getMessageAuthor().getId();
			player = players.get(id);
			if (player == null) { // look up player from mySQL
				new Thread(new QueryPlayerRunnable(event)).start();
			} else {
				player.lastMessage = tick;
				if (player.parting) {
					player.parting = false;
					player.send(messages.get("PART_CANCELLED"));
				}
				if (!GameCharacter.processNewCharacterMessage(player, event, channel)) {
					player.parse(event.getMessageContent());
				}
			}
		}
	}

	void backup() {
		for (Player p : players.values()) {
			if (p.active()) {
				p.save();
			}
		}
	}

	void flushMessages() {
		for (Player p : players.values()) {
			if (p.active) {
				if (p.cancelConfirmation) {
					if (tick > p.cancelConfirmationStamp) {
						p.cancelConfirmation = false;
					}
				}
				if (p.deleteConfirmation) {
					if (tick > p.deleteConfirmationStamp) {
						p.deleteConfirmation = false;
					}
				}
				p.flush();
			}
		}
	}

	void checkInactivePlayers() {
		for (Player p : players.values()) {
			p.checkInactive();
		}
	}

	void secondTimer() {

		long secondTimer = 0;
		int backupTicks = 0;
		int fetchTicks = 0;

		if (tick >= secondTimer) {
			secondTimer = tick + 1000;

			checkInactivePlayers();

			backupTicks++;
			if (backupTicks >= 300) {
				backupTicks = 0;
				backup();

			}

			if (fetchTicks >= 86400) {
				fetchTicks = 0;
				fetch();
			}

		}

	}

	void mainLoop() {

		try {

			while (!shutdown) {
				tick = System.currentTimeMillis();

				GameCharacter.processQueryPlayerResults();
				processIncomingMessages();

				secondTimer();

				world.update(tick);

				flushMessages();

				Thread.sleep(20);
			}

		} catch (Exception e) {
			e.printStackTrace();
		}

	}

	public void fetch() {
		Log.info("Retrieving non-world data from SQL");
		try {

			ResultSet rs;
			int i = 0;

			Model.clear();

			rs = MySQL.querySQL("SHOW COLUMNS FROM player");
			while (rs.next()) {
				Model.playerModel += (String) rs.getObject(1);
				if (!rs.isLast()) {
					Model.playerModel += ",";
				}
			}

			rs = MySQL.querySQL("SHOW COLUMNS FROM monster");
			while (rs.next()) {
				Model.monsterModel += (String) rs.getObject(1);
				if (!rs.isLast()) {
					Model.monsterModel += ",";
				}
			}

			rs = MySQL.querySQL("SHOW COLUMNS FROM item");
			while (rs.next()) {
				Model.itemModel += (String) rs.getObject(1);
				if (!rs.isLast()) {
					Model.itemModel += ",";
				}
			}

			rs = MySQL.querySQL("SELECT name,plaintext FROM frontend_message");
			while (rs.next()) {
				messages.put(rs.getString(1), rs.getString(2));
			}

			rs = MySQL.querySQL("SELECT name,format,help_description FROM frontend_command");
			while (rs.next()) {
				commands.put(rs.getString(1), new CommandData(rs.getString(1), rs.getString(2), rs.getString(3)));
			}

			rs = MySQL.querySQL("SELECT name,json FROM area");
			while (rs.next()) {
				world.area.put(rs.getString(1), (Area) Util.fromJSON(rs.getString(2), Area.class));
			}

			rs = MySQL.querySQL("SELECT " + Model.monsterModel + " FROM monster");
			while (rs.next()) {
				MonsterSheet m = new MonsterSheet();
				int c = 1;
				String[] split = Model.monsterModel.split(",");
				for (String s : split) {
					m.fields.put(s, rs.getObject(c));
					c++;
				}
				world.monsterSheets.put((int) m.fields.get("id"), m);
			}

			rs = MySQL.querySQL("SELECT " + Model.itemModel + " FROM item");
			while (rs.next()) {
				ItemSheet is = new ItemSheet();
				int c = 1;
				String[] split = Model.itemModel.split(",");
				for (String s : split) {
					is.fields.put(s, rs.getObject(c));
					c++;
				}
				world.itemSheets.put((int) is.fields.get("id"), is);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void sendChannel(String name, String msg) {
		api.getTextChannelsByNameIgnoreCase(name).stream().findFirst().get().sendMessage(msg);
	}

	public Player findPlayer(String name) {
		try {
			for (Player p : players.values()) {
				if (((String) p.fields.get("name")).equalsIgnoreCase(name) && p.active()) {
					return p;
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

}
