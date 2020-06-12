using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Nrjwolf.Attributes.Editor
{
    public static class AttachAttributesEditor
    {
        public static Color GUIColorDefault = new Color(.6f, .6f, .6f, 1);
        public static Color GUIColorNull = new Color(1f, .5f, .5f, 1);

        public static string GetPropertyType(this SerializedProperty property)
        {
            var type = property.type;
            var match = Regex.Match(type, @"PPtr<\$(.*?)>");
            if (match.Success)
                type = match.Groups[1].Value;
            return type;
        }

        public static Type StringToType(this string aClassName) => System.AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).First(x => x.IsSubclassOf(typeof(Component)) && x.Name == aClassName);

        public static void OnGUI(Rect position, SerializedProperty property, GUIContent label, Action<GameObject, Type> func)
        {
            bool isPropertyValueNull = property.objectReferenceValue == null;

            // Change gui color
            var prevColor = GUI.color;
            GUI.color = isPropertyValueNull ? AttachAttributesEditor.GUIColorNull : AttachAttributesEditor.GUIColorDefault;

            // Default draw
            EditorGUI.PropertyField(position, property, label, true);

            // GetComponentInChildren
            property.serializedObject.Update();
            if (isPropertyValueNull)
            {
                var type = property.GetPropertyType().StringToType();
                var go = ((MonoBehaviour)(property.serializedObject.targetObject)).gameObject;
                func(go, type);
            }

            property.serializedObject.ApplyModifiedProperties();
            GUI.color = prevColor;
        }
    }

    /// GetComponent
    [CustomPropertyDrawer(typeof(GetComponentAttribute))]
    public class GetComponentAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            AttachAttributesEditor.OnGUI(position, property, label, (go, type) =>
            {
                property.objectReferenceValue = go.GetComponent(type);
            });
        }
    }

    /// GetComponentInChildren
    [CustomPropertyDrawer(typeof(GetComponentInChildrenAttribute))]
    public class GetComponentInChildrenAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GetComponentInChildrenAttribute labelAttribute = attribute as GetComponentInChildrenAttribute;
            AttachAttributesEditor.OnGUI(position, property, label, (go, type) =>
            {
                property.objectReferenceValue = go.GetComponentInChildren(type, labelAttribute.IncludeInactive);
            });
        }
    }

    /// AddComponent
    [CustomPropertyDrawer(typeof(AddComponentAttribute))]
    public class AddComponentAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            AttachAttributesEditor.OnGUI(position, property, label, (go, type) =>
            {
                property.objectReferenceValue = go.AddComponent(type);
            });
        }
    }

    /// FindObjectOfType
    [CustomPropertyDrawer(typeof(FindObjectOfTypeAttribute))]
    public class FindObjectOfTypeAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            AttachAttributesEditor.OnGUI(position, property, label, (go, type) =>
            {
                property.objectReferenceValue = FindObjectsOfTypeByName(property.GetPropertyType());
            });
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
}
