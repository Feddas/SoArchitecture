using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/Unite2017SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    public interface ISoVariable
    {
#if UNITY_EDITOR
        System.Action EditorRepaint { get; set; }
#endif

        void Reset();
    }

    public interface ISoVariable<T>
    {
        void SetValue(T newValue);
    }

    /// <summary> Base class for ScriptableObject variables </summary>
    public class SoVariableBase<T> : ScriptableObject, ISoVariable<T>, ISoVariable
    {
#if UNITY_EDITOR
        public System.Action EditorRepaint { get; set; }
#endif
        [TextArea]
        public string DeveloperDescription = "";

        [Tooltip("Value is set to this once, when the application is first started. This is used so modifications during playmode are not persisted")]
        public T StartingValue; // alternate method is to have the real value not serialized, thus not saved in the scriptableobject or visible in the editor. details at http://www.roboryantron.com/2017/10/unite-2017-game-architecture-with.html?showComment=1515706263836#c3575015313096271234

        [Tooltip("The value accessed by FloatReference. Automatically overwritten by StartingValue when the application first starts.")]
        public T Value;

        public void OnEnable()
        {
            // Debug.Log("OnEnable SO called for " + this.name + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            hideFlags = HideFlags.DontUnloadUnusedAsset; // Make uneligible for Garbage collection, this allows the soObject to keep it's value even when not used in a scene. Even when not rooted in a scene. HideFlags set as per https://blogs.unity3d.com/2012/10/25/unity-serialization/
        }

        public void Reset()
        {
            SetValue(StartingValue);
        }

        public void SetValue(T value)
        {
            Value = value;
        }

        public void SetValue(SoVariableBase<T> value)
        {
            Value = value.Value;
        }
    }
}