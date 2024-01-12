using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoSLoofah.BuffSystem
{
    /// <summary>
    /// 储存所有Buff的ScriptableObject类
    /// </summary>
    [CreateAssetMenu(fileName = "BFcollection", menuName = "BFcollection")]
    public class BuffCollection : ScriptableObject
    {
        [SerializeField] private int size = 20;
        [HideInInspector] public List<Buff> buffList = new List<Buff>();
        public int Size => size;
        /// <summary>
        /// 修改最大Buff数量
        /// </summary>
        /// <param name="size">目标数量</param>
        public void Resize(int size)
        {
            while (size < buffList.Count) buffList.RemoveAt(buffList.Count - 1);
            while (size > buffList.Count) buffList.Add(Buff.CreateInstance("PlaceholderBuff", buffList.Count));
            this.size = size;
        }
        /// <summary>
        /// 对齐最大Buff数量，用于在读入时初始化
        /// </summary>
        public void Resize()
        {
            Resize(size);
        }
    }
}