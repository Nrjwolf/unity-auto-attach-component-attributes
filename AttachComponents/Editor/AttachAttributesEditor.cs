using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public static class AttachAttributesEditor
{
    public static string GetPropertyType(this SerializedProperty property)
    {
        var type = property.type;
        var match = Regex.Match(type, @"PPtr<\$(.*?)>");
        if (match.Success)
            type = match.Groups[1].Value;
        return type;
    }

    public static Type StringToType(this string aClassName) => System.AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).First(x => x.IsSubclassOf(typeof(Component)) && x.Name == aClassName);
}

/// GetComponent
[CustomPropertyDrawer(typeof(GetComponentAttribute))]
public class GetComponentAttributeEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Default draw
        EditorGUI.PropertyField(position, property, label, true);

        // GetComponent
        property.serializedObject.Update();
        if (property.objectReferenceValue == null)
        {
            var type = property.GetPropertyType().StringToType();
            var go = ((MonoBehaviour)(property.serializedObject.targetObject)).gameObject;
            property.objectReferenceValue = go.GetComponent(type);
        }
        property.serializedObject.ApplyModifiedProperties();
    }
}

/// AddComponent
[CustomPropertyDrawer(typeof(AddComponentAttribute))]
public class AddComponentAttributeEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Default draw
        EditorGUI.PropertyField(position, property, label, true);

        // AddComponent
        property.serializedObject.Update();
        if (property.objectReferenceValue == null)
        {
            var type = property.GetPropertyType().StringToType();
            var go = ((MonoBehaviour)(property.serializedObject.targetObject)).gameObject;
            property.objectReferenceValue = go.AddComponent(type);
        }
        property.serializedObject.ApplyModifiedProperties();
    }
}

/// FindObjectOfType
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
            property.objectReferenceValue = FindObjectsOfTypeByName(property.GetPropertyType());
        }
        property.serializedObject.ApplyModifiedProperties();
    }

    public UnityEngine.Object FindObjectsOfTypeByName(string aClassName)
    {
        var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
        for (int i = 0; i < assemblies.Length; i++)
        {
            var types = assemblies[i].GetTypes();
            for (int n = 0; n < types.Length; n++)
            {
                if (typeof(UnityEngine.Object).IsAssignableFrom(types[n]) && aClassName == types[n].Name)
                    return UnityEngine.Object.FindObjectOfType(types[n]);
            }
        }
        return new UnityEngine.Object();
    }
}