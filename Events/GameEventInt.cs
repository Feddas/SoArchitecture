using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    [CreateAssetMenu(fileName = "GameEventInt", menuName = "SoArchitecture/GameEventInt")]
    public class GameEventInt : GameEventPayload<int, IGameEventListener<int>, SoArchitecture.IntVariable>
    {
        [ContextMenu("Increment then Raise Event")]
        public void Increment()
        {
            this.Raise(LastPayload + 1);
        }

        [ContextMenu("Decrement then Raise Event")]
        public void Decrement()
        {
            this.Raise(LastPayload - 1);
        }
    }
}
