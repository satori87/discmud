package com.bg.discmud.database;

public class Model {

	// These are all populated during a fetch

	public static String playerModel = "";
	public static String monsterModel = "";
	public static String itemModel = "";
	public static String npcModel = "";
	public static String uniqueModel = "";

	public static void clear() {
		playerModel = "";
		monsterModel = "";
		itemModel = "";
		npcModel = "";
		uniqueModel = "";
	}

}
