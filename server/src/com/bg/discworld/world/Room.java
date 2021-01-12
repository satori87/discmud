package com.bg.discworld.world;

import java.util.LinkedList;

import com.bg.discmud.core.MUD;
import com.bg.discworld.battle.Battle;
import com.bg.discworld.mobile.Mobile;
import com.bg.discworld.mobile.Monster;
import com.bg.discworld.player.Player;
import com.bg.discworld.utility.Log;

public class Room {

	// public boolean active = false;
	MUD mud;
	World world;

	public int id = 0;
	public String name = "Room";
	public String desc = "Description";

	public String displayName;
	public int[] exit = new int[6];
	public int height = 0;
	public int x = 0;
	public int y = 0;
	public String linkTo = "";

	public LinkedList<Mobile> mobs = new LinkedList<Mobile>();
	public LinkedList<Player> players = new LinkedList<Player>();
	public LinkedList<Monster> monsters = new LinkedList<Monster>();

	public LinkedList<Battle> battles = new LinkedList<Battle>();

	public Room() {

	}

	public Room(MUD mud, World world, int id) {
		this.mud = mud;
		this.world = world;
		exit = new int[10];
		name = "Room " + this.id;
	}

	public boolean command(Player p, String cmd) {
		// run script here, return false if script executed a command
		return true;
	}

	public String getExitString(int i) {
		try {
			if (exit[i] > 0) {
				return getStdExitName(i) + " ";
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return "";
	}

	public static String getStdExitName(int i) {
		switch (i) {
		case 0:
			return "north";
		case 1:
			return "south";
		case 2:
			return "west";
		case 3:
			return "east";
		case 4:
			return "up";
		case 5:
			return "down";
		default:
			return "into a mysterious glowing light";
		}
	}

	public void sendAll(String st) { // note these send without prompts or newlines
		try {
			for (Player p : players) {
				p.send(st);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void sendAllCanSeeBut(Mobile but, String st) {
		try {
			for (Player p : players) {
				if (p.active() && p != but && p.canSee(but)) {
					p.send("\n" + st);
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void sendImm(String st) { // note these send without prompts or newlines
		try {
			for (Player p : players) {
				if (p.hasAccess()) {
					p.send("\n" + st);
				}

			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static String getArrivalString(int d) {
		try {
			switch (d) {
			case 0: //
				return MUD.messages.get("JOIN_DIRECTION_SOUTH");
			case 1:
				return MUD.messages.get("JOIN_DIRECTION_NORTH");
			case 2:
				return MUD.messages.get("JOIN_DIRECTION_EAST");
			case 3:
				return MUD.messages.get("JOIN_DIRECTION_WEST");
			case 4:
				return MUD.messages.get("JOIN_DIRECTION_DOWN");
			case 5:
				return MUD.messages.get("JOIN_DIRECTION_UP");
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return "around";
	}

	public static String getDepartureString(int d) {
		try {
			switch (d) {
			case 0:
				return MUD.messages.get("PART_DIRECTION_NORTH");
			case 1: //
				return MUD.messages.get("PART_DIRECTION_SOUTH");
			case 2:
				return MUD.messages.get("PART_DIRECTION_WEST");
			case 3:
				return MUD.messages.get("PART_DIRECTION_EAST");
			case 4:
				return MUD.messages.get("PART_DIRECTION_UP");
			case 5:
				return MUD.messages.get("PART_DIRECTION_DOWN");
			}

		} catch (Exception e) {
			e.printStackTrace();
		}
		return "around";
	}

	public void join(Mobile mob, int dir) { // note that dir is the direction player is coming FROM
		// so we need to invert it to describe their arrival
		try {
			mobs.add(mob);
			//Log.debug(mob.getFullID() + " joined room " + id);
			if (mob.isPlayer) {
				Player p = (Player) mob;
				players.add(p);
			} else if (mob.isMonster) {
				Monster m = (Monster) mob;
				monsters.add(m);
			}

			if (dir == -1) {
				sendAllCanSeeBut(mob, mob.getArticleWithSpace(true)
						+ MUD.messages.get("LOGON_MAP").replace("{{name}}", (String) mob.fields.get("name")));
			} else {
				String join = mob.getArticleWithSpace(true) + MUD.messages.get("JOIN_MAP");
				sendAllCanSeeBut(mob,
						join.replace("{{name}}", (String) mob.fields.get("name"))
								.replace("{{travel:mode}}", mob.getTravelVerbPlural(dir))
								.replace("{{travel:direction_join}}", getArrivalString(dir)));
			}
			if (mob.isPlayer && dir != -1) {
				((Player) mob).look(false);
				// roomScript("join", MUD.generalScriptFields, new Object[] { this, mob, dir });
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public boolean say(Mobile mob, String rest) {
		// LinkedList<Object> result = roomScript("say", MUD.generalScriptFields, new
		// Object[] { this, mob, rest });
		// for (Object o : result) {
		// ((boolean) o == false) {
		// return false;
		// }
		// }
		mob.say(rest);
		return true;
	}

	public void part(Mobile mob, int dir) {
		try {
			mobs.remove(mob);
			//Log.debug(mob.getFullID() + " parted room " + id);
			if (mob instanceof Player) {
				Player p = (Player) mob;
				players.remove(p);
			} else if (mob instanceof Monster) {
				Monster m = (Monster) mob;
				monsters.remove(m);
			}
			if (dir == -1) {
				sendAllCanSeeBut(mob,
						MUD.messages.get("LOGOFF_MAP").replace("{{name}}", (String) mob.fields.get("name")));
			} else {
				String part = mob.getArticleWithSpace(true) + MUD.messages.get("PART_MAP");
				String s = part.replace("{{name}}", (String) mob.fields.get("name"));
				s = s.replace("{{travel:mode}}", mob.getTravelVerbPlural(dir));
				s = s.replace("{{travel:direction_part}}", getDepartureString(dir));
				sendAllCanSeeBut(mob, s);
			}
			if (mob.isPlayer) {
				// roomScript("part", MUD.generalScriptFields, new Object[] { this, mob, dir });
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void sendAllBut(Mobile but, String st) { // note these send without prompts or newlines
		try {
			for (Player p : players) {
				if (p.isPlayer && p != but) {
					p.send(st);
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void sendImmBut(Mobile but, String st) { // note these send without prompts or newlines
		try {
			for (Player p : players) {
				if (p.isPlayer && p != but && p.hasAccess()) {
					p.send("\n" + st);
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void update(long tick) {
		for (Battle b : battles) {
			b.update(tick);
		}
	}

	public Monster[] getMonsterArray() {
		Monster[] mon = new Monster[monsters.size()];
		int i = 0;
		for (Monster m : monsters) {
			mon[i] = m;
			i++;
		}
		return mon;
	}

	public Mobile findMob(String name) {
		String mname = "";
		for (Mobile m : mobs) {
			mname = ((String) m.fields.get("name")).toLowerCase();
			if (mname.indexOf(name.toLowerCase()) == 0) {
				return m;
			}
		}
		return null;
	}

}
