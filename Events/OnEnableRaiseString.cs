using UnityEngine;
using UnityEngine.Events;

namespace SoArchitecture
{
    public class OnEnableRaiseString : MonoBehaviour
    {
        [System.Serializable]
        public class UnityEventString : UnityEvent<string> { }

        [Tooltip("This variable is sent as the payload when OnEnable occurs")]
        public StringVariable Variable;

        public UnityEventString OnEnableEvent;

        public void OnEnable()
        {
            OnEnableEvent.Invoke(Variable.Value);
        }
    }
}
