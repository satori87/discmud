package com.bg.discworld.world;

import java.util.HashMap;

public class Area {

	public long id = 0;
	public String name = "";
	public String displayName = "";

	public HashMap<Integer, Room> rooms = new HashMap<Integer, Room>();

	// ambient effects, weather data, maybe wild animal likelihood
	// generic area-wide mobile spawns maybe?
	// area-wide item?
	// quest info, maybe list of specific rooms for some purpose

	public Area() {

	}

	public Area(long id) {
		this.id = id;
	}

	public Room searchRoom(String name) {
		for(Room r : rooms.values()) {
			if(r.name.equals(name)) {
				return r;
			}
		}
		return null;
	}
	
	public void update(long tick) {
		for (Room r : rooms.values()) {
			r.update(tick);
		}
	}

}
