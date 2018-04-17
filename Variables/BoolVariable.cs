using System;
using UnityEngine;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/Unite2017SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "SoArchitecture/BoolVariable")]
    public class BoolVariable : SoVariableBase<bool>
    {
        public void Toggle()
        {
            Value = Value == false;
        }
    }
}