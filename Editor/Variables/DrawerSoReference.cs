using UnityEditor;
using UnityEngine;

/// <summary>
/// Scriptable Objects fork https://github.com/Feddas/Unite2017SoArchitecture
/// Author: Shawn Featherly in 2018
/// </summary>
namespace SoArchitecture
{
    [CustomPropertyDrawer(typeof(BoolReference))]
    [CustomPropertyDrawer(typeof(FloatReference))]
    [CustomPropertyDrawer(typeof(IntReference))]
    [CustomPropertyDrawer(typeof(StringReference))]
    public class DrawerSoReference : PropertyDrawer
    {
        /// <summary>
        /// Options to display in the popup to select constant or variable.
        /// </summary>
        private readonly string[] popupOptions =
            { "Use Constant", "Use Variable" };

        /// <summary> Cached style to use to draw the popup button. </summary>
        private GUIStyle popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            // Get properties
            SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
            SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");
            SerializedProperty variable = property.FindPropertyRelative("Variable");

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);

            useConstant.boolValue = result == 0;

            if (useConstant.boolValue)
            {
                EditorGUI.PropertyField(position, constantValue, GUIContent.none);
            }
            else
            {
                // show variable value
                showVariable(useConstant.boolValue, position, variable);

                // field to show and modify variable
                var rectSoVariable = new Rect(position);
                rectSoVariable.xMin += 32f;
                EditorGUI.PropertyField(rectSoVariable, variable, GUIContent.none);
            }

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        /// <summary>
        /// Shows the current value of the variable
        /// </summary>
        void showVariable(bool isSkipped, Rect position, SerializedProperty property)
        {
            if (isSkipped || property.objectReferenceValue == null)
                return;

            // get the variables value
            var variableValue = watchValue(property);

            // set the position
            var rectVariableValue = new Rect(position);
            rectVariableValue.width = 30f;

            // display the variable
            EditorGUI.PropertyField(rectVariableValue, variableValue, GUIContent.none);
        }

        SerializedProperty watchValue(SerializedProperty property)
        {
            // get the value of the variable https://answers.unity.com/questions/629803/findrelativeproperty-never-worked-for-me-how-does.html & http://sketchyventures.com/2015/08/07/unity-tip-getting-the-actual-object-from-a-custom-property-drawer/
            var asSerializedObject = new SerializedObject(property.objectReferenceValue);
            var objectInstance = asSerializedObject.targetObject as ISoVariable;

            // If the repaint callback hasn't been set, set it
            if (objectInstance.EditorRepaint == null)
            {
                var target = property.serializedObject.targetObject; // https://answers.unity.com/questions/795693/how-can-one-get-the-actual-drawn-type-instance-fro.html
                objectInstance.EditorRepaint = () =>
                {
                    if (target != null)
                    {
                        // Debug.Log("objectInstance.EditorRepaint target was destroyed");
                        EditorUtility.SetDirty(target); // http://www.voidbred.com/blog/2014/12/auto-updating-custom-inspectors-for-unity/ & https://answers.unity.com/questions/505697/how-to-repaint-from-a-property-drawer.html
                    }
                };
            }

            return asSerializedObject.FindProperty("Value");
        }
    }
}
