package com.bg.discworld.mobile;

import com.bg.discmud.core.MUD;
import com.bg.discworld.battle.Formula;
import com.bg.discworld.utility.Log;

public class Monster extends Mobile {
	public String name = "";
	public long nextMoveAt = 0; // to add delay to monsters
	public int moveState = 0; // 0 = finished first delay, act, 1 = finished action, end turn

	public Monster(MUD mud, String name) {
		super(mud, 0);
		try {
			this.name = name;
			active = true;
			fields.putAll(world.monsterSheets.get(name).fields);
			int hp = Formula.dice((String)fields.get("hp_dice"));
			fields.put("hp", hp);
			fields.put("max_hp", hp);
		} catch (Exception e) {
			Log.debug(e);
		}

	}	
	
}
