using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelOverflow
{
	Clamp,
	Loop,
	Random
}

public class LevelSelector : MonoBehaviour
{
	public LevelOverflow overflow;

	public Level[] levels;

	public Level GetLevel(int index)
	{
		if (index < levels.Length)
			return levels[index];
		
		switch (overflow)
		{
			case LevelOverflow.Loop:
				return levels[index % levels.Length];
				
			case LevelOverflow.Random:
				return RandomUtils.Choice(levels);
				
			case LevelOverflow.Clamp:
			default:
				return levels[Mathf.Min(levels.Length-1, index)];
		}
		
	}
}