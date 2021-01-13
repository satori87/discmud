package com.bg.discworld.player;

import java.sql.ResultSet;
import java.util.ArrayList;
import org.javacord.api.event.message.MessageCreateEvent;
import com.bg.discmud.core.MUD;
import com.bg.discmud.database.Model;
import com.bg.discmud.database.MySQL;
import com.bg.discworld.utility.Log;


public class QueryPlayerRunnable implements Runnable {

	MessageCreateEvent event;
	long id;

	public QueryPlayerRunnable(MessageCreateEvent event) {
		this.event = event;
		id = event.getMessageAuthor().getId();
	}

	public void run() {
		Player p = queryPlayer(id);
		if (p == null) {
			MUD.queryPlayerResults.add(new QueryPlayerResult(id, null, event));
		} else {
			p.fields.put("name", event.getMessageAuthor().getDisplayName());
			p.active = true;
			MUD.queryPlayerResults.add(new QueryPlayerResult(id, p, event));
		}
	}

	Player queryPlayer(long id) {
		try {
			String statement = "SELECT " + Model.playerModel + " FROM player WHERE id=?";
			ArrayList<Object> obj = new ArrayList<Object>();
			obj.add(id);
			ResultSet rs = MySQL.querySQL(statement, obj);
			Player p = null;
			int c;
			while (rs.next()) {
				p = new Player(MUD.mud, id);
				c = 1;
				String[] split = Model.playerModel.split(",");
				for (String s : split) {
					p.fields.put(s, rs.getObject(c));
					c++;
				}
				return p;
			}
		} catch (Exception e) {
			Log.debug(e);
		}
		return null;
	}

}
