package com.bg.discworld.utility;

import com.bg.discmud.core.MUD;
import com.bg.discmud.item.Item;
import com.bg.discworld.player.Player;

public class TextParser {
	
	public static String eraseNotation(String st) {
		try {
			if (st == null)
				return "";
			st = st.replaceAll("\\{\\{t}}", "");
			st = st.replaceAll("\\{\\{n}}", "");
			int indexOfOpen = st.indexOf("{{");
			int indexOfClose = 0;
			String everythingAfter = "";
			while (indexOfOpen >= 0) {
				indexOfClose = st.indexOf("}}", indexOfOpen + 2);
				st = st.substring(0, indexOfOpen);
				if (indexOfClose >= 0) {
					everythingAfter = st.substring(indexOfClose + 2);
					st += everythingAfter;
				} else {
					// no closing brace!? patch up st with everything but the '{{'
					st += st.substring(indexOfOpen + 2);
				}
				indexOfOpen = st.indexOf("{{");
			}
		} catch (Exception e) {
			Log.debug(e);
		}
		return st;
	}

	public static String eraseMetadata(String st) {
		try {
			String before = "";
			String after = "";
			int open = st.indexOf((char) 27);
			int close = 0;
			while (open >= 0) {
				close = st.indexOf("m", open + 2);
				before = st.substring(0, open);
				after = st.substring(close + 1);
				st = before + after;
				open = st.indexOf((char) 27);
			}
		} catch (Exception e) {
			Log.debug(e);
		}
		return st;
	}

	public static String replaceNotation(String st, boolean spans, Object o) {
		try {
			if (st == null)
				return "";
			st = st.replaceAll("\\{\\{t}}", "    ");
			st = st.replaceAll("\\{\\{n}}", "\n");

			int indexOfOpen = st.indexOf("{{");
			int indexOfClose = 0;
			String everythingBefore = "";
			String everythingAfter = "";
			String everythingMiddle = "";
			while (indexOfOpen >= 0) {
				indexOfClose = st.indexOf("}}", indexOfOpen + 2);
				everythingBefore = st.substring(0, indexOfOpen);
				if (indexOfClose >= 0) {
					everythingMiddle = st.substring(indexOfOpen + 2, indexOfClose);
					everythingAfter = st.substring(indexOfClose + 2);

					everythingMiddle = parse(everythingMiddle, o);
					st = everythingBefore + everythingMiddle + everythingAfter;

				} else {
					// no closing brace!? patch up st with everything but the '{{'
					st = everythingBefore + st.substring(indexOfOpen + 2);
				}
				indexOfOpen = st.indexOf("{{");
			}
			// now iterate through and find the spans

			return st;
		} catch (Exception e) {
			Log.debug(e);
		}
		return st;
	}

	public static String replace(String st) {
		return replaceNotation(st, true, null);
	}

	public static String cleanSpans(String s) {
		return s;
	}

	public static String parse(String entry, Object object) {
		try {
			String property = "";
			String value = "";
			String[] statements = entry.split(";");
			int stotal = statements.length;
			int scount = 0;
			String sub = "";
			int width = 0;
			int align = 0; // -1,0,1
			String[] splitstatement = null;
			for (String statement : statements) {
				int indexOfColon = statement.indexOf(":");
				if (indexOfColon > 0) {
					splitstatement = statement.split(":");
					property = splitstatement[0];
					value = splitstatement[1];
					switch (property.toLowerCase()) {
					case "player":
						if (object != null && object instanceof Player) {
							Player p = (Player) object;
							String fieldName = value.toLowerCase();
							if (allowedPlayerFields(fieldName)) {
								if (p.fields.containsKey(fieldName)) {
									sub = p.fields.get(fieldName).toString();
								} else {
									sub = checkPlayerNotation(p, fieldName);
								}
								scount++;
								if (stotal < 2) {
									return sub;
								}
							} else {
								return "";
							}
						} else {
							return "";
						}
						break;
					case "item":
						if (object != null && object instanceof Item) {
							Item item = (Item) object;
							String fieldName = value.toLowerCase();
							if (item.fields.containsKey(fieldName)) {
								sub = item.fields.get(fieldName).toString();
							} else {
								sub = checkItemNotation(item, fieldName);
							}
							scount++;
							if (stotal < 2) {
								return sub;
							}

						} else {
							return "";
						}
						break;
					case "width":
						scount++;
						width = Integer.parseInt(value);
						if (scount >= stotal) {
							return formattedText(sub, width, align);
						}
						break;
					case "align":
						scount++;
						if (value.equalsIgnoreCase("center")) {
							align = 0;
						} else if (value.equalsIgnoreCase("left")) {
							align = -1;
						} else if (value.equalsIgnoreCase("right")) {
							align = 1;
						}
						if (scount >= stotal) {
							return formattedText(sub, width, align);
						}
						break;
					case "msg":
						return MUD.messages.get(value);
					default:
						return ""; // invalid tag with colon, make it INVISIBLE
					}
				} else {
					switch (statement.toLowerCase()) {
					case "t":
						return "    ";
					case "n":
						return "\n";
					default:
						return ""; // invalid tag without colon, make it INVISIBLE
					}
				}
			}
		} catch (Exception e) {
			Log.debug(e);
		}
		return entry;
	}

	public static String formatNumber(int n, int width) {
		String st = "" + n;
		try {
			int diff = width - st.length();
			if (diff < 0) {
				return st;
			}
			for (int i = 0; i < diff; i++) {
				st = "0" + st;
			}
		} catch (Exception e) {
			Log.debug(e);
		}
		return st;
	}
	
	public static String formattedText(String st, int width, int align) {
		String f = "";
		boolean odd = false;
		try {
			int diff = width - st.length();
			if (diff < 0) {
				return st;
			}
			if (align < 0) { // left align
				for (int i = 0; i < diff; i++) {
					f += " ";
				}
				return st + f;
			} else if (align > 0) { // right align
				for (int i = 0; i < diff; i++) {
					f += " ";
				}
				return f + st;
			} else { // centered
				if (diff % 2 == 1) { // odd
					odd = true;
				}
				diff /= 2;
				for (int i = 0; i < diff; i++) {
					f += " ";
				}
				st = f + st + f;
				if (odd) {
					st += " ";
				}
				return st;
			}
		} catch (Exception e) {
			Log.debug(e);
		}
		return st;
	}

	public static String checkPlayerNotation(Player p, String fieldName) {
		switch (fieldName.toLowerCase()) {
		case "eggs":
			return "bacon";
		default:
			return "";
		}
	}

	public static String checkItemNotation(Item i, String fieldName) {
		switch (fieldName.toLowerCase()) {
		case "qty":
			return i.qty + "";
		case "weight":
			return i.getWeight() + " kg";
		default:
			return "";
		}
	}

	public static boolean allowedPlayerFields(String name) {
		return (Player.blockedFieldStr.indexOf(name) < 0);
	}

}
