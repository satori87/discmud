package com.bg.discmud.item;

import java.util.HashMap;

public class Equipment extends HashMap<String, Integer> {

	private static final long serialVersionUID = 1L;

	public static final String validSlots = "head,torso,legs,left-hand,right-hand,feet,hands,arms,shoulders,face,wrists,waist";

	public Equipment() {
		super();
	}

	@Override
	public Integer get(Object key) {
		try {
			String[] slots = validSlots.split(",");
			for (String s : slots) {
				if (s.equalsIgnoreCase((String) key)) {
					return super.get(key);
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return -1;
	}

	@Override
	public Integer put(String key, Integer val) {
		try {
			String[] slots = validSlots.split(",");
			for (String s : slots) {
				if (s.equalsIgnoreCase(key)) {
					//put item requirement script call here
					return super.put(key, val);
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return -1;
	}

	public void load(String st) {
		
	}
	
}
