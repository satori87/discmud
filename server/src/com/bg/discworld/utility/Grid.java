package com.bg.discworld.utility;

import java.util.ArrayList;
public class Grid extends Network {

	private int width, height;
	private ArrayList<Node> tiles;

	public Grid(int width, int height, ArrayList<Node> tiles) {
		this.width = width;
		this.height = height;
		this.tiles = tiles;
	}

	public int getWidth() {
		return width;
	}

	public int getHeight() {
		return height;
	}

	public ArrayList<Node> getTiles() {
		return tiles;
	}

	public Node find(int x, int y) {
		int minX = 0;
		int minY = 0;
		int maxX = getWidth();
		int maxY = getHeight();
		if (x >= minX && x < maxX && y >= minY && y < maxY) {
			for (Node t : tiles) {
				if (t.getX() == x && t.getY() == y)
					return t;
			}
		}
		return null;
	}

	@Override
	public Iterable<Node> getNodes() {
		ArrayList<Node> nodes = new ArrayList<>();
		nodes.addAll(tiles);
		return nodes;
	}
}
