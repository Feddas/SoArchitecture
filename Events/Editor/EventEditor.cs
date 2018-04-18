﻿// ----------------------------------------------------------------------------
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
    public class EventEditor : Editor
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
    public class EventBoolEditor : EventPayloadEditor<bool, GameEventBool>
    {
    }

    [CustomEditor(typeof(GameEventFloat))]
    public class EventFloatEditor : EventPayloadEditor<float, GameEventFloat>
    {
    }

    [CustomEditor(typeof(GameEventInt))]
    public class EventIntEditor : EventPayloadEditor<int, GameEventInt>
    {
    }

    public class EventPayloadEditor<TPayload, TGameEvent> : Editor
        where TGameEvent : class, IGameEvent<TPayload>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            TGameEvent e = target as TGameEvent;
            if (GUILayout.Button("Raise"))
                e.Raise(e.LastPayload);

            GUILayout.Label("LastPayload: " + e.LastPayload.ToString());
        }
    }
}