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
    public class TextStringFormat : MonoBehaviour
    {
        [Tooltip("Where to put the formatted text")]
        public UnityEngine.UI.Text OutputText;

        [Tooltip("First variable here is placed into {0} below, second into {1}, etc..")]
        public Object[] SoVariables;

        [TextArea]
        [Tooltip("Use string.Format style, with the {0}'s")]
        public string Format;

        [Tooltip("Resulting text")]
        public string Formatted;

        private void Start()
        {
            UpdateText();
        }

        private void OnValidate()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            var asISoVariable = SoVariables.Cast<SoArchitecture.ISoVariable>().ToArray();
            if (SoVariables.Count() != asISoVariable.Count())
            {
                Debug.LogError(this.name + "'s SoStringFormat.cs must have all SoVariables be derived from SoArchitecture.ISoVariable");
            }

            var formatArgs = asISoVariable.Select(v => v.GetType().GetField("Value").GetValue(v)).ToArray();
            Formatted = string.Format(Format, formatArgs);

            if (OutputText != null)
            {
                OutputText.text = Formatted;
            }
        }
    }
}
