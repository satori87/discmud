package com.bg.discworld.battle;

import java.util.Random;

import com.bg.discworld.mobile.Mobile;

public class Formula {

	public static Random random = new Random();

	public static void seed(long s) {
		random.setSeed(s);
	}

	public static int d20() {
		return random.nextInt(20) + 1;
	}

	public static int getSavingThrow(Mobile m, String stat) {
		try {
			return d20() + abilityCheck(m, stat) + proficiencyCheck(m, stat + "_save");
		} catch (Exception e) {
			e.printStackTrace();
		}
		return 0;
	}

	public static int dexSave(Mobile m) {
		return getSavingThrow(m, "dexterity");
	}

	public static int intSave(Mobile m) {
		return getSavingThrow(m, "intelligence");
	}

	public static int strSave(Mobile m) {
		return getSavingThrow(m, "strength");
	}

	public static int conSave(Mobile m) {
		return getSavingThrow(m, "constitution");
	}

	public static int wisSave(Mobile m) {
		return getSavingThrow(m, "wisdom");
	}

	public static int chaSave(Mobile m) {
		return getSavingThrow(m, "charisma");
	}

	public static float initiativeCheck(Mobile m) {
		try {
			int alert = 0;
			int ibonus = 0;
			if (m.isPlayer) {
				alert = (boolean) m.fields.get("alert") ? 0 : 5;
				ibonus = (int) m.fields.get("initiative_bonus");
			}
			float check = d20() + dexCheck(m) + alert + ibonus;
			float fdex = (float) ((int) m.fields.get("dexterity"));
			check += fdex / 100f;
			return check;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return 0;
	}

	public static int skillCheck(Mobile m, String skill) {
		//TODO 
		//add check for if the skill is present
		int check = d20();
		switch (skill.toLowerCase()) {
		case "athletics":
			check += strCheck(m);
			break;
		case "acrobatics":
		case "sleight_of_hand":
		case "stealth":
			check += dexCheck(m);
			break;
		case "arcana":
		case "history":
		case "investigation":
		case "nature":
		case "religion":
			check += intCheck(m);
			break;
		case "animal_handling":
		case "insight":
		case "medicine":
		case "perception":
		case "survival":
			check += wisCheck(m);
			break;
		case "deception":
		case "intimidation":
		case "performance":
		case "persuasion":
			check += chaCheck(m);
			break;
		default:
			return 0;
		}
		return check + proficiencyCheck(m, skill);
	}

	public static int proficiencyCheck(Mobile m, String skill) {
		try {
			return (boolean) m.fields.get(skill) ? getProficiencyBonus(m) : 0;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return 0;
	}

	public static int abilityCheck(Mobile m, String stat) {
		try {
			return ((int) m.fields.get(stat) / 2) - 5;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return 0;
	}

	public static int dexCheck(Mobile m) {
		return abilityCheck(m, "dexterity");
	}

	public static int intCheck(Mobile m) {
		return abilityCheck(m, "intelligence");
	}

	public static int strCheck(Mobile m) {
		return abilityCheck(m, "strength");
	}

	public static int conCheck(Mobile m) {
		return abilityCheck(m, "constitution");
	}

	public static int wisCheck(Mobile m) {
		return abilityCheck(m, "wisdom");
	}

	public static int chaCheck(Mobile m) {
		return abilityCheck(m, "charisma");
	}

	public static int getProficiencyBonus(Mobile m) {
		try {
			return (((int) m.fields.get("level") - 1) / 4) + 2;
		} catch (Exception e) {
			e.printStackTrace();
		}
		return 2;
	}

	public static int roll(int numSides) {
		return random.nextInt(numSides) + 1;
	}
	
	public static int roll(int numDice, int numSides, int bonus) {
		int roll = bonus;
		for(int i = 0; i < numDice; i++) {
			roll += random.nextInt(numSides)+1;
		}
		return roll;
	}

	public static double distance(double x1, double y1, double x2, double y2) {

		double ac = Math.abs(y2 - y1);
		double cb = Math.abs(x2 - x1);

		return Math.hypot(ac, cb);
	}

	public static int getAC(Mobile m) {
		// we can add equipment jawn here
		return dexCheck(m) + 10;
	}

	public static String getGerund(String verb) {
		switch (verb) {
		case "pierce":
			return "piercing";
		}
		return verb + "ing";
	}
}
