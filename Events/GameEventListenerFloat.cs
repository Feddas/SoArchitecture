using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/Unite2017SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    public class GameEventListenerFloat : MonoBehaviour, IGameEventListener<float>
    {
        [System.Serializable]
        public class UnityEventFloat : UnityEvent<float> { }

        [Tooltip("Event to register with.")]
        public GameEventFloat Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEventFloat Response;

        [Tooltip("When the object is first enabled. Gets the last raised value for the event.")]
        public bool FetchOnEnable;

        private void OnEnable()
        {
            Event.RegisterListener(this);
            if (FetchOnEnable)
            {
                float payload = Event.BindTo != null ? Event.BindTo.Value : Event.LastPayload;
                OnEventRaised(payload);
            }
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(float payload)
        {
            Response.Invoke(payload);
        }
    }
}
