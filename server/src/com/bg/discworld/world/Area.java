package com.bg.discworld.world;

import com.bg.discmud.core.MUD;

public class Area {

	MUD mud;
	World world;

	public long id = 0;

	public  volatile String name = "Wilderness";

	//ambient effects, weather data, maybe wild animal likelihood
	//generic area-wide mobile spawns maybe?
	//area-wide item?
	//quest info, maybe list of specific rooms for some purpose
	
	public Area(MUD mud, World world, long id) {
		this.mud = mud;
		this.world = world;
		this.id = id;
	}

}
