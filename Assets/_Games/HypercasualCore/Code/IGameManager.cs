using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
	event Action winLevel;
	event Action looseLevel;

	void LoadLevel(int index);
	
	void ResetLevel();
	
	void StartLevel();
	
	void PauseLevel(bool pause);
}
