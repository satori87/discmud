package com.bg.discworld.utility;

import javax.script.Invocable;
import javax.script.ScriptEngine;
import javax.script.ScriptEngineManager;
import javax.script.ScriptException;
import java.util.HashMap;
import java.util.Map;

public class Scripter {

	static ScriptEngineManager manager = new ScriptEngineManager();
	static ScriptEngine engine = manager.getEngineByName("JavaScript");
	
	public static Object runScript(String script, String function, Map<String, Object> fields,
			Object[] args) {
		try {
			for (String s : fields.keySet()) {
				engine.put(s, fields.get(s));
			}
			engine.eval(script);
			Map<Integer, Object> arg = new HashMap<>();
			int i = 0;
			for(Object o : args) {
				arg.put(i, o);
				i++;
			}
			Invocable inv = (Invocable) engine;

				return inv.invokeFunction(function, arg);
			} catch (NoSuchMethodException e) {
				return true;
		} catch (ScriptException e) {
			Log.debug(e);
				return true;				
			}
		//return null;
	}

}
