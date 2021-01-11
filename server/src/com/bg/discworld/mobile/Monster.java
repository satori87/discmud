package com.bg.discworld.mobile;

import com.bg.discmud.core.MUD;
import com.bg.discworld.battle.Formula;

public class Monster extends Mobile {
	public String name = "";
	public long nextMoveAt = 0; // to add delay to monsters
	public int moveState = 0; // 0 = finished first delay, act, 1 = finished action, end turn

	public Monster(MUD mud, String name, int id) {
		super(mud, id);
		try {
			this.name = name;
			active = true;
			isMonster = true;
			fields.putAll(world.monsterSheets.get(name).fields);
			//fields.put("hit_dice_modifier", 0);
			//fields.put("melee_damage_modifier", 3);
			//fields.put("melee_damage_dice", 2);
			//fields.put("melee_damage_bonus", 3);
			int hpdice = 3;
			int hpsides = 8;
			int hpbonus = 4;
			//fields.put("hp_dice", hpdice);
			//fields.put("hp_dice_sides", hpsides);
			//fields.put("hp_dice_bonus", hpbonus);
			int hp = Formula.roll(hpdice,hpsides,hpbonus);
			fields.put("hp", hp);
			fields.put("max_hp", hp);
		} catch (Exception e) {
			e.printStackTrace();
		}
		/*
		 * 
		 * hp_dice_modifier = models.IntegerField(default=0) hit_dice_modifier =
		 * models.IntegerField(default=0) melee_damage_modifier =
		 * models.IntegerField(default=0)
		 */

	}

	
	
}
