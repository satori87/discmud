package com.bg.discmud.database;

import java.util.ArrayList;

public class RawStatement {

	public String statement = "";
	public ArrayList<Object> objects = new ArrayList<Object>();

	public RawStatement(String s, ArrayList<Object> o) {
		statement = s;
		objects = o;
	}

}
