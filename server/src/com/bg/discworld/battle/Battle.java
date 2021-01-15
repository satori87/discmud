package com.bg.discworld.battle;

import java.util.ArrayList;

import com.bg.discmud.core.MUD;
import com.bg.discworld.mobile.Mobile;
import com.bg.discworld.mobile.Monster;
import com.bg.discworld.player.Player;
import com.bg.discworld.utility.Log;
import com.bg.discworld.utility.TextParser;
import com.bg.discworld.world.Room;
import com.bg.discworld.world.World;

public class Battle {

	MUD mud;
	World world;
	Room room;

	BattleTurn battleTurn;
	
	ArrayList<Mobile> mobs = new ArrayList<Mobile>();	//all mobs incl player and monster

	public Battle(MUD mud, Room r) {
		try {
			this.mud = mud;
			this.world = mud.world;
			room = r;			
		} catch (Exception e) {
			Log.debug(e);
		}
	}
	
	public void addMob(Mobile m, int side) {
		mobs.add(m);
		m.side = side;
	}
	
	public ArrayList<Mobile> getSide(int side) {
		ArrayList<Mobile> ret = new ArrayList<Mobile>();
		for(Mobile m : mobs) {
			if(m.side == side) {
				ret.add(m);
			}
		}
		return ret;
	}

	public void start() {
		battleTurn = new BattleTurn(mud, mobs);
		// announce start of battle and the battle order
		sendAll("Battle headeR");
		beginTurn();
	}

	public boolean command(Player p, String cmd) {
		try {
			String[] cmds = cmd.split(" ");
			// Mobile m = null;
			switch (cmds[0].toUpperCase()) {
			case "MELEE":
			case "MELE":
			case "MEL":
			case "ME":
				// m = cmds.length > 1 ? p.getRoom().findMob(cmds[1]) : closestEnemyTo(p);
				// if (m != null) {
				// p.meleeAttack(m);
				// p.missedTurns = 0;
				// endTurn();
				// }
				return false;
			}
		} catch (Exception e) {
			Log.debug(e);
		}
		return true;
	}

	void beginTurn() {
		try {
			Mobile mob = battleTurn.next();
			// announce whose turn it is
			sendAllBut("It is " + mob.fields.get("name") + "'s turn.", mob);
			if (mob.isPlayer()) {
				Player p = (Player) mob;
				p.send("It is your turn.");
				p.timesWarned = 0;
				int interval = MUD.PLAYER_TURN_TIME;
				if (p.missedTurns > 3) {
					interval = MUD.PLAYER_TURN_TIME_RUSHED;
					p.timesWarned = 99;
				}
				p.turnOverAt = System.currentTimeMillis() + interval;
				showBattle(p);
			} else {
				// is monster, do the thing
				Monster m = (Monster) mob;
				m.nextMoveAt = System.currentTimeMillis() + MUD.MONSTER_ACTION_DELAY;
				m.moveState = 0;
			}
		} catch (Exception e) {
			Log.debug(e);
		}
	}
	
	void showBattle(Player p) {
		
	}

	void endTurn() {
		try {
			Mobile mob = battleTurn.current();
			if (mob.isPlayer()) {
				((Player) mob).send("You end your turn.");
			}
			sendAllBut(mob.fields.get("name") + " ends turn.", mob);
			beginTurn();
		} catch (Exception e) {
			Log.debug(e);
		}
	}

	String turnLine(int i) {
		String line = "  $$.  @@@@@@@@@@@@@@@@  #";
		try {
			Mobile m;
			String empty = "                         #";
			boolean doublespace = battleTurn.size() <= 11;
			if (doublespace) {
				if (i % 2 == 0) {
					line = empty;
				} else {
					int n = (21 - i) / 2;
					if (n < battleTurn.size()) {
						m = battleTurn.get(n);
						line = line.replace("$$", TextParser.formattedText("" + (m.turn + 1), 2, 1));
						line = line.replace("@@@@@@@@@@@@@@@@", TextParser.formattedText(m.getName(), 16, 0));
						// line = line.replace("%%%%%%%",
						// TextParser.formatNumber(m.getHP(), 3) + "/" +
						// TextParser.formatNumber(m.getMaxHP(), 3));
					} else {
						line = empty;
					}
				}
			}
		} catch (Exception e) {
			Log.debug(e);
		}
		return line;
	}

	public void update(long tick) {
		try {
			// once finished, this function should really only handle idleness and changing
			// of turns
			// when a mobiles turn comes up, move everything to mobile.turn() with a
			// parameter to support delay
			Mobile mob = battleTurn.current();
			if (mob.isPlayer()) {
				Player p = (Player) mob;
				if (tick > p.turnOverAt) { // force turn to be OVER
					p.missedTurns++;
					if (p.missedTurns <= 4) {
						p.send("Your turn will be forcibly ended. After 3 consecutive missed turns, your turn timer is set to "
								+ MUD.PLAYER_TURN_TIME_RUSHED / 1000 + " seconds until you take an action.");
					} else {
						p.send("Your turn timer has been set to " + MUD.PLAYER_TURN_TIME_RUSHED / 1000
								+ " seconds until you act.");
					}
					endTurn();
				} else if (p.timesWarned == 0 && p.turnOverAt - tick < 20000) {
					p.send("You have 20 seconds left before your turn is forcibly ended.");
					p.timesWarned++;
				} else if (p.timesWarned == 1 && p.turnOverAt - tick < 10000) {
					p.send("You have 10 seconds left before your turn is forcibly ended.");
					p.timesWarned++;
				} else {
					// its players turn and they are within their rights to be taking this
					// long...for now
				}
			} else {
				Monster m = (Monster) mob;
				if (tick > m.nextMoveAt) {
					// do monsters turn here
					switch (m.moveState) {
					case 0:// MONSTER TURN
						autoTurn(m);
						m.moveState++;
						m.nextMoveAt = tick + MUD.MONSTER_ACTION_DELAY;
						break;
					case 1:
						endTurn();
						break;
					}
				} else {
					// we are still delaying so monster doesnt immediately spam all its shit, more
					// natural
				}
			}
		} catch (Exception e) {
			Log.debug(e);
		}
	}
	
	public void autoTurn(Mobile m) {
		
	}

	public void sendAll(String s) {
		try {
			for (Mobile m : battleTurn) {
				if (m.isPlayer()) {
					Player p = (Player) m;
					p.send(s);
				}
			}
		} catch (Exception e) {
			Log.debug(e);
		}
	}

	public void sendAllBut(String s, Mobile but) {
		try {
			for (Mobile m : battleTurn) {
				if (m.isPlayer() && m != but) {
					Player p = (Player) m;
					p.send(s);
				}
			}
		} catch (Exception e) {
			Log.debug(e);
		}
	}

}
