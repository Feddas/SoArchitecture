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
    public class GameEventListenerInt : MonoBehaviour, IGameEventListener<int>
    {
        [System.Serializable]
        public class UnityEventInt : UnityEvent<int> { }

        [Tooltip("Event to register with.")]
        public GameEventInt Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEventInt Response;

        [Tooltip("When the object is first enabled. Gets the last raised value for the event.")]
        public bool FetchOnEnable;

        private void OnEnable()
        {
            Event.RegisterListener(this);
            if (FetchOnEnable)
            {
                int payload = Event.BindTo != null ? Event.BindTo.Value : Event.LastPayload;
                OnEventRaised(payload);
            }
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(int payload)
        {
            Response.Invoke(payload);
        }
    }
}
