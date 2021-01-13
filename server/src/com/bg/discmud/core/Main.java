package com.bg.discmud.core;

import com.bg.discworld.utility.Log;

public class Main {

	public static void main(String[] args) {
		try {
			MUD mud = new MUD();
			mud.start();
		} catch (Exception e) {
			Log.debug(e);
		}
	}

}
