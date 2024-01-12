using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace NoSLoofah.BuffSystem.Manager.Editor
{
    [CustomEditor(typeof(BitBuffTagManager))]
    public class CustomBitBuffTagManager : UnityEditor.Editor
    {
        private SerializedProperty data;
        private void OnEnable()
        {
            data = serializedObject.FindProperty("tagData");
        }
        public override void OnInspectorGUI()
        {
            //serializedObject.UpdateIfRequiredOrScript();

            //EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.PropertyField(data);
            //if (GUILayout.Button(new GUIContent("New")))
            //{

            //    AssetDatabase.CreateAsset(CreateInstance<BitBuffTagData>(), EditorUtility.SaveFilePanelInProject("保存新BitBuffTag数据", "NewBitBuffData", "asset", "输入文件名"));
            //}
            //EditorGUILayout.EndHorizontal();

            //serializedObject.ApplyModifiedProperties();
        }
    }
}