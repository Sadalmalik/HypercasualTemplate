using System;
using HutongGames.PlayMaker;

namespace ISelf.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Changing game screen")]
    public class ChangeScreenAction : FsmStateAction
    {
        public FsmString screenName;
        public bool instantChange;
        public FsmEvent finishEvent;

        public override void Reset()
        {
            screenName = "";
            instantChange = true;
        }

        public override void OnEnter()
        {
            UIScreensManager.Instance.SetScreen(screenName.Value, instantChange, SendEvent);
        }
        
        private void SendEvent()
        {
            Fsm.Event(finishEvent);
        }
    }
}
