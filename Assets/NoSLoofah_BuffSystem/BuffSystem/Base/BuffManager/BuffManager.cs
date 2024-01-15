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
        [HideInInspector][SerializeField] private BuffCollection collection;
        private IBuffTagManager tagManager;
        public bool IsWorking => collection != null;

        public IBuffTagManager TagManager => tagManager;
        public void SetData(BuffCollection buffCollection)
        {
            this.collection = buffCollection;
        }

        protected override void Awake()
        {
            base.Awake();
            if (collection == null) Debug.LogError("BuffCollection数据丢失");
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