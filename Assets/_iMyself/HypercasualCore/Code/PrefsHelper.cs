using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

public static class PrefsHelper
{
	public static object Get(VariableType type, string name, object defaultValue)
	{
		object result = null;
		switch (type)
		{
			case VariableType.Int:
				result = PlayerPrefs.GetInt(name, (int)defaultValue);
				break;
			case VariableType.Float:
				result = PlayerPrefs.GetFloat(name, (float)defaultValue);
				break;
			case VariableType.String:
				result = PlayerPrefs.GetString(name, (string)defaultValue);
				break;
			case VariableType.Bool:
				result = PlayerPrefs.GetInt(name, (bool)defaultValue ? 1 : 0) == 1;
				break;
		}
		return result;
	}
	
	public static void Set(VariableType type, string name, object value)
	{
		switch (type)
		{
			case VariableType.Int:
				PlayerPrefs.SetInt(name, (int)value);
				break;
			case VariableType.Float:
				PlayerPrefs.SetFloat(name, (float)value);
				break;
			case VariableType.String:
				PlayerPrefs.SetString(name, (string)value);
				break;
			case VariableType.Bool:
				PlayerPrefs.SetInt(name, (bool)value ? 1 : 0);
				break;
		}
	}
}
