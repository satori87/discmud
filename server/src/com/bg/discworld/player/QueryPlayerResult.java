package com.bg.discworld.player;

import org.javacord.api.event.message.MessageCreateEvent;

public class QueryPlayerResult {
	public long id;
	public Player player;
	public MessageCreateEvent event;
	public boolean create = false;

	public QueryPlayerResult(long id, Player player, MessageCreateEvent event) {
		this.id = id;
		this.player = player;
		this.event = event;
	}
}
