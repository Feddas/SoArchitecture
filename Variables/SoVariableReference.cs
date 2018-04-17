using System;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/Unite2017SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    [Serializable]
    public class BoolReference : SoVariableReference<bool, BoolVariable>
    {
        public static implicit operator bool(BoolReference reference)
        {
            return reference.Value;
        }
    }

    [Serializable]
    public class FloatReference : SoVariableReference<float, FloatVariable>
    {
        public static implicit operator float(FloatReference reference)
        {
            return reference.Value;
        }
    }

    [Serializable]
    public class IntReference : SoVariableReference<int, IntVariable>
    {
        public static implicit operator int(IntReference reference)
        {
            return reference.Value;
        }
    }

    [Serializable]
    public class StringReference : SoVariableReference<string, StringVariable>
    {
        public static implicit operator string(StringReference reference)
        {
            return reference.Value;
        }
    }

    /// <summary>
    /// For the inspector value to be updated a GameEventPayloadType needs to be raised.
    /// That raising is what dirties the UI.
    /// </summary>
    [System.Serializable]
    public class SoVariableReference<T, TVariable>
        where TVariable : SoVariableBase<T>
    {
        public T StartingValue
        {
            get
            {
                return UseConstant ? startingConstant : Variable.StartingValue;
            }
        }

        public bool UseConstant = false;
        public T ConstantValue;
        public TVariable Variable; // Do not replace with SoVariableBase<T>. Sadily doing that would break the PropertyDrawer since generic types can't be serialized as needed for property.FindPropertyRelative("Variable");

        private T startingConstant;

        public SoVariableReference()
        { }

        public SoVariableReference(T value)
        {
            UseConstant = true;
            ConstantValue = value;
            startingConstant = ConstantValue;
        }

        public T Value
        {
            get
            {
                // Can't use a scriptable object if it hasn't been set, silently fall back to using a constant
                if (UseConstant == false && Variable == null)
                    UseConstant = true;

                return UseConstant ? ConstantValue : Variable.Value;
            }
            set
            {
                if (UseConstant)
                {
                    ConstantValue = value;
                }
                else
                {
                    Variable.Value = value;
                }
            }
        }
    }
}