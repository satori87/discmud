package com.bg.discmud.item;

import java.util.HashMap;
import java.util.Map;

public class ItemSheet {

	public  Map<String, Object> fields = new HashMap<>();
	
	public double getWeight() {
		return (double) fields.get("weight");
	}

}
