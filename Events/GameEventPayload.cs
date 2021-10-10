using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/SoArchitecture
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

        [TextArea]
        public string DeveloperDescription = "";

        /// <summary> Stores the last payload used when the event was raised </summary>
        public TPayload LastPayload { get; private set; }
        public TBind BindTo;

        #region [ PlayerPref sync ]
        [Tooltip("When true, Sync values to PlayerPrefs")]
        public bool IsPlayerPref;

        [Tooltip("On the first run of the app, what should the player pref value be")]
        public TPayload PlayerPrefDefault;

        protected string playPrefsKey { get { return "So" + this.name; } }
        protected System.Action<TPayload> playerPrefSet = null; // custom PlayerPref.Set should be defined in child classes to handle their TPayload
        protected System.Func<TPayload> playerPrefGet = null;
        #endregion [ PlayerPref sync ]

        public void OnEnable()
        {
#if UNITY_EDITOR
            // run only when playmode entered. not for every script recompile
            if (false == UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                return;
#endif

            hideFlags = HideFlags.DontUnloadUnusedAsset; // Make uneligible for Garbage collection, this allows the soObject to keep it's value even when not used in a scene. Even when not rooted in a scene. HideFlags set as per https://blogs.unity3d.com/2012/10/25/unity-serialization/

            LateAwake();
        }

        /// <summary> in Editor: run once every time entering playmode.
        /// in Builds: run once every time executable is opened </summary>
        public virtual void LateAwake()
        {
            if (IsPlayerPref && playerPrefGet != null)
            {
                SetPayload(playerPrefGet());
            }
        }

        /// <summary> Notify all subscribers that there is a new payload value for this event </summary>
        /// <param name="payload"></param>
        public virtual void Raise(TPayload payload)
        {
            // Update class members; LastPayload and BindTo
            SetPayload(payload);

            // Update event subscribers
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(payload);

            // Update PlayerPrefs
            if (IsPlayerPref && playerPrefSet != null)
            {
                playerPrefSet(payload);
            }
        }

        /// <summary> Sets value of LastPayload without raising an event. Typical usage would be to initalize an event's payload. </summary>
        public void SetPayload(TPayload payload)
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
        }

        public void RegisterListener(TListener listener)
        {
            if (false == eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(TListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }

        public override string ToString()
        {
            return LastPayload.ToString();
        }
    }
}
