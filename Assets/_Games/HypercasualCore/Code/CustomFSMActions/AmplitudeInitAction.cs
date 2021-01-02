using System;
using HutongGames.PlayMaker;

namespace ISelf.PlayMaker.Actions
{
	[ActionCategory("Custom")]
	[Tooltip("Init Amplitude")]
	public class AmplitudeInitAction : FsmStateAction
	{
		public FsmString ApiKey;
		public FsmBool logging;

		public override void OnEnter()
		{
			var amp = Amplitude.Instance;
			amp.logging = logging.Value;
			amp.init(ApiKey.Value);

			Finish();
		}
	}
}