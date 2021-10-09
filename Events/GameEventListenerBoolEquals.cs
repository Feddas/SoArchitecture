using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoArchitecture
{
    /// <summary> Filter events based on payload matching EqualTo </summary>
    public class GameEventListenerBoolEquals : GameEventListenerBool
    {
        [Header("Filter")]
        [Tooltip("An event will be raised only when the payload of the event matches this value.")]
        public bool EqualTo = true;

        /// <summary> Event is raised only when the value of the payload matches MatchTarget </summary>
        public override void OnEventRaised(bool payload)
        {
            if (payload == EqualTo)
            {
                Response.Invoke(payload);
            }
        }
    }
}
