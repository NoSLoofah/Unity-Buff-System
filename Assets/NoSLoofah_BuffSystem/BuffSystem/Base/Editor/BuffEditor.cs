using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.IO;
using System.Reflection;
using NoSLoofah.BuffSystem.Manager;

namespace NoSLoofah.BuffSystem.Editor
{
    public class BuffEditor : EditorWindow
    {
        [MenuItem("Tools/BuffEditor")]
        public static void OpenWindow()
        {
            BuffEditor wd = GetWindow<BuffEditor>();       //唤出窗口        
            wd.titleContent = new GUIContent("Buff编辑器"); //添加标题
            wd.Initialize();
        }

        public static string SO_PATH
        {
            get
            {
                var l = AssetDatabase.FindAssets("BuffMgr t:Prefab");
                if (l.Length <= 0) throw new Exception("BuffMgr.prefab丢失,请重新导入BuffSystem");
                else if (l.Length > 1) Debug.LogError("请保证项目中只有一个BuffMgr.prefab");
                var path = AssetDatabase.GUIDToAssetPath(l[0]).Replace("Base/BuffMgr.prefab", "Data/BuffData");
                return path;
            }
        }//保存Data的路径


        //左侧列表
        private const float WIDTH_DIVISON = 0.3f;           //左栏比例
        private int selectedBuffindex = -1;                 //选中Buff序号
        private Vector2 leftScrollPos = Vector2.zero;
        private BuffCollection SO;                          //BuffSO
        private SerializedObject SO_SO;

        private SerializedObject currentBuffSO;
        private SerializedProperty currentBuffSP;
        //文件保存
        private static readonly string assetBundleName = "Buff";
        public static readonly string defaultSOName = "BuffCollection.asset";
        public static readonly string defaultTagSOName = "BuffTagData.asset";
        private int maxLengthBuffer;
        private GameObject mgr = null;
        private BitBuffTagData tagData = null;

        public static string BUFF_PATH => SO_PATH + "/Buffs";

        //语言
        private readonly string[] LANGUAGES = { "中文", "English" };
        private int langSelection = 0;

        //程序集
        private const string ASSEMBLY_NAME = "Assembly-CSharp";
        private Assembly assembly;

        //子类选择
        int selectedSubclassIndex = 0;
        int lastSelectSubclassIndex = 0;
        bool changedSubClassSelection = false;
        const string PLACEHOLDER_BUFF_NAME = "PlaceholderBuff";
        const string NONE_TYPE = "[NONE]";


        //类    
        List<Type> subClasses;
        string[] subClassNames;

        //int nameIndex = 0;
        //int nameIndexLength = 3;

        //bool useClassNamePrefix;    //使用类名作为前缀
        //bool keepInput;             //创建后保留输入
        //bool useIndexName;          //使用序号命名

