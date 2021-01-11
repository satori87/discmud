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
	
	public static int getAC(Mobile m) {
		// we can add equipment jawn here
		return dexCheck(m) + 10;
	}

	public static double distance(double x1, double y1, double x2, double y2) {

		double ac = Math.abs(y2 - y1);
		double cb = Math.abs(x2 - x1);

		return Math.hypot(ac, cb);
	}

	public static String getGerund(String verb) {
		switch (verb) {
		case "pierce":
			return "piercing";
		}
		return verb + "ing";
	}
}
