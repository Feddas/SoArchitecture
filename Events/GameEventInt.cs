using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/Unite2017SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    [CreateAssetMenu(fileName = "GameEventInt", menuName = "SoArchitecture/GameEventInt")]
    public class GameEventInt : GameEventPayload<int, GameEventListenerInt, SoArchitecture.IntVariable>
    {
    }
}
