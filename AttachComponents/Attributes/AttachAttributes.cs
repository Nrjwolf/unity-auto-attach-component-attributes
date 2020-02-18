
using System;
using UnityEngine;

[AttributeUsage(System.AttributeTargets.Field)] public class GetComponentAttribute : PropertyAttribute { }
[AttributeUsage(System.AttributeTargets.Field)] public class AddComponentAttribute : PropertyAttribute { }
[AttributeUsage(System.AttributeTargets.Field)] public class FindObjectOfTypeAttribute : PropertyAttribute { }