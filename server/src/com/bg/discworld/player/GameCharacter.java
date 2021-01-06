package com.bg.discworld.player;

import org.javacord.api.entity.channel.TextChannel;
import org.javacord.api.event.message.MessageCreateEvent;
import com.bg.discmud.core.MUD;
import com.bg.discmud.database.MySQL;
import com.bg.discworld.utility.Util;

public class GameCharacter {

	public static boolean processNewCharacterMessage(Player player, MessageCreateEvent event, TextChannel channel) {
		switch (event.getMessageContent().toLowerCase()) {
		case "!create":
			if (player.createState == 0) {
				// already have a character, use !
				player.send(MUD.messages.get("NEED_TO_DELETE_CHARACTER"));
			} else if (player.createState == 1) {
				player.send(MUD.messages.get("NEW_CHARACTER_CHOOSE_RACE"));
				player.send(MUD.messages.get("NEW_CHARACTER_MAY_CANCEL"));
			} else {
				player.send(MUD.messages.get("ALREADY_CREATING_CHARACTER"));
			}
			break;
		case "!delete":
			deleteCharacter(player, event, channel);
			break;
		case "!cancel":
			cancelCharacter(player, event, channel);
			break;
		default:
			if (player.active && !player.playing && player.createState > 0) {
				processNewCharacter(player, event, channel);
			} else {
				return false;
			}
			break;
		}

		return true;

	}

	static void cancelCharacter(Player player, MessageCreateEvent event, TextChannel channel) {
		if (player.createState > 0) {
			if (player.cancelConfirmation) {
				player.cancelConfirmation = false;
				player.createState = 1;
				player.send(MUD.messages.get("NEW_CHARACTER_CHOOSE_RACE"));
			} else {
				player.cancelConfirmation = true;
				player.cancelConfirmationStamp = MUD.tick + 10000;
				player.send(MUD.messages.get("NEW_CHARACTER_CONFIRM_CANCEL"));
			}
		} else {
			player.send(MUD.messages.get("NEW_CHARACTER_CANT_CANCEL"));
		}
	}

	static void deleteCharacter(Player player, MessageCreateEvent event, TextChannel channel) {
		if (!player.canPart()) {
			return;
		}
		if (player.createState == 0) {
			if (player.deleteConfirmation) {
				String statement = "DELETE FROM player WHERE id=" + player.id;
				MUD.players.remove(player.id);
				player.part(MUD.messages.get("CHARACTER_DELETED"), MUD.messages.get("ANNOUNCE_DELETE_CHARACTER").replace("{{name}}", player.getName()));
				MySQL.save(statement, null);
				player.deleteConfirmation = false;
			} else {
				player.deleteConfirmation = true;
				player.deleteConfirmationStamp = MUD.tick + 10000;
				player.send(MUD.messages.get("CHARACTER_CONFIRM_DELETE"));
			}
		} else {
			player.send(MUD.messages.get("NO_CHARACTER"));
		}
	}

	static void processNewCharacter(Player player, MessageCreateEvent event, TextChannel channel) {
		switch (player.createState) {
		case 0:
			player.send("Uh oh? Error code: what the hell");
			break;
		case 1:
			switch (event.getMessageContent().toLowerCase()) {
			case "orc":
			case "dwarf":
			case "human":
			case "elf":
			case "saurian":
			case "noraxite":
			case "nord":
				player.send(MUD.messages.get("NEW_CHARACTER_CONFIRM_RACE").replace("{{race}}",
						getRaceString(event.getMessageContent())));
				player.send(MUD.messages.get("NEW_CHARACTER_CHOOSE_CLASS"));
				player.fields.put("race", event.getMessageContent().toLowerCase());
				player.createState = 2;
				break;
			default:
				player.send(MUD.messages.get("NEW_CHARACTER_CHOOSE_RACE"));
				break;
			}
			break;
		case 2:
			switch (event.getMessageContent().toLowerCase()) {
			case "barbarian":
			case "light mage":
			case "dark mage":
			case "cleric":
			case "knight":
			case "thief":
			case "monk":
			case "rogue":
				player.send(MUD.messages.get("NEW_CHARACTER_CONFIRM_CLASS").replace("{{class}}",
						getClassString(event.getMessageContent())));
				player.send(MUD.messages.get("NEW_CHARACTER_STATS"));
				player.fields.put("class", event.getMessageContent().toLowerCase());
				player.createState = 3;
				rollStats(player, event, channel);
				break;
			default:
				player.send(MUD.messages.get("NEW_CHARACTER_CHOOSE_CLASS"));
				break;
			}
			break;
		case 3:
			switch (event.getMessageContent().toLowerCase()) {
			case "reroll":
				rollStats(player, event, channel);
				break;
			case "accept":
				player.send(MUD.messages.get("NEW_CHARACTER_STATS_ACCEPT"));
				player.createState = 4;
				player.send(MUD.messages.get("NEW_CHARACTER_CHOOSE_SEX"));
				break;
			default:
				player.send(MUD.messages.get("NEW_CHARACTER_CHOOSE_STATS"));
				break;
			}
			break;
		case 4:
			switch (event.getMessageContent().toLowerCase()) {
			case "asexual":
			case "male":
			case "female":
				player.fields.put("sex", event.getMessageContent().toLowerCase());
				player.send(MUD.messages.get("NEW_CHARACTER_CONFIRM_SEX").replace("{{sex}}",
						getSexString(event.getMessageContent())));
				completeNewCharacter(player, channel);
				break;
			default:
				player.send(MUD.messages.get("NEW_CHARACTER_CHOOSE_STATS"));
				break;
			}
			break;
		}
	}

