package com.bg.discworld.world;

import java.util.HashMap;
import com.bg.discmud.core.MUD;
import com.bg.discmud.item.ItemSheet;
import com.bg.discworld.mobile.MonsterSheet;

public class World {

	MUD mud;

	public HashMap<String, Area> area = new HashMap<String, Area>();
	
	public HashMap<String, MonsterSheet> monsterSheets = new HashMap<String, MonsterSheet>();
	public HashMap<String, ItemSheet> itemSheets = new HashMap<String, ItemSheet>();

	public World(MUD mud) {
		this.mud = mud;
	}

	public void update(long tick) {
		for(Area a : area.values()) {
			a.update(tick);
		}
	}

}
