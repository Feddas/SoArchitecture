// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17 https://youtu.be/raQ3iHhE_Kk
// Modified by: Feddas
// ----------------------------------------------------------------------------

using System;
using UnityEngine;

namespace SoArchitecture
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "SoArchitecture/FloatVariable")]
    public class FloatVariable : SoVariableBase<float>
    {
        public void ApplyChange(float amount)
        {
            Value += amount;
        }

        public void ApplyChange(FloatVariable amount)
        {
            Value += amount.Value;
        }
    }
}