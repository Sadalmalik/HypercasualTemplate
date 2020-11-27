using System;
using HutongGames.PlayMaker;

namespace ISelf.PlayMaker.Actions
{
	[ActionCategory("Custom")]
	[Tooltip("Init amplitude")]
	public class AmplitudeInitAction : FsmStateAction
	{
		public FsmString ApiKey;

		public override void OnEnter()
		{
			var amp = Amplitude.Instance;
			amp.logging = true;
			amp.init(ApiKey.Value);

			Finish();
		}
	}
}