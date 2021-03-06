// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Sets the Text used by the GUIText Component attached to a Game Object.")]
	#if UNITY_2017_2_OR_NEWER
	#pragma warning disable CS0618  
	[Obsolete("GUIText is part of the legacy UI system and will be removed in a future release")]
	#endif
	public class SetGUIText : ComponentAction<Text>
	{
		[RequiredField]
		[CheckForComponent(typeof(Text))]
		public FsmOwnerDefault gameObject;

        [UIHint(UIHint.TextArea)]
		public FsmString text;

		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			text = "";
		}

		public override void OnEnter()
		{
			DoSetGUIText();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}
		
		public override void OnUpdate()
		{
			DoSetGUIText();
		}
		
		void DoSetGUIText()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (UpdateCache(go))
		    {
		        guiText.text = text.Value;
		    }
		}
	}
}