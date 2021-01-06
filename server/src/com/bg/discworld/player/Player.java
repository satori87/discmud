package com.bg.discworld.player;

import java.util.LinkedList;
import org.javacord.api.entity.channel.TextChannel;
import org.javacord.api.entity.message.embed.EmbedBuilder;

import com.bg.discmud.core.CommandData;
import com.bg.discmud.core.MUD;
import com.bg.discmud.database.MySQL;
import com.bg.discmud.database.Model;
import com.bg.discmud.item.Item;
import com.bg.discworld.battle.Battle;
import com.bg.discworld.mobile.Mobile;
import com.bg.discworld.mobile.Monster;
import com.bg.discworld.utility.Log;
import com.bg.discworld.utility.TextParser;
import com.bg.discworld.utility.Util;
import com.bg.discworld.world.Area;
import com.bg.discworld.world.Room;

public class Player extends Mobile {

	// list of fields blocked from being used by players in notation
	public final static String blockedFieldStr = "id,room,state,banned,classes,equipment,inventory,languages,last_played";

	public boolean playing = false;

	// Idle management
	public long turnOverAt = 0; // the time in ms that turn will end
	public int missedTurns = 0;
	public int timesWarned = 0;
	public long lastMessage;
	public long partStamp;
	public boolean parting;

	// Message
	public TextChannel channel;
	private String message = "";

	// Character management including logging

	public int createState = 0;
	public boolean deleteConfirmation = false;
	public boolean cancelConfirmation = false;
	public long cancelConfirmationStamp;
	public long deleteConfirmationStamp;

