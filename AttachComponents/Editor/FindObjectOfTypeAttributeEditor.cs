using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FindObjectOfTypeAttribute))]
public class FindObjectOfTypeAttributeEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Default draw
        EditorGUI.PropertyField(position, property, label, true);

        // FindObjectOfType
        property.serializedObject.Update();
        if (property.objectReferenceValue == null)
        {
            var go = ((MonoBehaviour)(property.serializedObject.targetObject)).gameObject;
            var type = Type.GetType($"{GetPropertyType(property)}, Assembly-CSharp");
            property.objectReferenceValue = MonoBehaviour.FindObjectOfType(type);
        }
        property.serializedObject.ApplyModifiedProperties();
    }

    public static string GetPropertyType(SerializedProperty property)
    {
        var type = property.type;
        var match = Regex.Match(type, @"PPtr<\$(.*?)>");
        if (match.Success)
            type = match.Groups[1].Value;
        return type;
    }
}
