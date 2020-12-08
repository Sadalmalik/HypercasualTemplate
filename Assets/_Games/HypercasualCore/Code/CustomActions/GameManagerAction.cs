using UnityEngine;
using HutongGames.PlayMaker;

namespace ISelf.PlayMaker.Actions
{
	public enum GameActionType
	{
		Init,
		LoadLevel,
		ResetLevel,
		StartLevel,
		PauseLevel
	}
	
	[ActionCategory("Custom")]
	[HutongGames.PlayMaker.Tooltip("Control game")]
	public class GameManagerAction : FsmStateAction
	{
		public GameActionType action;
		[Space]
		public FsmInt setLevel;
		[Space]
		public FsmBool setPause;
		[Space]
		public bool listenEvents;
		public FsmEvent onWinLevel;
		public FsmEvent onLooseLevel;

		public override void OnEnter()
		{
			if (listenEvents)
			{
				GameContainer.instance.gameManager.OnWinLevel   += HandleWin;
				GameContainer.instance.gameManager.OnLooseLevel += HandleLoose;
			}
			
			HandleAction();
			
			Finish();
		}

		public override void OnExit()
		{
			if (listenEvents)
			{
				GameContainer.instance.gameManager.OnWinLevel   -= HandleWin;
				GameContainer.instance.gameManager.OnLooseLevel -= HandleLoose;
			}
		}

		private void HandleWin() => Fsm.Event(onWinLevel);
		private void HandleLoose() => Fsm.Event(onLooseLevel);
		
		private void HandleAction()
		{
			switch (action)
			{
				case GameActionType.Init:
					GameContainer.instance.gameManager.Init();
					break;
				case GameActionType.LoadLevel:
					GameContainer.instance.gameManager.LoadLevel(setLevel.Value);
					break;
				case GameActionType.ResetLevel:
					GameContainer.instance.gameManager.ResetLevel();
					break;
				case GameActionType.StartLevel:
					GameContainer.instance.gameManager.StartLevel();
					break;
				case GameActionType.PauseLevel:
					GameContainer.instance.gameManager.PauseLevel(setPause.Value);
					break;
			}
	        
		}
	}
}