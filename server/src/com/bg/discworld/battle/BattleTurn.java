package com.bg.discworld.battle;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;

import com.bg.discmud.core.MUD;
import com.bg.discworld.mobile.Mobile;

public class BattleTurn extends ArrayList<Mobile> {

	MUD mud;

	private static final long serialVersionUID = 1L;

	public int turn = -1;

	public BattleTurn(MUD mud, Mobile[] mobs) {
		this.mud = mud;
		for (Mobile m : mobs) {
			add(m);
		}
		determineOrder();
		for (Mobile m : mobs) {
			m.turn = getTurnByMob(m);
		}
	}

	public void determineOrder() {
		Collections.sort(this, new Comparator<Mobile>() {
			public int compare(Mobile o1, Mobile o2) {
				float co1 = o1.getTurnSpeed();
				float co2 = o2.getTurnSpeed();
				if (co1 == co2)
					return 0;
				return co1 > co2 ? -1 : 1;
			}
		});
	}

	public Mobile current() {
		return get(turn);
	}

	public Mobile next() {
		turn++;
		if (turn >= size()) {
			turn = 0;
		}
		return get(turn);
	}

	public int getTurnByMob(Mobile m) {
		for (int x = 0; x < size(); x++) {
			if (get(x) == m) {
				return x;
			}
		}
		return -1;
	}

	public String getString() {
		String s = "";
		int i = 0;
		for (i = turn; i < size(); i++) {
			Mobile m = get(i);
			s += getOrderString(i) + ". " + m.fields.get("name") + "\n";
		}
		for (i = 0; i < turn; i++) {
			Mobile m = get(i);
			s += getOrderString(i) + ". " + m.fields.get("name") + "\n";
		}
		return s;
	}

	public String getOrderString(int i) {
		String s = i + "";
		int single = 0;
		if (i > 9) {
			single = Integer.parseInt(s.split("")[1]);
		} else {
			single = Integer.parseInt(s);
		}

		switch (single) {
		case 0:
			s += "st";
			break;
		case 1:
			s += "nd";
			break;
		case 2:
			s += "rd";
			break;
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
			s += "th";
			break;
		}
		return s;
	}

}
