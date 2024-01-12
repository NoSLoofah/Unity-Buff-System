using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace NoSLoofah.BuffSystem
{
    /// <summary>
    /// 储存位Tag数据的ScriptableObject
    /// </summary>
    public class BitBuffTagData : BuffTagData
    {
        [Tooltip("这些tag的旧Buff会被挤掉")][SerializeField] private List<int> removedTags;
        [Tooltip("存在这些tag的Buff时新Buff无法被添加")][SerializeField] private List<int> blockTags;

        public int[] RemovedTags => removedTags.ToArray();
        public int[] BlockTags => blockTags.ToArray();

        /// <summary>
        /// 获取一个Tag在列表中的序号
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static int GetIndex(BuffTag tag)
        {
            if (tag == 0) return 0;
            int i = 0;
            while (((int)tag & (1 << i)) == 0) i++;
            return i + 1;
        }

        public void Initialize()
        {
            removedTags = Enumerable.Repeat(0, 32).ToList();
            blockTags = Enumerable.Repeat(0, 32).ToList();
        }
    }
}