using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    [CreateAssetMenu(fileName = "GameEventFloat", menuName = "SoArchitecture/GameEventFloat")]
    public class GameEventFloat : GameEventPayload<float, IGameEventListener<float>, SoArchitecture.FloatVariable>
    {
    }
}
