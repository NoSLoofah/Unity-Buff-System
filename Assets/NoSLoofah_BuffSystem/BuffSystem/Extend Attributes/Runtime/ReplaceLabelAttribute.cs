using UnityEditor;
using UnityEngine;
namespace NoSLoofah.BuffSystem.Dependence
{
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
    public class ReplaceLabelAttribute : PropertyAttribute
    {
        public string label;

        public ReplaceLabelAttribute(string label)
        {
            this.label = label;
        }
    }
}