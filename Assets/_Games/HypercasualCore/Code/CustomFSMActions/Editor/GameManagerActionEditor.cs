using UnityEditor;
using UnityEngine;
using HutongGames.PlayMaker;
using HutongGames.PlayMakerEditor;


namespace ISelf.PlayMaker.Actions
{
	[CustomActionEditor(typeof(GameManagerAction))]
	public class GameManagerActionEditor : CustomActionEditor
	{
	    public override void OnEnable()
	    {
	    }

	    public override bool OnGUI()
	    {
	        var gameAction = target as GameManagerAction;

			EditorGUI.BeginChangeCheck();
			
			gameAction.action = (GameActionType)EditorGUILayout.EnumPopup("Action:", gameAction.action);

			switch (gameAction.action)
			{
				case GameActionType.Init:
					break;
				case GameActionType.LoadLevel:
					EditField("setLevel");
					break;
				case GameActionType.ResetLevel:
					break;
				case GameActionType.StartLevel:
					break;
				case GameActionType.PauseLevel:
					EditField("setPause");
					break;
				case GameActionType.ListenEvents:
			        EditField("onWinLevel");
			        EditField("onLooseLevel");
					break;
			}
	        
			return EditorGUI.EndChangeCheck();
	    }
	}
}