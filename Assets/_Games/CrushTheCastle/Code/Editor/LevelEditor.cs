using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
	private Level _target;

	public void OnEnable()
	{
		_target = target as Level;
	}

	public override void OnInspectorGUI()
	{
		if(GUILayout.Button("Get Barrels"))
		{
			GetBarrels();
		}
		DrawDefaultInspector();
	}
	
	private void GetBarrels()
	{
		_target.barrels = _target.GetComponentsInChildren<Barrel>(true);
		EditorUtility.SetDirty(_target);
	}
}