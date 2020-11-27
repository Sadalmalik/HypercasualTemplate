using System;
using HutongGames.PlayMaker;

namespace ISelf.PlayMaker.Actions
{
	[ActionCategory("Custom")]
	[Tooltip("Changing game screen")]
	public class ChangeScreenAction : FsmStateAction
	{
		public FsmString screenName;
		public bool instantChange;

		public override void Reset()
		{
			screenName    = "";
			instantChange = true;
		}

		public override void OnEnter()
		{
			UIScreensManager.Instance.SetScreen(screenName.Value, instantChange, Finish);
		}
	}
}