	static int getMaxHP(Player player) {
		return 100;
	}

	static void completeNewCharacter(Player player, TextChannel channel) {
		player.createState = 0;
		player.fields.put("room", 1);
		player.fields.put("max_hp", getMaxHP(player));
		player.fields.put("hp", getMaxHP(player));
		player.fields.put("level", 1);
		player.fields.put("front_row", false);
		player.fields.put("last_played", MUD.tick);
		player.fields.put("alert", false);
		player.fields.put("melee_damage_type", "bash");
		player.fields.put("melee_damage_dice", 2);
		player.fields.put("melee_damage_sides", 6);
		player.fields.put("melee_damage_modifier", 4);
		player.fields.put("initiative_bonus", 2);
		player.fields.put("speed", 5);
		player.fields.put("admin", 0);
		player.send(MUD.messages.get("NEW_CHARACTER_COMPLETE"));
		player.newCharacter();
		player.save();
		player.join();
		player.look(false);
	}

	static void rollStats(Player player, MessageCreateEvent event, TextChannel channel) {
		int[] baseStats = { 10, 10, 10, 10, 10 };
		int[] rollStats = new int[5];
		int total = 0, i;
		while (total != 20) {
			total = 0;
			for (i = 0; i < 5; i++) {
				rollStats[i] = Util.randInt(3, 10);
				total += rollStats[i];
			}
		}
		for (i = 0; i < 5; i++) {
			baseStats[i] += rollStats[i];
		}
		player.fields.put("strength", baseStats[0]);
		player.fields.put("dexterity", baseStats[1]);
		player.fields.put("intelligence", baseStats[2]);
		player.fields.put("wisdom", baseStats[3]);
		player.fields.put("constitution", baseStats[4]);
		player.send(MUD.messages.get("NEW_CHARACTER_ROLL").replace("{{str}}", baseStats[0] + "")
				.replace("{{dex}}", baseStats[1] + "").replace("{{int}}", baseStats[2] + "")
				.replace("{{wis}}", baseStats[3] + "").replace("{{con}}", baseStats[4] + ""));
		player.send(MUD.messages.get("NEW_CHARACTER_CHOOSE_STATS"));
	}

	static String getRaceString(String anycase) {
		switch (anycase.toLowerCase()) {
		case "orc":
			return "Orc";
		case "dwarf":
			return "Dwarf";
		case "human":
			return "Human";
		case "elf":
			return "Elf";
		case "saurian":
			return "Saurian";
		case "noraxite":
			return "Noraxite";
		case "nord":
			return "Nord";
		}
		return "Bear";
	}

	static String getClassString(String anycase) {
		switch (anycase.toLowerCase()) {
		case "barbarian":
			return "Barbarian";
		case "light mage":
			return "Light Mage";
		case "dark mage":
			return "Dark Mage";
		case "cleric":
			return "Cleric";
		case "knight":
			return "Knight";
		case "thief":
			return "Thief";
		case "monk":
			return "Monk";
		case "rogue":
			return "Rogue";
		}
		return "Bear";
	}

	static String getSexString(String anycase) {
		switch (anycase.toLowerCase()) {
		case "asexual":
			return "Asexual";
		case "male":
			return "Male";
		case "female":
			return "Female";
		}
		return "Bear";
	}

	public static Player createPlayer(QueryPlayerResult psr) {
		Player player = new Player(MUD.mud, psr.id);
		player.fields.put("name", psr.event.getMessageAuthor().getDisplayName());
		player.channel = psr.event.getChannel();
		MUD.players.put(psr.id, player);
		player.active = true;
		return player;
	}

	public static void processQueryPlayerResults() {
		QueryPlayerResult psr;
		Player player;
		while (!MUD.queryPlayerResults.isEmpty()) {
			psr = MUD.queryPlayerResults.poll();
			if (psr.player == null) { // no character found!
				if (psr.event.getMessageContent().equals("!create")) { // but thats ok because they asked to create one
					player = createPlayer(psr);
					player.createState = 1;
					MUD.incoming.add(psr.event);
				} else {
					psr.event.getChannel().sendMessage(MUD.messages.get("NO_CHARACTER"));
				}
			} else { // found character
				MUD.players.put(psr.id, psr.player);
				psr.player.fields.put("name", psr.event.getMessageAuthor().getDisplayName());
				psr.player.channel = psr.event.getChannel();
				MUD.incoming.add(psr.event);
				psr.player.join();
			}
		}
	}

}
