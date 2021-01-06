package com.bg.discworld.mobile;

import java.util.HashMap;
import java.util.Map;

import com.bg.discmud.core.MUD;
import com.bg.discmud.item.Container;
import com.bg.discmud.item.Equipment;
import com.bg.discworld.battle.Battle;
import com.bg.discworld.battle.Formula;
import com.bg.discworld.player.Player;
import com.bg.discworld.utility.Log;
import com.bg.discworld.utility.TextParser;
import com.bg.discworld.world.Room;
import com.bg.discworld.world.World;

public class Mobile {

	public MUD mud;
	public World world;

	public boolean isPlayer = false;
	public boolean isMonster = false;
	public boolean isNPC = false;

	public long id = 0;
	public Map<String, Object> fields = new HashMap<>();

	public Container inventory;
	public Equipment equipment;

	// some state variables
	public boolean active = true;
	public Battle battle;

	public boolean frontRow = true;
	public boolean ally = true;
	public int x = 0;
	public int y = 0;
	public int turn = 0;

	public boolean moved = false;
	public boolean acted = false;
	public boolean bonus_acted = false;

	public Mobile(MUD mud, long id) {
		this.mud = mud;
		this.world = mud.world;
		this.id = id;
		inventory = new Container(mud, 100.0, 1.0, true);

		equipment = new Equipment();
	}

	public String getFullID() { // this is how player is represented in logs
		try {
			if (player() != null) {
				return "[name:" + fields.get("name") + " id:" + id +  "]";
			} else {
				return (String) fields.get("name");
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return "unnamed_mobile";
	}

	public Room getRoom() {
		try {
			int room = (int) fields.get("room");
			Log.debug("getroom" + room);
			return world.room[room];
		} catch (Exception e) {
			e.printStackTrace();
		}
		return world.room[0];
	}

	public void setRoom(int room) {
		try {
			Log.debug("setroom " + room);
			fields.put("room", room);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void moveTo(int nextRoom, int dir) {
		Log.debug(getFullID() + " move to " + nextRoom);
		try {
			getRoom().part(this, dir);
			setRoom(nextRoom);
			getRoom().join(this, dir);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void say(String rest) {
		try {
			getRoom().sendAllBut(this, fields.get("name") + " says '" + rest + "'");
			if (isPlayer) {
				((Player) this).send("You say '" + rest + "'");
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public boolean canSee(Mobile m) {
		return m.isVisible();
	}

	public boolean isVisible() {
		return true;
	}

	public String getTravelVerbPlural(int dir) {
		return "walks";
	}
	
	public String getTravelVerb(int dir) {
		return "walk";
	}

	public String getArticleWithSpace(boolean upper) {
		if (monster() != null) { // replace when we get field in db
			if (upper) {
				return "A ";
			} else {
				return "a ";
			}
		} else {
			return ""; // player will never have article probably so no space either
		}
	}

	public boolean active() {
		return active;
	}

	public Player player() {
		if (this instanceof Player) {
			return (Player) this;
		} else {
			return null;
		}
	}

	public Monster monster() {
		if (this instanceof Monster) {
			return (Monster) this;
		} else {
			return null;
		}
	}

	public String getIdlePhrase() {
		return getArticleWithSpace(true) + "{{name}} is here.";
	}

	public void load() {
		// inventory.load((String) fields.get("inventory"));
		// equipment.load((String) fields.get("equipment"));
	}

	public boolean frontRow() {
		try {
			return (boolean) fields.get("front_row");
		} catch (Exception e) {
			e.printStackTrace();
		}
		return true;
	}

	public boolean canAttack(Mobile m) {
		return battle == null && m.battle == null;
	}

	public String getShortHealthString() {
		//String st = "";
		double hp = (double) ((int) fields.get("hp"));
		double maxhp = (double) ((int) fields.get("max_hp"));
		double h = ((hp / maxhp) * 6);
		for (int i = 0; i < Math.round(h); i++) {
			//st += "+";
		}
		return TextParser.formattedText((int) hp + "/" + (int) maxhp, 9, 0);
		// return TextParser.formattedText(st, 6, -1);
	}

	public void takeDamageFrom(Mobile m, int dam, String type) {
		int hp = getHP();
		hp -= dam;
		if (hp < 0) {
			// DIED
			// restore health for testing purposes
			setHP(getMaxHP());
		} else {
			setHP(hp);
		}
	}

	public int getHP() {
		return (int) fields.get("hp");
	}

	public int getMaxHP() {
		return (int) fields.get("max_hp");
	}

	public String getName() {
		return (String) fields.get("name");
	}

	public void setHP(int hp) {
		fields.put("hp", hp);
	}

	public String getDamType() {
		Object o = fields.get("melee_damage_type");
		if (o != null) {
			return (String) o;
		} else {
			return "slash";
		}
	}

	public int meleeDamageDice() {
		Object o = fields.get("melee_damage_dice");
		if (o == null) {
			return 1;
		} else {
			return (int) o;
		}
	}

	public int meleeDamageSides() {
		Object o = fields.get("melee_damage_sides");
		if (o == null) {
			return 1;
		} else {
			return (int) o;
		}
	}

	public int meleeDamageBonus() {
		Object o = fields.get("melee_damage_modifier");
		if (o == null) {
			return 1;
		} else {
			return (int) o;
		}
	}

	public void meleeAttack(Mobile nme) {
		Mobile m = this;
		String sign = "";
		String name = m.getName();
		int hitbonus = Formula.strCheck(m);
		String damtype = m.getDamType();
		String nmeName = nme.getName();
		int hitroll = Formula.d20();
		int ac = Formula.getAC(nme);
		if (hitbonus > 0) {
			sign = "+";
		}
		if (hitbonus != 0) {
			sign += hitbonus;
		}
		boolean hit = (hitroll == 20) || (hitroll > 1 && hitroll + hitbonus > ac);
		String st = name + " rolls 1d20" + sign + " to hit " + nmeName + " (AC:" + ac + ") and rolls a "
				+ (hitroll + hitbonus) + ". " + (hit ? "SUCCESS" : "FAILURE");
		battle.sendAll(st);
		if (hitroll == 0) {
			battle.sendAll(name + " curses angry fairies for its embarassing display.");
		} else if (hit) {
			int damdice = m.meleeDamageDice();
			int damsides = m.meleeDamageSides();
			int dambonus = m.meleeDamageBonus();
			sign = "";
			if (dambonus > 0) {
				sign = "+";
			}
			sign += dambonus + "";
			if (hitroll == 20) {
				damdice *= 2;
				battle.sendAll("Critical hit! Hermes himself couldn't have guided " + name + "'s " + damtype
						+ " with greater accuracy.");
			}
			int damroll = Formula.roll(damdice, damsides, dambonus);
			if (damroll < 0)
				damroll = 0;
			st = name + " rolls " + damdice + "d" + damsides + sign + " and does " + damroll + " "
					+ Formula.getGerund(damtype) + " damage to " + nmeName + ".";
			if (damroll > 0) {
				nme.takeDamageFrom(m, damroll, damtype);
			}
			battle.sendAll(st);

		} else {
			battle.sendAll(name + "'s " + damtype + " misses " + nmeName + ".");
		}
	}

	public double distTo(Mobile m) {
		return Formula.distance(m.x, m.y, x, y);
	}

	public double getRange() {
		return 1.5;
	}
	
	public int getSpeed() {
		return (int) fields.get("speed");
	}
}
