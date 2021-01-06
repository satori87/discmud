package com.bg.discworld.utility;

import java.awt.*;
import java.util.ArrayList;

import com.bg.discworld.mobile.Mobile;
import com.bg.discworld.player.Player;

public class Node {

	private Node parent = null;
	public ArrayList<Node> neighbors = new ArrayList<Node>();
	private double cost, heuristic, function;
	private boolean valid;

	public int x, y;

	public double weight = 1.0;
	public double virtual_weight = 1.0; // for weight considerations that dont effect path-finding directly

	public Mobile mob = null;
	public int tile = 0;

	public static int TILE_SIZE = 12;

	public boolean blocked = false;

	public Node(int x, int y, int t) {
		this.x = x;
		this.y = y;
		tile = t;
		setValid(true);
	}

	public int getX() {
		return x;
	}

	public int getY() {
		return y;
	}

	public void calculateNeighbours(Network network) {

		Grid grid = (Grid) network;

		ArrayList<Node> nodes = new ArrayList<>();

		Node t = null;

		for (int p = -1; p < 2; p++) {
			for (int q = -1; q < 2; q++) {
				if (!(p == 0 && q == 0))
					t = grid.find(x + p, y + q);
				if (t != null && t.blocked == false)
					nodes.add(t);
			}
		}

		setNeighbours(nodes);

	}

	public double heuristic(Node dest, boolean vWeight) {
		return distanceTo(dest, vWeight);
	}

	public double distanceTo(Node dest, boolean vWeight) {
		Node d = (Node) dest;
		double dist = new Point(x, y).distance(new Point(d.x, d.y));
		if (dist > 1.0) {
			dist = 1.4 * weight;
		} else {
			dist = 1.0 * weight;
		}
		return dist;
	}

	public String getString(Player p) {
		if (mob == null) {
			return getTileString(tile);
		} else {
			return " " + ((p.ally == mob.ally) ? "{{fg:cyan}}" : "{{fg:red}}") + (mob.turn + 1) + "{{fg:default}} ";
		}
	}

	public String getTileString(int t) {
		switch (t) {
		case 1:
			return "/|\\";
		case 2:
			return "###";
		}
		return "   ";
	}

	public double getCost() {
		return cost;
	}

	public void setCost(double cost) {
		this.cost = cost;
	}

	public double getHeuristic() {
		return heuristic;
	}

	public void setHeuristic(double heuristic) {
		this.heuristic = heuristic;
	}

	public double getFunction() {
		return function;
	}

	public void setFunction(double function) {
		this.function = function;
	}

	public ArrayList<Node> getNeighbours() {
		return neighbors;
	}

	public void setNeighbours(ArrayList<Node> neighbours) {
		this.neighbors = neighbours;
	}

	public Node getParent() {
		return parent;
	}

	public void setParent(Node parent) {
		this.parent = parent;
	}

	public boolean isValid() {
		return valid;
	}

	public void setValid(boolean valid) {
		this.valid = valid;
	}

	public void reverseValidation() {
		valid = !valid;
	}

	public boolean adjacentTo(int tx, int ty) {
		return (Math.abs(tx - x) < 2 && Math.abs(ty - y) < 2 && !(tx == x && ty == y));
	}
}
