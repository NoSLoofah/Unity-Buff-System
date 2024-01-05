using NoSLoofah.BuffSystem.Dependence;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace NoSLoofah.BuffSystem.Manager
{
    /// <summary>
    /// Buff管理器，单例模式。
    /// 用于通过序号获取Buff对象
    /// </summary>
    public class BuffManager : MonoSingleton<BuffManager>, IBuffManager
    {
        //public static readonly string SO_PATH = "Assets/NoSLoofah_BuffSystem/BuffSystem/Data/BuffData";    //保存Data的路径
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
        private BuffCollection collection;
        private IBuffTagManager tagManager;
        public bool IsWorking => collection != null;

        public IBuffTagManager TagManager => tagManager;

        protected override void Awake()
        {
            base.Awake();
            collection = null;
            string[] assetPaths = AssetDatabase.FindAssets("t:BuffCollection", new string[] { SO_PATH });
            if (assetPaths.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetPaths[0]);
                collection = AssetDatabase.LoadAssetAtPath<BuffCollection>(assetPath);
            }
        }
        public IBuff GetBuff(int id)
        {
            if (id < 0 || id >= collection.Size)
            {
                throw new System.Exception("使用非法的Buff id：" + id + " (当前Buff总数为" + collection.Size + ")");
            }
            if (collection.buffList[id] == null) throw new System.Exception("引用的Buff为null。id：" + id);
            return collection.buffList[id].Clone();
        }

        public void RegisterBuffTagManager(IBuffTagManager mgr)
        {
            tagManager = mgr;
        }
    }
}