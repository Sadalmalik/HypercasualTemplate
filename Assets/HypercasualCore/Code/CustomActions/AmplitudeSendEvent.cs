using System;
using System.Collections.Generic;
using System.Linq;
using HutongGames.PlayMaker;

namespace ISelf.PlayMaker.Actions
{
    [Serializable]
    public class StringsPair
    {
        public FsmString key;
        public FsmVar value;
    }

    [ActionCategory("Custom")]
    [Tooltip("Sending analytic event")]
    public class AmplitudeSendEvent : FsmStateAction
    {
        public FsmString eventName;
        public StringsPair[] eventParams;

        public override void Reset()
        {
            eventName = "";
            eventParams = new StringsPair[]{};
        }

        public override void OnEnter()
        {
            if (!string.IsNullOrEmpty(eventName.Value))
            {
                var parameters = new Dictionary<string, object>();
                foreach (var p in eventParams)
                {
                    if (!string.IsNullOrEmpty(p.key.Value))
                        parameters.Add(p.key.Value, p.value.GetValue());
                }
                Amplitude.Instance.logEvent(eventName.Value, parameters);
            }

            Finish();
        }
    }
}
