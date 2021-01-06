package com.bg.discworld.world;

import java.util.HashMap;
import java.util.Map;

import com.badlogic.gdx.utils.LongMap;
import com.bg.discmud.core.MUD;
import com.bg.discmud.item.ItemSheet;
import com.bg.discworld.mobile.Monster;
import com.bg.discworld.mobile.MonsterSheet;

public class World {

	public volatile String name = "Tidebreak";

	MUD mud;

	public volatile Area[] area = new Area[100];
	public volatile Room[] room = new Room[100000];

	 //public Map<Integer, Area> area = new HashMap<>(); //come back and do
	// this
	 //public Map<Integer, Room> room = new HashMap<>();

	public volatile Map<Integer, Monster> monster = new HashMap<>();

	public volatile LongMap<MonsterSheet> monsterSheets = new LongMap<MonsterSheet>();
	public volatile LongMap<ItemSheet> itemSheets = new LongMap<ItemSheet>();

	public volatile static int monsterPointer = 0; // next free monster uid

	public World(MUD mud) {
		this.mud = mud;
		for (int i = 0; i < 100; i++) {
			area[i] = new Area(mud, this, i);
		}
		for (int i = 0; i < 100000; i++) {
			room[i] = new Room(mud, this, 0, i);
		}
	}

	public void update(long tick) {
		for (Room r : room) {
			r.update(tick);
		}
	}

	public Monster spawnMonster(int r, long id) {
		Monster m = null;
		try {
			MonsterSheet ms = monsterSheets.get(id);
			if (ms == null) {
				return null;
			}
			int uid = monsterPointer;
			monsterPointer++;
			m = new Monster(mud, uid);
			room[r].join(m, -1);
		} catch (Exception e) {
			e.printStackTrace();
		}
		return m;
	}

}
