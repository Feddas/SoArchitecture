using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/Unite2017SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    public interface IGameEvent<TPayload>
    {
        TPayload LastPayload { get; }

        void Raise(TPayload payload);
    }

    public interface IGameEventListener<TPayload>
    {
        void OnEventRaised(TPayload payload);
    }

    [System.Serializable]
    public abstract class GameEventPayload<TPayload, TListener, TBind> : ScriptableObject, IGameEvent<TPayload>
        where TListener : IGameEventListener<TPayload>
        where TBind : SoArchitecture.ISoVariable<TPayload>, ISoVariable
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<TListener> eventListeners =
            new List<TListener>();

        /// <summary> Stores the last payload used when the event was raised </summary>
        public TPayload LastPayload { get; private set; }
        public TBind BindTo;
        public virtual void Raise(TPayload payload)
        {
            if (BindTo != null)
            {
                // Update bound variable
                BindTo.SetValue(payload);

#if UNITY_EDITOR
                // Set UnityEditor inspector to update
                if (BindTo.EditorRepaint != null)
                {
                    BindTo.EditorRepaint();
                }
#endif
            }

            LastPayload = payload;
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(payload);
        }

        public void RegisterListener(TListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(TListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}
