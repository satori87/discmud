package com.bg.discmud.item;

import com.bg.discmud.core.MUD;
import com.bg.discworld.utility.Log;
import com.bg.discworld.world.World;

public class Item extends ItemSheet {

	MUD mud;
	World world;

	public int qty = 0;

	public Container container = null;

	public Item(MUD mud, String name, int qty) {
		super();
		try {
			this.mud = mud;
			world = mud.world;
			this.qty = qty;
			fields.putAll(world.itemSheets.get(name).fields);
			fields.put("name", name);
		} catch (Exception e) {
			Log.debug(e);
		}
	}

	public double getWeight() {
		return getContainerWeight() + super.getWeight();
	}

	public double getContainerWeight() {
		if (container != null) {
			return container.getWeight();
		}
		return 0;
	}

	public boolean quantifiable() {
		switch (getType().toUpperCase()) {
		case "CURRENCY":
		case "RESOURCE":
			return true;
		}
		return false;
	}

	public String getType() {
		Object o = fields.get("type");
		if (o == null) {
			return "resource";
		} else {
			return (String) o;
		}
	}

}
