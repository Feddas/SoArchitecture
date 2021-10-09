// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17 https://youtu.be/raQ3iHhE_Kk
// Modified by: Feddas
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace SoArchitecture
{
    [CustomEditor(typeof(GameEvent))]
    public class EditorSoEvent : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEvent e = target as GameEvent;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }

    [CustomEditor(typeof(GameEventBool))]
    public class EditorSoEventBool : EditorSoEventPayload<bool, GameEventBool>
    {
        protected override void customUi(GameEventBool e)
        {
            if (GUILayout.Button("Toggle & Raise"))
                e.Toggle();
        }
    }

    [CustomEditor(typeof(GameEventFloat))]
    public class EditorSoEventFloat : EditorSoEventPayload<float, GameEventFloat>
    {
    }

    [CustomEditor(typeof(GameEventInt))]
    public class EditorSoEventInt : EditorSoEventPayload<int, GameEventInt>
    {
        protected override void customUi(GameEventInt e)
        {
            if (GUILayout.Button("Increment & Raise"))
                e.Increment();
            if (GUILayout.Button("Decrement & Raise"))
                e.Decrement();
        }
    }

    public class EditorSoEventPayload<TPayload, TGameEvent> : Editor
        where TGameEvent : class, IGameEvent<TPayload>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            TGameEvent e = target as TGameEvent;
            if (GUILayout.Button("Raise"))
                e.Raise(e.LastPayload);

            customUi(e);

            GUILayout.Label("LastPayload: " + e.LastPayload.ToString());
        }

        /// <summary> Optionaly add custom Editor Ui. such as type specific buttons </summary>
        protected virtual void customUi(TGameEvent e) { }
    }
}
