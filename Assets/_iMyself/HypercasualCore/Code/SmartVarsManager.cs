using System;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

[Serializable]
public class SmartVariableBindSetup
{
	public FsmVar variable;
	public bool bindPrefs;
	public string prefsSpace;
	public bool bindAmplitude;

	public string prefsAddress => $"{prefsSpace}.{variable.variableName}";
}

public class SmartVariableBind
{
	private SmartVariableBindSetup _setup;
	private object _lastValue;

	public SmartVariableBind(SmartVariableBindSetup setup)
	{
		_setup = setup;

		if (_setup.bindPrefs)
		{
			var value = PrefsHelper.Get(
				setup.variable.Type,
				setup.prefsAddress,
				setup.variable.GetValue());
			setup.variable.SetValue(value);
			_lastValue = value;
		}

		if (_setup.bindAmplitude)
		{
			ProvideAmplitudeProperty(
				setup.variable.Type,
				setup.variable.variableName,
				setup.variable.GetValue());
		}
	}

	private object GetVariableValue(NamedVariable variable)
	{
		switch (variable.VariableType)
		{
			case VariableType.Int:
				return (variable as FsmInt)?.Value;
			case VariableType.Float:
				return (variable as FsmFloat)?.Value;
			case VariableType.String:
				return (variable as FsmString)?.Value;
			case VariableType.Bool:
				return (variable as FsmBool)?.Value;
		}

		return null;
	}

	public void CheckChange()
	{
		var variable = _setup.variable.NamedVar;
		var newValue = GetVariableValue(variable);
		if (_lastValue != newValue)
		{
			_lastValue = newValue;
			if (_setup.bindPrefs)
			{
				PrefsHelper.Set(
					variable.VariableType,
					_setup.prefsAddress,
					newValue);
			}

			if (_setup.bindAmplitude)
			{
				ProvideAmplitudeProperty(
					variable.VariableType,
					variable.Name,
					newValue);
			}
		}
	}

	private void ProvideAmplitudeProperty(VariableType type, string name, object value)
	{
		switch (type)
		{
			case VariableType.Int:
				Amplitude.Instance.setUserProperty(name, (int) value);
				break;
			case VariableType.Float:
				Amplitude.Instance.setUserProperty(name, (float) value);
				break;
			case VariableType.String:
				Amplitude.Instance.setUserProperty(name, (string) value);
				break;
			case VariableType.Bool:
				Amplitude.Instance.setUserProperty(name, (bool) value);
				break;
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