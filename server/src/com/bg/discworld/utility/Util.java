package com.bg.discworld.utility;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.security.MessageDigest;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.List;
import java.util.Random;
import java.util.zip.DeflaterOutputStream;
import java.util.zip.InflaterInputStream;

import com.esotericsoftware.kryo.Kryo;
import com.esotericsoftware.kryo.io.Input;
import com.esotericsoftware.kryo.io.Output;
import com.fasterxml.jackson.databind.ObjectMapper;

public class Util {

	public class Coord {

		public int x, y;

		public Coord(int x, int y) {
			this.x = x;
			this.y = y;
		}

	}

	public static double distance(int x1, int y1, int x2, int y2) {
		return Math.sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
	}

	public static double distance(float x1, float y1, float x2, float y2) {
		return Math.sqrt((y2 - y1) * (y2 - y1) + (x2 - x1) * (x2 - x1));
	}

	public static void exportJSON(String filename, Object o) {
		try {
			ObjectMapper mapper = new ObjectMapper();
			writeFile(filename, mapper.writeValueAsString(o));
		} catch (Exception e) {
			Log.error(e);
		}
	}

	public static boolean exists(String filename) {
		File file = new File(filename);
		return file.exists();
	}

	@SuppressWarnings({ "rawtypes", "unchecked" })
	public static Object importJSON(String filename, Class c) {
		String json = readFile(filename);
		if (json.length() > 0) {
			ObjectMapper mapper = new ObjectMapper();
			try {
				return mapper.readValue(json, c);
			} catch (Exception e) {
				Log.error(e);
			}
		}
		return null;
	}

	public static void writeFile(String filename, String s) {
		try {
			Files.write(Paths.get(filename), s.getBytes());
		} catch (IOException e) {
			Log.error(e);
		}
	}

	public static String readFile(String filename) {
		String s = "";
		try {
			s = new String(Files.readAllBytes(Paths.get(filename)));
		} catch (Exception e) {
			Log.warn(filename + " not found for reading");
		}
		return s;
	}

	public static String encryptPassword(String password) {
		try {
			MessageDigest messageDigest;
			messageDigest = MessageDigest.getInstance("SHA-256");
			messageDigest.update(password.getBytes());
			return new String(messageDigest.digest());
		} catch (Exception e) {
			Log.error(e);

		}
		return "";
	}

	public static int rndInt(int min, int max) {
		return randInt(min, max);
	}

	static Random rand = new Random(System.currentTimeMillis() / 7);

	public static int randInt(int min, int max) {

		// NOTE: This will (intentionally) not run as written so that folks
		// copy-pasting have to think about how to initialize their
		// Random instance. Initialization of the Random instance is outside
		// the main scope of the question, but some decent options are to have
		// a field that is initialized once and then re-used as needed or to
		// use ThreadLocalRandom (if using at least Java 1.7).
		//
		// In particular, do NOT do 'Random rand = new Random()' here or you
		// will get not very good / not very random results.

		// nextInt is normally exclusive of the top value,
		// so add 1 to make it inclusive
		int randomNum = rand.nextInt((max - min) + 1) + min;

		return randomNum;
	}

	public static String getDate(String pattern) {
		SimpleDateFormat simpleDateFormat = new SimpleDateFormat(pattern);
		return simpleDateFormat.format(new Date());
	}

	@SuppressWarnings({ "rawtypes", "unchecked" })
	public static Object deserializeB(byte[] data, Class c) {
		Object object = null;
		try {
			ByteArrayInputStream bais = new ByteArrayInputStream(data);
			ObjectInputStream ois = new ObjectInputStream(bais);
			InflaterInputStream is = new InflaterInputStream(ois);
			Input input = new Input(is);
			Kryo kryo = new Kryo();
			object = kryo.readObject(input, c);
			input.close();
		} catch (Exception e) {
			Log.error(e);
		}
		return object;
	}

	public static String serialize(Object object) {
		return new String(serializeB(object), StandardCharsets.UTF_8);
	}

	@SuppressWarnings("rawtypes")
	public static Object deserialize(String s, Class c) {
		return deserializeB(s.getBytes(StandardCharsets.UTF_8), c);
	}

	public static byte[] serializeB(Object object) {
		try {
			ByteArrayOutputStream baos = new ByteArrayOutputStream();
			ObjectOutputStream oos = new ObjectOutputStream(baos);
			DeflaterOutputStream os = new DeflaterOutputStream(oos);
			Output output = new Output(os);
			Kryo kryo = new Kryo();
			kryo.writeObject(output, object);
			output.close();
			return baos.toByteArray();
		} catch (Exception e) {
			Log.error(e);
		}
		return null;
	}

	public static byte[][] divideArray(byte[] source, int chunksize) {
		byte[][] ret = new byte[(int) Math.ceil(source.length / (double) chunksize)][chunksize];
		try {
			int start = 0;
			for (int i = 0; i < ret.length; i++) {
				ret[i] = Arrays.copyOfRange(source, start, start + chunksize);
				start += chunksize;
			}
		} catch (Exception e) {
			Log.error(e);
		}
		return ret;
	}

	static public boolean inCenteredBox(int x, int y, int centerX, int centerY, int width, int height) {
		int topY = centerY - (height / 2);
		int bottomY = centerY + (height / 2);
		int leftX = centerX - (width / 2);
		int rightX = centerX + (width / 2);
		if (x > leftX && x < rightX && y > topY && y < bottomY) {
			return true;
		}
		return false;
	}

	static public boolean inBox(int x, int y, int lowerX, int upperX, int lowerY, int upperY) {
		return (x >= lowerX && x < upperX && y >= lowerY && y < upperY);
	}

	public static int getDir(int fx, int fy, int tx, int ty) {
		if (ty < fy) {
			return 0;
		} else if (ty > fy) {
			return 1;
		} else if (tx < fx) {
			return 2;
		} else if (tx > fx) {
			return 3;
		}
		return 0;
	}

	public static int reverseDir(int d) {
		int n = d;
		if (d == 0) {
			n = 1;
		} else if (d == 1) {
			n = 0;
		} else if (d == 2) {
			n = 3;
		} else if (d == 3) {
			n = 2;
		}
		return n;
	}

	public static void assureDir(String s) {
		File directory = new File(s);
		if (!directory.exists()) {
			directory.mkdir();
		}
	}

	public static int setBit(int value, int bit) {
		// Create mask
		int mask = 1 << bit;
		// Set bit
		return value | mask;
	}

	public static int clearBit(int x, int kth) {
		return (x & ~(1 << kth));
	}

	public static boolean checkBit(int value, int bit) {
		return ((value >> bit) & 1) != 0;
	}

	public static int setBit(int value, int bit, boolean to) {
		if (to) {
			return setBit(value, bit);
		} else {
			return clearBit(value, bit);
		}
	}

	public static String[] splitForDiscord(String in) {
		String[] lines = in.split("\n");
		List<String> out = new ArrayList<String>();
		String currentLine = "";
		for (String line : lines) {
			if (line.length() < 2000) {
				if (line.length() + currentLine.length() < 2000) {
					if (currentLine.length() == 0) {
						currentLine = line;
					} else {
						currentLine += "\n" + line;
					}
				} else {
					out.add(currentLine);
					currentLine = line;
				}
			} else {
				// just dont do it!
			}
		}
		out.add(currentLine);
		return out.stream().toArray(String[]::new);
	}

}
