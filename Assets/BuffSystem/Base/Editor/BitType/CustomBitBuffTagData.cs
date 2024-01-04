using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
namespace NoSLoofah.BuffSystem.Editor
{
    [CustomEditor(typeof(BitBuffTagData))]
    public class CustomBitBuffTagData : UnityEditor.Editor
    {
        int l;
        SerializedProperty removedTagsData;
        SerializedProperty blockTagsData;
        private void OnEnable()
        {
            l = Enum.GetNames(typeof(BuffTag)).Length;
            removedTagsData = serializedObject.FindProperty("removedTags");
            blockTagsData = serializedObject.FindProperty("blockTags");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            removedTagsData.arraySize = l;
            blockTagsData.arraySize = l;            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Tag"), GUILayout.MinWidth(0));
            EditorGUILayout.LabelField(new GUIContent("被该Tag抵消的"), GUILayout.MinWidth(0));
            EditorGUILayout.LabelField(new GUIContent("禁止该Tag添加的"), GUILayout.MinWidth(0));
            EditorGUILayout.EndHorizontal();
            for (int i = 1; i < removedTagsData.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent(((BuffTag)(1 << (i - 1))).ToString()), GUILayout.MinWidth(0));
                //string name = tagNamesData.GetArrayElementAtIndex(i).stringValue;
                //tagNamesData.GetArrayElementAtIndex(i).stringValue = EditorGUILayout.TextField(name);

                removedTagsData.GetArrayElementAtIndex(i).intValue = (int)(BuffTag)EditorGUILayout.EnumFlagsField((BuffTag)removedTagsData.GetArrayElementAtIndex(i).intValue);
                blockTagsData.GetArrayElementAtIndex(i).intValue = (int)(BuffTag)EditorGUILayout.EnumFlagsField((BuffTag)blockTagsData.GetArrayElementAtIndex(i).intValue);
                EditorGUILayout.EndHorizontal();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}