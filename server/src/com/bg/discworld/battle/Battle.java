package com.bg.discworld.battle;

import java.util.ArrayList;
import java.util.Collection;
import java.util.LinkedList;
import java.util.List;

import com.bg.discmud.core.MUD;
import com.bg.discworld.mobile.Mobile;
import com.bg.discworld.mobile.Monster;
import com.bg.discworld.player.Player;
import com.bg.discworld.utility.AStarAlgorithm;
import com.bg.discworld.utility.BCConvert;
import com.bg.discworld.utility.Grid;
import com.bg.discworld.utility.Log;
import com.bg.discworld.utility.Node;
import com.bg.discworld.utility.TextParser;
import com.bg.discworld.world.Room;
import com.bg.discworld.world.World;

public class Battle {

	MUD mud;
	World world;
	Room room;

	BattleTurn battleTurn;
	Node[][] field = new Node[12][12];

	static AStarAlgorithm astar;
	Grid grid;

	public Battle(MUD mud, Room r, Mobile[] ally, Mobile[] enemy) {
		try {
			this.mud = mud;
			this.world = mud.world;
			room = r;
			field = randomField();
			Mobile[] mobs = new Mobile[ally.length + enemy.length];
			int i = 0;
			for (Mobile m : ally) {
				place(m, true);
				mobs[i] = m;
				i++;
			}
			for (Mobile m : enemy) {
				place(m, false);
				mobs[i] = m;
				i++;
			}
			battleTurn = new BattleTurn(mud, mobs);
			// announce start of battle and the battle order
			beginTurn();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void start() {
		generateGrid(12, 12);
	}

	Node[][] randomField() {
		Node[][] field = new Node[12][12];
		try {
			for (int x = 0; x < 12; x++) {
				for (int y = 0; y < 12; y++) {
					field[x][y] = new Node(x, y, 0);
					if (x > 1 && x < 10) {
						if (y > 1 && y < 10) {
							if (Formula.random.nextInt(20) <= 3) {
								field[x][y].tile = 1;
								field[x][y].blocked = true;
							}
						}
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return field;
	}

	void place(Mobile m, boolean ally) {
		try {
			if (m.isPlayer) {
				Player p = (Player) m;
				p.missedTurns = 0;
			}
			m.ally = ally;
			int y;
			int x;
			int tries = 0;
			while (true) { // this will cause a dropped monster if you try to put more mobs on a team than
							// there is room on the 2 edge rows (16 without any obstacles)
				y = m.frontRow ? (ally ? 1 : 10) : (ally ? 0 : 11);
				x = placeInRow(m, y);
				if (x < 0) {
					if (tries == 0) {
						m.frontRow = !m.frontRow;
					} else {
						break; // drop this mobile, there is no room in either row!
					}
				} else {
					field[x][y].mob = m;
					m.x = x;
					m.y = y;
					break;
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	int placeInRow(Mobile m, int y) {
		int x = -1;
		try {
			for (int i = 0; i < 6; i++) {
				if (field[5 + i][y].tile == 0 && field[i + 5][y].mob == null) {
					x = 5 + i;
					break;
				} else if (field[5 - i][y].tile == 0 && field[5 - i][y].mob == null) {
					x = 5 - i;
					break;
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return x;
	}

	public boolean command(Player p, String cmd) {
		try {
			String[] cmds = cmd.split(" ");
			//Mobile m = null;
			switch (cmds[0].toUpperCase()) {
			case "MOVE":
			case "MOV":
			case "MO":
			case "M":
				Log.debug("move");
				if (cmds.length > 1 && cmds[1].length() == 2) {

				}
				break;
			case "MELEE":
			case "MELE":
			case "MEL":
			case "ME":
				//m = cmds.length > 1 ? p.getRoom().findMob(cmds[1]) : closestEnemyTo(p);
				//if (m != null) {
				//	p.meleeAttack(m);
				//	p.missedTurns = 0;
				//	endTurn();
				//}
				return false;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return true;
	}

	void beginTurn() {
		try {
			Mobile mob = battleTurn.next();
			mob.acted = false;
			mob.moved = false;
			// announce whose turn it is
			sendAllBut("It is " + mob.fields.get("name") + "'s turn.", mob);
			if (mob.isPlayer) {
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
				// its a players turn, send them battle menu and start a countdown
			} else {
				// is monster, do the thing
				Monster m = (Monster) mob;
				m.nextMoveAt = System.currentTimeMillis() + MUD.MONSTER_ACTION_DELAY;
				m.moveState = 0;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	void endTurn() {
		try {
			Mobile mob = battleTurn.current();
			if (mob.isPlayer) {
				((Player) mob).send("You end your turn.");
			}
			sendAllBut(mob.fields.get("name") + " ends turn.", mob);
			beginTurn();
		} catch (Exception e) {
			e.printStackTrace();
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
			e.printStackTrace();
		}
		return line;
	}

	String battleLine(Player p, int i) {
		int y = i / 2;
		String st = "";
		try {
			if (i % 2 == 0) { // its a space line
				st = "＃0000＃111｜222｜333｜444｜555｜666｜777｜888｜999｜!!!｜@@@｜$$$＃";
				st = st.replace("0000", " " + TextParser.formatNumber(y + 1, 2) + " ");
				st = st.replace("111", field[0][y].getString(p));
				st = st.replace("222", field[1][y].getString(p));
				st = st.replace("333", field[2][y].getString(p));
				st = st.replace("444", field[3][y].getString(p));
				st = st.replace("555", field[4][y].getString(p));
				st = st.replace("666", field[5][y].getString(p));
				st = st.replace("777", field[6][y].getString(p));
				st = st.replace("888", field[7][y].getString(p));
				st = st.replace("999", field[8][y].getString(p));
				st = st.replace("!!!", field[9][y].getString(p));
				st = st.replace("@@@", field[10][y].getString(p));
				st = st.replace("$$$", field[11][y].getString(p));

			} else { // its a grid line
				st = "＃＃＃＃＃＃－－－＋－－－＋－－－＋－－－＋－－－＋－－－＋－－－＋－－－＋－－－＋－－－＋－－－＋－－－＃";
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return st;
	}

	void showBattle(Player p) {
		try {
			String header = "＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃\r\n"
					+ "＃　　　　＃　Ａ　＃　Ｂ　＃　Ｃ　＃　Ｄ　＃　Ｅ　＃　F　＃　G　＃　H　＃　I　＃　J　＃　K　＃　L　＃　TURN　　　　　　　　NAME　　　　　　　　＃\r\n"
					+ "＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃";
			header = BCConvert.bj2qj(header);
			String footer = "＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃＃";
			p.send(header);
			for (int i = 22; i >= 0; i--) {
				p.send(BCConvert.bj2qj(battleLine(p, i) + turnLine(i)));
			}
			p.send(footer);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	boolean shouldMove(Mobile m) {
		try {
			if (m.acted) {
				return inEnemyRange(m, m.getRange()).size() > 0;
				// TODO theres definitely a lot of moves that are beneficial
				// to expand this, mob can attempt to move towards frays when healthy
				// and prioritize moving out ofas many mobs movement range as possible as health
				// decreases
			} else {
				return enemyInRange(m, m.getRange()).size() == 0;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return false;
	}

	LinkedList<Mobile> inEnemyRange(Mobile m, double range) {
		LinkedList<Mobile> enemy = new LinkedList<Mobile>();
		try {
			for (Mobile e : battleTurn) {
				if (e != m && e.ally != m.ally) {
					if (m.distTo(e) <= e.getRange()) {
						enemy.add(e);
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return enemy;
	}

	boolean isTurnOver(Mobile m) {
		try {
			return (m.moved || !shouldMove(m)) && (m.acted || enemyInRange(m, m.getRange()).size() == 0);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return true;
	}

	public void update(long tick) {
		try {
			// once finished, this function should really only handle idleness and changing
			// of turns
			// when a mobiles turn comes up, move everything to mobile.turn() with a
			// parameter to support delay
			Mobile mob = battleTurn.current();
			if (mob.isPlayer) {
				Player p = (Player) mob;
				if (tick > p.turnOverAt) { // force turn to be OVER
					p.missedTurns++;
					if (p.missedTurns <= 4) {
						p.send(
								"Your turn will be forcibly ended. After 3 consecutive missed turns, your turn timer is set to "
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
							// Mobile nme = closestEnemyTo(m);
							// m.meleeAttack(nme);

						for (Node n : astar.getNetwork().getNodes()) {
							n.setCost(0);
							n.virtual_weight = 0;
						}
						for (Mobile e : battleTurn) {
							if (e.ally != m.ally) {
								int ex = 0;
								int ey = 0;
								for (int x = -1; x < 2; x++) {
									for (int y = -1; y < 2; y++) {
										ex = x + e.x;
										ey = y + e.y;
										if (ex >= 0 && ex < 12 && ey >= 0 && ey < 12) {
											field[ex][ey].virtual_weight += 1.0;
										}
									}
								}
							}
						}
						astar = new AStarAlgorithm(grid);
						autoTurn(m);
						if (isTurnOver(m)) {
							m.moveState++;
						}

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
			e.printStackTrace();
		}
	}

	LinkedList<Mobile> getEnemyInNodes(Mobile m, Collection<Node> nodes) {
		LinkedList<Mobile> enemy = new LinkedList<Mobile>();
		try {
			for (Node n : nodes) {
				if (n.mob != null && n.mob.ally != m.ally) {
					enemy.add(n.mob);
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return enemy;

	}

	Mobile closestEnemy(Mobile m) {
		Mobile best = null;
		try {
			double d = 0;
			double lowest = 9999.0;
			for (Mobile e : battleTurn) {
				if (e.ally != m.ally) {
					d = e.distTo(m);
					if (d < lowest) {
						best = e;
						lowest = d;
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return best;
	}

	Node closestNodeTo(Mobile m, Collection<Node> nodes) {
		try {
			return closestNodeTo(m.x, m.y, nodes);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	Node closestNodeTo(int x, int y, Collection<Node> nodes) {
		Node best = null;
		try {
			double lowest = 9999.0;
			double d = 0;
			for (Node n : nodes) {
				d = Formula.distance(x, y, n.x, n.y);
				if (d < lowest) {
					lowest = d;
					best = n;
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return best;
	}

	Node getSafestNode(Mobile m, Collection<Node> nodes) {
		Node best = null;
		try {
			double[][] count = new double[12][12];
			for (Node n : nodes) {
				count[n.x][n.y] = 0.0;
			}
			LinkedList<Mobile> nearbyEnemies = getEnemyInNodes(m, nodes);
			ArrayList<Node> validNodes;
			for (Mobile e : nearbyEnemies) {
				validNodes = getValidMoves(e);
				for (Node n : validNodes) {
					count[n.x][n.y] += 0.4;
				}
			}

			double lowest = 9999.0;
			double d = 0;
			for (Node n : nodes) {
				d = count[n.x][n.y];
				d += n.virtual_weight + n.weight;
				if (d < lowest) {
					lowest = d;
					best = n;
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return best;
	}

	void chooseMove(Mobile m) {
		try {
			// LinkedList<Mobile> theirRange = inEnemyRange(m, m.getRange());
			Node best = null;
			Mobile e = null;
			ArrayList<Node> validNodes = getValidMoves(m);
			if (validNodes.size() == 0) {
				Log.debug("B0");
				return;
			}
			if (m.acted) {
				Log.debug("B1");
				best = getSafestNode(m, validNodes);
				if (best != null) {
					Log.debug("BFREE");
					move(m, best);
				}
				// lets move away from the most enemies
			} else {
				// lets move towards best target
				Log.debug("B2");
				LinkedList<Mobile> nearbyEnemies = getEnemyInNodes(m, validNodes);
				// now that we have a list of enemies in nodes we can move to, we can pick one
				// to fight
				while (true) {
					Log.debug("BW");
					e = bestTarget(m, nearbyEnemies);
					if (e == null) {
						Log.debug("B3");
						// try to move towards enemy
						e = closestEnemy(m);
						if (e != null) {
							Log.debug("B4");
							best = closestNodeTo(e, validNodes);
							if (best != null) {
								Log.debug("B5");
								move(m, best);
							} else {
								Log.error("Really Shouldnt happen but it did in chooseMove");
							}
						} else {
							Log.error("Shouldnt happen but it did in chooseMove");
							// wait, why are we in a battle then? somethings fucked up here
						}
						break;
					} else {
						Log.debug("B6");
						best = bestSpotNear(m, e, validNodes);
						if (!validNodes.contains(best)) {
							Log.debug("B7");
							if (best == null) {
								Log.debug("B8");
								nearbyEnemies.remove(e);
								if (nearbyEnemies.size() == 0) {
									Log.debug("B9");
									// try to get closer to the fray since we cant move to attack anything
									Log.debug("neato2");
									break;
								}
							} else {
								Log.debug("B10");
								move(m, best);
								break;
							}
						}
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	void move(Mobile m, Node n) {
		try {
			field[m.x][m.y].mob = null;
			m.x = n.x;
			m.y = n.y;
			n.mob = m;
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	Node bestSpotNear(Mobile m, Mobile n, Collection<Node> nodes) {
		try {
			return bestSpotNear(m, n.x, n.y, nodes);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	Node bestSpotNear(Mobile m, int x, int y, Collection<Node> nodes) {
		Node best = null;
		try {
			double lowest = 9999f;
			// find best node such that node is adjacent to x/y
			for (Node n : nodes) {
				if (n.adjacentTo(x, y)) {
					if (n.virtual_weight < lowest) {
						lowest = n.virtual_weight;
						best = n;
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return best;
	}

	void autoTurn(Mobile m) {
		Log.debug("A1");
		try {
			if (!m.moved) {
				Log.debug("A2");
				if (shouldMove(m)) {
					Log.debug("A3");
					chooseMove(m);
					m.moved = true;
					return;
				}
			}
			Log.debug("A4");
			LinkedList<Mobile> enemy = enemyInRange(m, m.getRange());
			if (!m.acted) {
				Log.debug("A5");
				if (m.moved) {
					Log.debug("A6");
					if (enemy.size() > 0) {
						// pick one and fight it
						Log.debug("A7");
						chooseAction(m, enemy);
					} else {
						Log.debug("A8");
						// guess we cant do nothing
					}
				} else {
					// we didnt move for a reason, its because we can already fight or we couldnt
					// find a move
					Log.debug("A9");
					if (enemy.size() > 0) {
						// pick one and fight it
						Log.debug("A10");
						chooseAction(m, enemy);
					} else {
						Log.debug("A11");
						m.moved = true;
						// we didnt move and we cant act, this sucks
					}

				}
				m.acted = true;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	void chooseAction(Mobile m, List<Mobile> enemy) {
		try {
			Mobile e = bestTarget(m, enemy);
			m.meleeAttack(e);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	Mobile bestTarget(Mobile m, List<Mobile> enemy) {
		Mobile best = null;
		try {
			double dist = 9999.0;
			double d = 0;
			for (Mobile e : enemy) {
				if (e.ally != m.ally) {
					d = m.distTo(e);
					if (d < dist) {
						dist = d;
						best = e;
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return best;
	}

	public void sendAll(String s) {
		try {
			for (Mobile m : battleTurn) {
				if (m.isPlayer) {
					Player p = (Player) m;
					p.send(s);
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void sendAllBut(String s, Mobile but) {
		try {
			for (Mobile m : battleTurn) {
				if (m.isPlayer && m != but) {
					Player p = (Player) m;
					p.send(s);
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	LinkedList<Mobile> enemyInRange(Mobile m, double range) {
		LinkedList<Mobile> enemy = new LinkedList<Mobile>();
		try {
			for (Mobile e : battleTurn) {
				if (e != m && e.ally != m.ally) {
					if (m.distTo(e) <= m.getRange()) {
						enemy.add(e);
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return enemy;
	}

	ArrayList<Node> getValidMoves(Mobile m) {
		ArrayList<Node> valid = new ArrayList<Node>();
		try {
			astar.setStart(grid.find(m.x, m.y));
			Node end = null;
			ArrayList<Node> path = null;
			double total = 0;
			for (int x = 0; x < 12; x++) {
				for (int y = 0; y < 12; y++) {
					//Log.debug(x + "," + y);
					if (Formula.distance(x, y, m.x, m.y) <= (double) ((m.getSpeed() + 1) / 5)) {
						total = 0;
						end = grid.find(x, y);
						if (end != null && end.mob == null && !end.blocked) {
							astar.setEnd(end);
							astar.solve(false);
							path = astar.getPath();
							if (path != null) {
								for (Node n : path) {
									total += n.getCost() * 5.0;
								}
								total -= 15.0; // remove unnecesary nodes from this total. might alter this later
								if (total <= (double) m.getSpeed() && total > 0) {
									valid.add(end);
								}
							}
						}
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return valid;
	}

	void generateGrid(int width, int height) {
		try {
			ArrayList<Node> tiles = new ArrayList<>();
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					tiles.add(field[x][y]);
				}
			}
			Grid grid = new Grid(width, height, tiles);
			for (Node t : grid.getTiles()) {
				t.neighbors.clear();
				t.calculateNeighbours(grid);

			}
			astar = new AStarAlgorithm(grid);
			this.grid = grid;
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}
