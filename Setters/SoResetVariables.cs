using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/Unite2017SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    public class SoResetVariables : MonoBehaviour
    {
        [Tooltip("Resets these ScriptableObject Variables to their StartingValue")]
        public Object[] SoVariables;

        void Start()
        {
            var asISoVariable = SoVariables.Cast<SoArchitecture.ISoVariable>().ToArray();
            if (SoVariables.Count() != asISoVariable.Count())
            {
                Debug.LogError(this.name + "'s SoStringFormat.cs must have all its SoVariables derived from SoArchitecture.ISoVariable");
            }

            foreach (var soVariable in asISoVariable)
            {
                soVariable.Reset();
            }
        }
    }
}