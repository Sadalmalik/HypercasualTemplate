using System;
using System.Collections.Generic;
using UnityEngine;

public class IGameManager : MonoBehaviour
{
	public event Action OnWinLevel;
	public event Action OnLooseLevel;
    public event Action<float> OnProgress;

    protected void Win() => OnWinLevel?.Invoke();
    protected void Loose() => OnLooseLevel?.Invoke();
    protected void Progress(float progress) => OnProgress?.Invoke(progress);
    
	public virtual void Init() => throw new NotImplementedException("Init");
	public virtual void LoadLevel(int index) => throw new NotImplementedException("LoadLevel");
	public virtual void ResetLevel() => throw new NotImplementedException("ResetLevel");
	public virtual void StartLevel() => throw new NotImplementedException("StartLevel");
	public virtual void PauseLevel(bool pause) => throw new NotImplementedException("PauseLevel");
}
