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
    public class GameEventInt : GameEventPayload<int, IGameEventListener<int>, IntVariable>
    {
        public override void LateAwake()
        {
            // Ensure base.LateAwake can leverage PlayerPrefs
            playerPrefGet = () => PlayerPrefs.GetInt(playPrefsKey, PlayerPrefDefault);
            playerPrefSet = (payload) => PlayerPrefs.SetInt(playPrefsKey, payload);

            base.LateAwake();
        }

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
