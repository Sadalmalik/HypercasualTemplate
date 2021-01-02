using System;
using HutongGames.PlayMaker;

namespace ISelf.PlayMaker.Actions
{
	[ActionCategory("Custom")]
	[Tooltip("Check variables and broadcast changes")]
	public class SmartVarsCheckAction : FsmStateAction
	{
		public override void OnEnter()
		{
			SmartVarsManager.CheckVars();

			Finish();
		}
	}
}