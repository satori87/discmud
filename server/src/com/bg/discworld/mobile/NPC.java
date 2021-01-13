package com.bg.discworld.mobile;

import com.bg.discmud.core.MUD;

public class NPC extends Mobile {

	public NPC(MUD mud, int uid) {
		super(mud, uid);
		active = true;
	}

}
