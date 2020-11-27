using System;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

[Serializable]
public class SmartVariableBindSetup
{
	public string name;
	public FsmVar variable;
	public bool bindPrefs;
	public string prefsSpace;
	public bool bindAmplitude;

	public string prefsAddress => $"{prefsSpace}.{name}";
}

public class SmartVariableBind
{
	private SmartVariableBindSetup setup;
	private object _lastValue;

	public SmartVariableBind(SmartVariableBindSetup setup)
	{
		this.setup = setup;

		if (this.setup.bindPrefs)
		{
			var value = PrefsHelper.Get(
				setup.variable.Type,
				setup.prefsAddress,
				setup.variable.GetValue());
			setup.variable.SetValue(value);
			_lastValue = value;
		}
	}

	public void CheckChange()
	{
		var newValue = setup.variable.GetValue();
		Debug.Log($"[TEST] Compare {_lastValue} and {newValue}");
		if (_lastValue != newValue)
		{
			_lastValue = newValue;
			if (setup.bindPrefs)
			{
				PrefsHelper.Set(
					setup.variable.Type,
					setup.prefsAddress,
					setup.variable.GetValue());
			}

			if (setup.bindAmplitude)
			{
				switch (setup.variable.Type)
				{
					case VariableType.Int:
						Amplitude.Instance.setUserProperty(setup.name, (int) newValue);
						break;
					case VariableType.Float:
						Amplitude.Instance.setUserProperty(setup.name, (float) newValue);
						break;
					case VariableType.String:
						Amplitude.Instance.setUserProperty(setup.name, (string) newValue);
						break;
					case VariableType.Bool:
						Amplitude.Instance.setUserProperty(setup.name, (bool) newValue);
						break;
				}
			}
		}
	}
}

public static class SmartVarsManager
{
	public static List<SmartVariableBind> binds = new List<SmartVariableBind>();

	public static void Bind(SmartVariableBindSetup setup)
	{
		binds.Add(new SmartVariableBind(setup));
	}

	public static void CheckVars()
	{
		foreach (var bind in binds)
		{
			bind.CheckChange();
		}
	}
}