package com.bg.discmud.database;

import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.PBEKeySpec;

import com.bg.discworld.utility.Log;

import java.nio.charset.Charset;
import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.KeySpec;
import java.sql.*;
import java.util.Base64;
import java.util.LinkedList;
import java.util.List;
import java.util.UUID;

public class MySQL {

	public static final Integer DEFAULT_ITERATIONS = 10000;
	public static final String algorithm = "pbkdf2_sha256";
	public static Connection con;

	public static UUID uuid = UUID.randomUUID();

	public static void connectSQL() {
		try {
			Class.forName("com.mysql.cj.jdbc.Driver");
			con = DriverManager.getConnection("jdbc:mysql://18.223.190.165:3306/mud?useJDBCCompliantTimezoneShift=true&useLegacyDatetimeCode=false&serverTimezone=UTC&autoReconnect=true", "bear",
					"%Pb?fYW@ydP9RLqeTnfSW-u!23c$f=%#");
			Log.info("Connected to mySQL.");
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static ResultSet querySQL(String statement) {
		return querySQL(statement, null);
	}

	public static ResultSet query2(String statement) {
		try {
			PreparedStatement p = MySQL.con.prepareStatement(statement);

			return p.executeQuery();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return null;
	}

	//comment
	public static ResultSet querySQL(String statement, LinkedList<Object> objects) {
		try {
			PreparedStatement p = MySQL.con.prepareStatement(statement);

			int c = 1;
			if (objects != null) {
				for (Object o : objects) {
					if (o instanceof Integer) {
						p.setInt(c, (int) o);
					} else if (o instanceof String) {
						p.setString(c, (String) o);
					} else if (o instanceof Long) {
						p.setLong(c, (long) o);
					} else if (o instanceof Timestamp) {
						p.setTimestamp(c, (Timestamp) o);
					} else if (o instanceof Boolean) {
						p.setBoolean(c, (boolean) o);
					} else if (o instanceof Double) {
						p.setDouble(c, (double) o);
					} else {
						p.setNull(c, Types.CHAR);
					}
					c++;
				}
			}
			return p.executeQuery();
		} catch (Exception e) {
			e.printStackTrace();
		}
		return null;
	}

	public static void updateSQL(String statement) {
		save(statement, null);
	}

	public static void save(List<RawStatement> statements) {
		try {
			con.setAutoCommit(false);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		try {
			for (RawStatement r : statements) {
				save(r.statement, r.objects);
			}
			con.commit();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		try {
			con.setAutoCommit(true);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public static void save(String statement, LinkedList<Object> objects) {
		try {
			PreparedStatement p = MySQL.con.prepareStatement(statement);
			int c = 1;
			if (objects != null) {
				for (Object o : objects) {
					if (o instanceof Integer) {
						p.setInt(c, (int) o);
					} else if (o instanceof String) {
						p.setString(c, (String) o);
					} else if (o instanceof Long) {
						p.setLong(c, (long) o);
					} else if (o instanceof Timestamp) {
						p.setTimestamp(c, (Timestamp) o);
					} else if (o instanceof Boolean) {
						p.setBoolean(c, (boolean) o);
					} else if (o instanceof Double) {
						p.setDouble(c, (double) o);
					} else {
						p.setNull(c, Types.CHAR);
					}
					c++;
				}
			}
			p.executeUpdate();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static String getEncodedHash(String password, String salt, int iterations) {
		// Returns only the last part of whole encoded password
		SecretKeyFactory keyFactory = null;
		try {
			keyFactory = SecretKeyFactory.getInstance("PBKDF2WithHmacSHA256");
		} catch (NoSuchAlgorithmException e) {
			System.err.println("Could NOT retrieve PBKDF2WithHmacSHA256 algorithm");
			System.exit(1);
		}
		KeySpec keySpec = new PBEKeySpec(password.toCharArray(), salt.getBytes(Charset.forName("UTF-8")), iterations,
				256);
		SecretKey secret = null;
		try {
			secret = keyFactory.generateSecret(keySpec);
		} catch (InvalidKeySpecException e) {
			System.out.println("Could NOT generate secret key");
			e.printStackTrace();
		}

		byte[] rawHash = secret.getEncoded();
		byte[] hashBase64 = Base64.getEncoder().encode(rawHash);

		return new String(hashBase64);
	}

	public static String encode(String password, String salt, int iterations) {
		// returns hashed password, along with algorithm, number of iterations and salt
		String hash = getEncodedHash(password, salt, iterations);
		return String.format("%s$%d$%s$%s", algorithm, iterations, salt, hash);
	}

	public static String encode(String password, String salt) {
		return encode(password, salt, MySQL.DEFAULT_ITERATIONS);
	}

	public static boolean checkPassword(String password, String hashedPassword) {
		// hashedPassword consist of: ALGORITHM, ITERATIONS_NUMBER, SALT and
		// HASH; parts are joined with dollar character ("$")
		String[] parts = hashedPassword.split("\\$");
		if (parts.length != 4) {
			// wrong hash format
			return false;
		}
		Integer iterations = Integer.parseInt(parts[1]);
		String salt = parts[2];
		String hash = encode(password, salt, iterations);
		return hash.equals(hashedPassword);
	}

	public static String getUUID() {
		try {
			return java.util.UUID.randomUUID().toString().replace("-", "");
		} catch (Exception e) {
			e.printStackTrace();
		}
		return "";
	}
}