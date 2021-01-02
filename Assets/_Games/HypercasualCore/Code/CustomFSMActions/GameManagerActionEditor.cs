// using UnityEditor;
// using HutongGames.PlayMakerEditor;
//
// namespace ISelf.PlayMaker.Actions
// {
// 	[CustomActionEditor(typeof(GameManagerAction))]
// 	public class GameManagerActionEditor : CustomActionEditor
// 	{
// 	    public override void OnEnable()
// 	    {
// 	    }
//
// 	    public override bool OnGUI()
// 	    {
// 	        var gameAction = target as GameManagerAction;
//
// 			EditorGUI.BeginChangeCheck();
// 			
// 			gameAction.action = (GameActionType)EditorGUILayout.EnumPopup(gameAction.action);
//
// 			switch (gameAction.action)
// 			{
// 				case GameActionType.Init:
// 					break;
// 				case GameActionType.LoadLevel:
// 					EditField("setLevel");
// 					break;
// 				case GameActionType.ResetLevel:
// 					break;
// 				case GameActionType.StartLevel:
// 					break;
// 				case GameActionType.PauseLevel:
// 					EditField("setPause");
// 					break;
// 			}
// 	        
// 			gameAction.listenEvents = EditorGUILayout.Toggle("Listen game events:", gameAction.listenEvents);
// 	        
// 	        if (gameAction.listenEvents)
// 	        {
// 		        EditField("onWinLevel");
// 		        EditField("onLooseLevel");
// 	        }
// 	        
// 			return EditorGUI.EndChangeCheck();
// 	    }
// 	}
// }