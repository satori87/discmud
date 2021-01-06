package com.bg.discmud.core;

public class Main {

	public static void main(String[] args) {
		try {
			MUD mud = new MUD();
			mud.start();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

}
