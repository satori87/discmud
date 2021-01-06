package com.bg.discmud.item;

import java.util.ArrayList;

import com.bg.discmud.core.MUD;
import com.bg.discworld.utility.TextParser;

public class Container extends ArrayList<Item> {

	private static final long serialVersionUID = 1L;
	MUD mud;
	private double capacity = 0; // total weight capacity
	private double weight = 0; // current weight of all contents
	public double factor = 1; // external weight factor 0-1

	public boolean allowOverfill = false; // enable to allow container to exceed capacity

	public Container(MUD mud, double capacity, double factor, boolean allowOverFill) {
		super();
		this.mud = mud;
		this.capacity = capacity;
		this.factor = factor;
	}

	@Override
	public boolean add(Item i) {
		try {
			double w = i.getWeight();
			if (w + weight <= capacity || allowOverfill || capacity == 0) {
				if (super.add(i)) {
					weight += w;
					return true;
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return false;
	}

	@Override
	public boolean remove(Object i) {
		try {
			if (super.remove((Item) i)) {
				weight -= ((Item) i).getWeight();
				return true;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return false;
	}

	@Override
	public void clear() {
		super.clear();
		weight = 0;
	}

	public double getWeight() { // returns the EXTERNAL weight
		return weight * factor;
	}

	public String getDisplayString() {
		String s = "";
		try {
			for (Item i : this) {
				s += TextParser.formattedText(i.qty + "", 6, 1) + "   " + i.fields.get("name") + "\n";
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return s;
	}
}
