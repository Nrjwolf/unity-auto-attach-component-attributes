
using System;
using UnityEngine;

namespace Nrjwolf.Attributes
{
    [AttributeUsage(System.AttributeTargets.Field)]
    public class GetComponentAttribute : PropertyAttribute
    {
    }

    [AttributeUsage(System.AttributeTargets.Field)]
    public class GetComponentInChildrenAttribute : PropertyAttribute
    {
        public bool IncludeInactive { get; private set; }

        public GetComponentInChildrenAttribute(bool includeInactive)
        {
            IncludeInactive = includeInactive;
        }
    }

    [AttributeUsage(System.AttributeTargets.Field)]
    public class AddComponentAttribute : PropertyAttribute
    {
    }

    [AttributeUsage(System.AttributeTargets.Field)]
    public class FindObjectOfTypeAttribute : PropertyAttribute
    {
    }
}
