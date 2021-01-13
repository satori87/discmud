package com.bg.discworld.battle;

import java.util.Random;


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
	
	public static int dice(String dice) {
		//TODO
		return 1;
	}
}