        private void OnEnable()
        {
            Initialize();
        }
        #region 更新方法
        private void UpdateBuffMgr()
        {
            var l = AssetDatabase.FindAssets("BuffMgr t:Prefab");
            if (l.Length <= 0) throw new Exception("BuffMgr.prefab丢失,请重新导入BuffSystem");
            else if (l.Length > 1) Debug.LogError("请保证项目中只有一个BuffMgr.prefab");
            string assetPath = AssetDatabase.GUIDToAssetPath(l[0]);
            mgr = PrefabUtility.LoadPrefabContents(assetPath);
            mgr.GetComponent<BitBuffTagManager>().SetData(tagData);
            mgr.GetComponent<BuffManager>().SetData(SO);
            PrefabUtility.SaveAsPrefabAsset(mgr, assetPath);
            PrefabUtility.UnloadPrefabContents(mgr);
        }
        private void Initialize()
        {
            assembly = AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name.Equals(ASSEMBLY_NAME));
            UpdateSubClass();
            UpdateLeftList();
            GenerateReadme();
            CreateBuffTagAsset();
            UpdateBuffMgr();
        }
        private void GenerateReadme()
        {
            var p = Path.Combine(SO_PATH, "README.txt");
            if (!File.Exists(p))
            {
                var s = File.CreateText(p);
                s.WriteLine("请不要修改该路径下的文件，也不要移动他们的位置");
                s.WriteLine("Please do not modify the files in this directory or move them from their current location.");
                s.Close();
            }
        }
        /// <summary>
        /// 更新子类列表
        /// </summary>
        private void UpdateSubClass()
        {
            selectedSubclassIndex = 0;
            lastSelectSubclassIndex = 0;
            changedSubClassSelection = false;
            subClasses = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Buff)) && !t.IsAbstract).ToList();
            subClassNames = subClasses.Select(c => c.Name).ToArray();
            var s = subClassNames[0];
            int i = Array.IndexOf(subClassNames, PLACEHOLDER_BUFF_NAME);
            subClassNames[i] = s;
            subClassNames[0] = NONE_TYPE;
        }
        /// <summary>
        /// 更新左侧列表数据（重新读入数据）
        /// </summary>
        private void UpdateLeftList()
        {
            string[] assetPaths = AssetDatabase.FindAssets("t:BuffCollection", new string[] { SO_PATH });
            if (assetPaths.Length <= 0)
            {
                Debug.Log("没有数据");//待处理，或许可以直接创建一个新的
                if (!Directory.Exists(SO_PATH)) Directory.CreateDirectory(SO_PATH);
                SO = CreateInstance<BuffCollection>();
                var path = Path.Combine(SO_PATH, defaultSOName);
                AssetDatabase.CreateAsset(SO, path);
                UpdateBuffMgr();
            }
            else
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetPaths[0]);
                SO = AssetDatabase.LoadAssetAtPath<BuffCollection>(assetPath);
            }
            SO.Resize();
            if (!Directory.Exists(BUFF_PATH)) Directory.CreateDirectory(BUFF_PATH);
            //Debug.Log(SO);
            //Debug.Log(SO.buffList);
            //foreach (var b in SO.buffList) { Debug.Log(b); }
            //for (int i = 0; i < SO.Size; i++)
            //{
            //    if (SO.buffList[i] == null || !AssetDatabase.Contains(SO.buffList[i]))
            //    {
            //        CreateBuffAsset(i);
            //    }
            //}
            maxLengthBuffer = SO.buffList.Count;
            //Debug.Log("测试" + SO.buffList[2]);
            SO_SO = new SerializedObject(SO);
        }
        #endregion

        /// <summary>
        /// 将Buff保存成asset
        /// </summary>
        /// <param name="index">保存的Buff在buffList的序号</param>
        private void CreateBuffAsset(int index)
        {
            string fileName = "Buff" + string.Format("{0:000}", index) + ".asset";
            string dir = Path.Combine(BUFF_PATH, fileName);
            if (File.Exists(dir)) File.Delete(dir);

            Buff b = SO.buffList[index];
            AssetDatabase.CreateAsset(b, dir);

        }
        private void CreateBuffTagAsset()
        {
            string dir = Path.Combine(SO_PATH, defaultTagSOName);
            if (File.Exists(dir))
            {
                tagData = AssetDatabase.LoadAssetAtPath<BitBuffTagData>(dir);
                return;
            }
            BitBuffTagData b = CreateInstance<BitBuffTagData>();
            b.Initialize();
            AssetDatabase.CreateAsset(b, dir);
            tagData = b;
            UpdateBuffMgr();
        }
        /// <summary>
        /// 绘制分界线
        /// </summary>
        private void DrawDivider()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("---------------------------------------------------------------------------------------------------------------");
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        private string I18nString(string c, string e)
        {
            if (langSelection == 0) return c;
            else return e;
        }

        private void OnGUI()
        {
            //左侧
            float leftWidth = position.width * WIDTH_DIVISON;

            Rect leftRect = new Rect(0, 0, leftWidth, position.height);
            GUILayout.BeginArea(leftRect, EditorStyles.helpBox);
            leftScrollPos = GUILayout.BeginScrollView(leftScrollPos);
            GUIStyle leftListButtonStyle = new GUIStyle(GUI.skin.button);
            leftListButtonStyle.alignment = TextAnchor.MiddleLeft;
            //内容        

            for (int i = 0; i < SO.Size; i++)
            {
                leftListButtonStyle.normal.textColor = i == selectedBuffindex ? Color.green : Color.white;
                if (GUILayout.Button(new GUIContent(string.Format("{0:000}", i) + ":" +
                    ((SO.buffList[i] == null || string.IsNullOrEmpty(SO.buffList[i].BuffName)) ? "NULL" : SO.buffList[i].BuffName))
                    , leftListButtonStyle))
                {
                    //选中                
                    if (selectedBuffindex == i) break;
                    selectedBuffindex = i;
                    //取消聚焦
                    GUIUtility.keyboardControl = 0;

                    //如果文件还没有创建，那么暂时还不创建
                    //等用户选择了他的类型后再创建
                    int index;
                    if (SO.buffList[i] == null)
                    {
                        index = 0;
                    }
                    else
                    {
                        string t = SO.buffList[i].GetType().Name;
                        index = Array.IndexOf(subClassNames, t);
                        index = index < 0 ? 0 : index;
                        currentBuffSO = new SerializedObject(SO.buffList[i]);
                    }

                    //重置选中的类型
                    lastSelectSubclassIndex = index;
                    selectedSubclassIndex = index;
                    changedSubClassSelection = false;
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndScrollView();
            GUILayout.BeginHorizontal();
            maxLengthBuffer = EditorGUILayout.IntField(new GUIContent("最大Buff数量"), maxLengthBuffer);
            if (GUILayout.Button(new GUIContent("修改")))
            {
                if (maxLengthBuffer < SO.Size)
                {
                    bool set = EditorUtility.DisplayDialog("重设最大Buff数量", "减少最大Buff数量会删除末尾的Buff，确认要修改吗？", "是", "否");

                    if (set)
                    {
                        SO.Resize(maxLengthBuffer);
                        selectedBuffindex = -1;

                        DirectoryInfo directory = new DirectoryInfo(BUFF_PATH);
                        FileInfo[] files = directory.GetFiles("Buff*.asset");

                        foreach (FileInfo file in files)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(file.Name);
                            string fileIndexString = fileName.Substring(4).Split('.')[0];

                            int index = int.Parse(fileIndexString);
                            if (index >= SO.Size)
                            {
                                AssetDatabase.DeleteAsset(Path.Combine(BUFF_PATH, file.Name));
                                Debug.Log("删除Buff：" + file.Name);
                            }

                        }
                    }
                }
                else SO.Resize(maxLengthBuffer);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();









            if (selectedBuffindex < 0) return;

            //右侧
            Rect rightRect = new Rect(leftWidth, 0, position.width - leftWidth, position.height);
            GUILayout.BeginArea(rightRect, EditorStyles.helpBox);

            //内容
            selectedSubclassIndex = EditorGUILayout.Popup(I18nString("类", "Class"), selectedSubclassIndex, subClassNames);

            changedSubClassSelection = selectedSubclassIndex != lastSelectSubclassIndex;
            lastSelectSubclassIndex = selectedSubclassIndex;

            if (changedSubClassSelection)
            {
                //修改Buff类型
                //创建Buff asset资源的唯一位置
                SO.buffList[selectedBuffindex] = Buff.CreateInstance(selectedSubclassIndex == 0 ? PLACEHOLDER_BUFF_NAME : subClassNames[selectedSubclassIndex], selectedBuffindex);
                CreateBuffAsset(selectedBuffindex);
                currentBuffSO = new SerializedObject(SO.buffList[selectedBuffindex]);
            }


            EditorGUILayout.Separator();
            DrawDivider();
            //序列化类
            if (selectedSubclassIndex != 0)
            {
                currentBuffSP = currentBuffSO.GetIterator();
                currentBuffSP.NextVisible(true);
                currentBuffSO.UpdateIfRequiredOrScript();
                while (currentBuffSP.NextVisible(true))
                {
                    if (currentBuffSP.name.Equals("buffTag"))
                    {
                        currentBuffSP.enumValueFlag = (int)(BuffTag)EditorGUILayout.EnumPopup(new GUIContent("Tag"), (BuffTag)currentBuffSP.enumValueFlag);
                    }
                    else if (currentBuffSP.name.Equals("icon"))
                    {
                        currentBuffSP.objectReferenceValue = EditorGUILayout.ObjectField("图标", (Sprite)currentBuffSP.objectReferenceValue, typeof(Sprite), false) as Sprite;
                    }
                    else if (currentBuffSP.name.Equals("isPermanent"))
                    {
                        bool p = EditorGUILayout.Toggle(I18nString("永久的", "Is Permanent"), currentBuffSP.boolValue);
                        currentBuffSP.boolValue = p;
                        if (p) currentBuffSP.NextVisible(true);
                    }
                    else
                        EditorGUILayout.PropertyField(currentBuffSP, true);
                }
                if (GUI.changed)
                {
                    //Debug.Log("Saved");
                    currentBuffSO.ApplyModifiedProperties();
                    SO_SO.ApplyModifiedProperties();
                    EditorUtility.SetDirty(SO.buffList[selectedBuffindex]);
                    EditorUtility.SetDirty(SO);
                    AssetDatabase.SaveAssets();
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(I18nString("刷新", "Refresh"), GUILayout.Width(60f))) Initialize();
            GUILayout.FlexibleSpace();
            langSelection = EditorGUILayout.Popup("Language", langSelection, LANGUAGES);
            EditorGUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
}