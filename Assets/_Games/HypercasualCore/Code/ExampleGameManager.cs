using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleGameManager : IGameManager
{
#region Regular unity stuff

	public GameObject gameplayUI;
	public Button winButton;
	public Button looseButton;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

#endregion

#region GameManager Api

	public override void Init()
    {
		winButton.onClick.AddListener(Win);
		looseButton.onClick.AddListener(Loose);
    }
	
    public override void LoadLevel(int index)
    {
		gameplayUI.SetActive(false);
    }

    public override void ResetLevel()
    {
		gameplayUI.SetActive(false);
    }

    public override void StartLevel()
    {
		gameplayUI.SetActive(true);
    }

    public override void PauseLevel(bool pause)
    {
		gameplayUI.SetActive(!pause);
    }

#endregion
}
