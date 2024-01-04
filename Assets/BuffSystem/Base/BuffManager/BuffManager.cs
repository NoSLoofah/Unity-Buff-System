using NoSLoofah.BuffSystem.Dependence;
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
        public static readonly string SO_PATH = "Assets/Data/BuffData";    //保存Data的路径
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
            if (collection.buffList[id]==null) throw new System.Exception("引用的Buff为null。id："+id);
            return collection.buffList[id].Clone();
        }

        public void RegisterBuffTagManager(IBuffTagManager mgr)
        {
            tagManager = mgr;
        }
    }
}