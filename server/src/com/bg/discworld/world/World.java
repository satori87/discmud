package com.bg.discworld.world;

import java.util.HashMap;
import java.util.Map;

import com.badlogic.gdx.utils.LongMap;
import com.bg.discmud.core.MUD;
import com.bg.discmud.item.ItemSheet;
import com.bg.discworld.mobile.Monster;
import com.bg.discworld.mobile.MonsterSheet;

public class World {

	public String name = "Tidebreak";

	MUD mud;

	//public Area[] area = new Area[100];
	//public Room[] room = new Room[100000];

	 //public Map<Integer, Area> area = new HashMap<>(); //come back and do
	// this
	// public Map<Integer, Room> room = new HashMap<>();

	public HashMap<String, Area> area = new HashMap<String, Area>();
	
	public HashMap<Integer, Monster> monster = new HashMap<>();

	public HashMap<Integer, MonsterSheet> monsterSheets = new HashMap<Integer, MonsterSheet>();
	public HashMap<Integer, ItemSheet> itemSheets = new HashMap<Integer, ItemSheet>();

	public static int monsterPointer = 0; // next free monster uid

	public World(MUD mud) {
		this.mud = mud;
	}

	public void update(long tick) {
		for(Area a : area.values()) {
			a.update(tick);
		}
	}

	public Monster spawnMonster(Room r, int id) {
		Monster m = null;
		try {
			MonsterSheet ms = monsterSheets.get(id);
			if (ms == null) {
				return null;
			}
			int uid = monsterPointer;
			monsterPointer++;
			m = new Monster(mud, uid);
			r.join(m, -1);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return m;
	}

}
