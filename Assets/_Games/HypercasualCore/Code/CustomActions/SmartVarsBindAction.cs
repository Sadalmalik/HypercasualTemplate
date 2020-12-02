using System;
using HutongGames.PlayMaker;

namespace ISelf.PlayMaker.Actions
{
	[ActionCategory("Custom")]
	[Tooltip("Bind variables connections")]
	public class SmartVarsBindAction : FsmStateAction
	{
		public SmartVariableBindSetup[] vars;

		public override void Reset()
		{
			vars = new SmartVariableBindSetup[] { };
		}

		public override void OnEnter()
		{
			foreach (var setup in vars)
			{
				SmartVarsManager.Bind(setup);
			}

			Finish();
		}
	}
}