package com.bg.discworld.mobile;

import java.util.HashMap;
import java.util.Map;
import com.bg.discmud.core.MUD;
import com.bg.discmud.item.Container;
import com.bg.discmud.item.Equipment;
import com.bg.discworld.battle.Battle;
import com.bg.discworld.player.Player;
import com.bg.discworld.utility.Log;
import com.bg.discworld.utility.TextParser;
import com.bg.discworld.world.Area;
import com.bg.discworld.world.Room;
import com.bg.discworld.world.World;

public class Mobile {

	public MUD mud;
	public World world;

	public int side = 0;

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
	
	public Mobile(MUD mud) {
		this.mud = mud;
		this.world = mud.world;
		inventory = new Container(mud, 100.0, 1.0, true);
		equipment = new Equipment();
	}
	
	public float getTurnSpeed() {
		return 1.0f;
	}
	
	public boolean isPlayer() {
		return this instanceof Player;
	}
	
	public boolean isMonster() {
		return this instanceof Monster;
	}
	
	public void autoTurn() {
		
	}
	
	public String getFullID() { // this is how player is represented in logs
		try {
			if (player() != null) {
				return "[name:" + fields.get("name") + " id:" + id +  "]";
			} else {
				return (String) fields.get("name");
			}
		} catch (Exception e) {
			Log.debug(e);
		}
		return "unnamed_mobile";
	}

	public Room getRoom() {
		try {
			int room = (int) fields.get("room");
			return getArea().rooms.get(room);
		} catch (Exception e) {
			Log.debug(e);
		}
		return null;
	}
	
	public Area getArea() {
		try {
			String area = (String) fields.get("area");
			return world.area.get(area);
		} catch (Exception e) {
			Log.debug(e);
		}
		return null;
	}

	public void setRoom(Room room) {
		try {
			fields.put("room", room.id);
		} catch (Exception e) {
			Log.debug(e);
		}
	}

	public void moveTo(Area area, Room room, int dir) {
		//Log.debug(getFullID() + " move to " + room.id + " " + area.name);
		try {
			getRoom().part(this, dir);
			fields.put("area", area.name);
			setRoom(room);			
			getRoom().join(this, dir);
		} catch (Exception e) {
			Log.debug(e);
		}
	}

	public void say(String rest) {
		try {
			getRoom().sendAllBut(this, fields.get("name") + " says '" + rest + "'");
			if (isPlayer()) {
				((Player) this).send("You say '" + rest + "'");
			}
		} catch (Exception e) {
			Log.debug(e);
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
			//return (boolean) fields.get("front_row");
			return true;
		} catch (Exception e) {
			Log.debug(e);
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
	
	public int getSpeed() {
		return (int) fields.get("speed");
	}
}