	public Player(MUD mud, long id) {
		super(mud, id);
		try {
			this.mud = mud;
			world = mud.world;
			isPlayer = true;
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void send(String in) {
		message += "\n" + in;
	}

	public void flush() {
		// every send gets routed to THIS method. this is where we do mirror
		if (!active) {
			return;
		}
		try {
			if (message.length() > 0) {
				String[] split = Util.splitForDiscord(message);
				for (String s : split) {
					channel.sendMessage(s);
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		message = "";
	}

	public void join() {
		try {
			playing = true;
			active = true;
			String join = MUD.messages.get("ANOTHER_USER_JOIN").replace("{{name}}", (String) fields.get("name"));
			mud.sendChannel("logging", join);
			// send(MUD.messages.get("USER_JOIN"));
			if (!getRoom().active)
				setRoom(1);
			getRoom().join(this, -1);
			Log.info(getFullID() + " join");
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void checkInactive() {
		if (active) {
			if (playing) {
				if (parting) {
					if (MUD.tick > partStamp) {
						part(MUD.messages.get("PARTING"), MUD.messages.get("ANOTHER_USER_PART").replace("{{name}}", getName()));
					}
				} else if (MUD.tick - lastMessage > MUD.PLAYING_IDLE_TIME * 1000) {
					if (canPart()) {
						part(MUD.messages.get("IDLE_MESSAGE"), MUD.messages.get("ANNOUNCE_PART_INACTIVE").replace("{{name}}", getName()));
					} else {
						Log.debug("battle thing");
					}

				}
			} else {
				if (MUD.tick - lastMessage > MUD.ACTIVE_IDLE_TIME * 1000) {
					Log.debug("removing" + id);
					MUD.players.remove(id);
				}
			}
		} else {
			MUD.players.remove(id);
		}
	}

	boolean tryPart() {
		if (canPart()) {
			send(MUD.messages.get("QUIT_TIMER_BEGIN"));
			parting = true;
			partStamp = MUD.tick + MUD.PART_TIME * 1000;
			return true;
		} else {
			send(MUD.messages.get("QUIT_CANT"));
			return false;
		}
	}

	boolean canPart() {
		return battle == null;
	}

	void part(String playerPartMessage, String announcePartMessage) {
		try {
			Log.debug("parting " + id);
			getRoom().part(this, -1);
			save();
			playing = false;
			parting = false;
			mud.sendChannel("logging", announcePartMessage);
			send(playerPartMessage);
			flush();
			Log.info(getFullID() + " part");
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void newCharacter() {
		try {
			String statement = "INSERT INTO player (id) VALUES (" + id + ")";
			MySQL.save(statement, null);

		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void save() {
		try {
			fields.put("id", id);
			Log.info("Saving player " + (String) fields.get("name"));
			fields.put("last_played", System.currentTimeMillis());
			String statement = "UPDATE player SET ";
			LinkedList<Object> obj = new LinkedList<Object>();
			String[] split = Model.playerModel.split(",");
			int count = split.length;
			for (int i = 0; i < count; i++) {
				if (i == count - 1) {
					statement += split[i] + "=?";
				} else {
					statement += split[i] + "=?,";
				}
				obj.add(fields.get(split[i]));
			}
			obj.add(id);
			statement += " WHERE id=?";
			MySQL.save(statement, obj);
			Log.info(getFullID() + " saved");
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public boolean hasAccess() {
		return ((int)fields.get("admin") > 0);
	}

	public void idleMsg() {
		try {
			String s = MUD.messages.get("IDLE_MESSAGE");
			send(s);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void moveTo(int nextRoom, int dir) {
		try {
			String exit = MUD.messages.get("USER_EXIT");
			send(exit.replace("{{travel:direction}}", Room.getStdExitName(dir)).replace("{{travel:mode}}", getTravelVerb(dir)));
		} catch (Exception e) {
			e.printStackTrace();
		}
		super.moveTo(nextRoom, dir);
	}

	@Override
	public boolean active() {
		return active && playing;
	}

	public void exit(int i) {
		Log.info(getFullID() + " exiting room " + getRoom() + " by exit " + Room.getStdExitName(i));
		try {
			int nextRoom = getRoom().exit[i];
			if (nextRoom > 0) {
				if (world.room[nextRoom].active) {
					moveTo(nextRoom, i);
				} else {
					send(MUD.messages.get("BAD_EXIT"));
				}
			} else {
				send(MUD.messages.get("BAD_EXIT"));
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public boolean canSee(Mobile m) {
		return super.canSee(m);
	}

	public int getID() {
		return (int) fields.get("id");
	}

	public String[] getCommands(String cmd) {
		try {
			if (cmd != null) {
				// remove leading & trailing whitespace

				String[] cmds = cmd.split(" ");
				if (cmds.length > 0) {
					return cmds;
				}
			}
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;
	}

	public void parse(String cmd) {
		Log.debug(getFullID() + ": " + cmd);
		cmd = TextParser.replaceNotation(cmd, false, this);
		try {
			String[] cmds = getCommands(cmd);
			String rest = "";
			int f = cmd.indexOf(" ");
			if (f > 0) {
				rest = cmd.substring(f);
				if (rest.length() > 1) {
					rest = rest.substring(1);
				}
			}
			rest = rest.trim();
			if (getRoom() != null && getRoom().command(this, cmd)) {
				if (battle == null || battle.command(this, cmd)) {
					switch (cmds[0].toUpperCase()) {
					case "!QUIT":
						tryPart();
						break;
					case "ATTACK":
					case "ATTAC":
					case "ATTA":
					case "ATT":
					case "AT":
					case "A":
						attack(rest);
						break;
					case "LOOK":
					case "LOO":
					case "LO":
					case "L":
						look(true);
						break;
					case "NORTH":
					case "NORT":
					case "NOR":
					case "NO":
					case "N":
						exit(0);
						break;
					case "EAST":
					case "EAS":
					case "EA":
					case "E":
						exit(1);
						break;
					case "SOUTH":
					case "SOUT":
					case "SOU":
					case "SO":
					case "S":
						exit(2);
						break;
					case "WEST":
					case "WES":
					case "WE":
					case "W":
						exit(3);
						break;
					case "UP":
					case "U":
						exit(4);
						break;
					case "DOWN":
					case "DOW":
					case "DO":
					case "D":
						exit(5);
						break;
					case "INVENTORY":
					case "INVENTOR":
					case "INVENTO":
					case "INVENT":
					case "INVEN":
					case "INVE":
					case "INV":
						inv();
						break;
					case "SAY":
						getRoom().say(this, rest);
						break;
					case "SCORE":
					case "SCOR":
					case "SCO":
					case "SC":
						score();
						break;
					case "ADMIN":
						admin(rest);
						break;
					case "HELP":
						help(rest);
						break;
					case "TEST":
						EmbedBuilder eb = new EmbedBuilder();
						for(int i = 1; i < 20; i++) {							
							eb.addInlineField("Weapon", "item name " + i);
							eb.addInlineField("Weapon", "item name " + i);
							eb.addInlineField("Weapon", "item name " + i);
							eb.addInlineField("Weapon", "item name " + i);
							eb.setImage("https://www.google.com/url?sa=i&url=https%3A%2F%2Fdisolncomment.blogspot.com%2F2013%2F06%2Fprocessing-multibyte-characters-like.html&psig=AOvVaw3_-_9XDU4GBVNempDi74v7&ust=1609806023999000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCOCH3vOAge4CFQAAAAAdAAAAABAO");
							eb.setDescription("whahuf feijowg4 efet24opj eoe t4t4 poeijewf iop4tr fijwfiweof jiw4ofweew fewjiofe oiewf ewiojfewfewfoij ewfijoewf iojefi ojewfewiojfef. f ewoifjewiojfjoiwefoi ewiojfewoi feiojf iowjefiojwef wef jioewiojf ew iojfewfioj iojwef ijoefi ewoif ef");
							eb.setTitle("REAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALLY LOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOONG YTIIIIIIYLE");
						}
						channel.sendMessage(eb);
						break;
					default:
						send(MUD.messages.get("COMMAND_NOT_RECOGNIZED"));
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	void help(String rest) {
		/*
		 * try { rest = rest.toUpperCase(); // TODO: Refactor into a
		 * "parse command arguments function" String[] word = rest.split(" "); String st
		 * = "SELECT id,name,plaintext FROM frontend_helpfile WHERE name LIKE '" + word
		 * + "%' LIMIT 1"; // ResultSet rs = DBMan.querySQL(st); while (rs.next()) {
		 * sendln("##########" + rs.getString(1) + " [" + rs.getString(0) +
		 * "] ##########"); // TODO: Refactor into // MUD_MESSAGES if // desired
		 * sendln(rs.getString(2)); // TODO: Send "related helpfiles" } } catch
		 * (Exception e) { // TODO: Special catch "Result not found" error and try
		 * search again with SELECT // ... FROM ... WHERE id= to allow search by
		 * primary_key e.printStackTrace(); } // return null;
		 */
	}

	void admin(String rest) {
		Player p = null;
		try {
			if (!hasAccess()) {
				return;
			}
			rest = rest.toUpperCase();
			String[] word = rest.split(" ");
			switch (word[0]) {
			case "SEXY":
				Mobile[] pro = new Mobile[4];
				Mobile[] con = new Mobile[4];
				pro[0] = this;
				pro[1] = world.spawnMonster((int) fields.get("room"), 1);
				pro[2] = world.spawnMonster((int) fields.get("room"), 1);
				pro[3] = world.spawnMonster((int) fields.get("room"), 1);
				con[0] = world.spawnMonster((int) fields.get("room"), 1);
				con[1] = world.spawnMonster((int) fields.get("room"), 1);
				con[2] = world.spawnMonster((int) fields.get("room"), 1);
				con[3] = world.spawnMonster((int) fields.get("room"), 1);

				battle = new Battle(mud, getRoom(), pro, con);
				getRoom().battles.add(battle);
				for (int a = 0; a < 4; a++) {
					pro[a].battle = battle;
					con[a].battle = battle;
				}
				// m.battle = battle;
				battle.start();
				break;
			case "SPAWNMOB":
				id = Integer.parseInt(word[1]);
				long uid = world.spawnMonster((int) fields.get("room"), id).id;
				if (uid >= 0) {
					send("Spawned monster " + uid);
				} else {
					send("Failed");
				}
				break;
			case "WARP":
				moveTo(Integer.parseInt(word[1]), -2);
				break;
			case "WARPTO":
				p = mud.findPlayer(word[1]);
				if (p != null) {
					moveTo(p.getRoom().id, -2);
				}
				break;
			case "FETCH":
				mud.fetch();

				look(false);
				break;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	void attack(String rest) {
		// String name = "";
		if (battle == null) {
			Mobile m = getRoom().findMob(rest);
			if (m != null) {
				if (m.battle == null) {
					// LETS START A BATTLE
					Room r = getRoom();

					Mobile[] con = new Mobile[] { m };
					con = r.getMonsterArray();
					battle = new Battle(mud, r, new Mobile[] { this }, con);
					r.battles.add(battle);
					m.battle = battle;
					battle.start();
					send("YOO");
				} else {
					// send a message they are already in battle until we add joining in mechanics
					send("YUCK");
				}
			}
		}
	}

	void inv() {
		try {
			String header = MUD.commands.get("inventory_header").format;
			String line = MUD.commands.get("inventory_line").format;
			String footer = MUD.commands.get("inventory_footer").format;
			send(header);
			String s = "";
			for (Item i : inventory) {
				s = TextParser.replaceNotation(line, false, i);
				send(s);
			}
			send(footer);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	void score() {
		try {
			CommandData c = MUD.commands.get("score");
			if (c != null) {
				// String s = checkPNotation(c.format, 6);
				// s = s.replaceAll("\\*", "{{R}}*" + ColorMan.getDefault());
				// sendln(s);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void look(boolean notify) {
		try {
			Room r = getRoom();
			Area a = world.area[r.area];

			if (r.header.length() > 0) {
				send("\n" + r.header);
			}

			String roomname = MUD.messages.get("ROOM_NAME");
			roomname = roomname.replace("{{name}}", r.name);
			roomname = roomname.replace("{{area}}", a.name);
			send(roomname);

			String desc = MUD.messages.get("ROOM_DESC");
			desc = desc.replace("{{desc}}", r.desc);
			send(desc);

			String exits = MUD.messages.get("ROOM_EXITS");
			exits = exits.replace("{{exit_north}}", r.getExitString(0));
			exits = exits.replace("{{exit_east}}", r.getExitString(1));
			exits = exits.replace("{{exit_south}}", r.getExitString(2));
			exits = exits.replace("{{exit_west}}", r.getExitString(3));
			exits = exits.replace("{{exit_up}}", r.getExitString(4));
			exits = exits.replace("{{exit_down}}", r.getExitString(5));
			send(exits);
			if (r.mobs.size() > 0) { // || r.items.size() > 0 || r.mapobjects.size() > 0
				send(" ");
			}
			for (Player p : r.players) {
				if (p.active() && p != this && canSee(p)) {
					send(p.getIdlePhrase().replace("{{name}}", (String) p.fields.get("name")));
				}
			}
			for (Monster m : r.monsters) {
				if (m.active() && canSee(m)) {
					send(m.getIdlePhrase().replace("{{name}}", (String) m.fields.get("name")));
				}
			}

			// send info on all items here
			// send info on all mapobjects here

			if (notify) {
				r.sendAllCanSeeBut(this,
						MUD.messages.get("USER_LOOK").replace("{{name}}", (String) fields.get("name")));
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}