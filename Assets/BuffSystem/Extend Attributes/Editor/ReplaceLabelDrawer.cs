using UnityEditor;
using UnityEngine;
namespace NoSLoofah.BuffSystem.Dependence.Editor
{
    [CustomPropertyDrawer(typeof(ReplaceLabelAttribute))]
    public class ReplaceLabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (ReplaceLabelConfig.Allow) label.text = ((ReplaceLabelAttribute)attribute).label;
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
