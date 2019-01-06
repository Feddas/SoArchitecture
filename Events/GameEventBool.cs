using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/Unite2017SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    [CreateAssetMenu(fileName = "GameEventBool", menuName = "SoArchitecture/GameEventBool")]
    public class GameEventBool : GameEventPayload<bool, GameEventListenerBool, SoArchitecture.BoolVariable>
    {
    }
}
