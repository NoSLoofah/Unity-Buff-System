using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoSLoofah.BuffSystem
{
    /// <summary>
    /// BuffTag管理器的接口，用于判断Tag之间的互斥关系
    /// </summary>
    public interface IBuffTagManager
    {
        /// <summary>
        /// 添加带有tag的Buff时，是否会移除Tag为other的已有Buff
        /// </summary>
        /// <param name="tag">新添加Buff的tag</param>
        /// <param name="other">已有Buff的Tag</param>
        /// <returns></returns>
        public bool IsTagRemoveOther(BuffTag tag, BuffTag other);
        /// <summary>
        /// 添加带有tag的Buff时，是否会被Tag为other的已有Buff抵消
        /// </summary>
        /// <param name="tag">新添加Buff的tag</param>
        /// <param name="other">已有Buff的Tag</param>
        /// <returns></returns>
        public bool IsTagCanAddWhenHaveOther(BuffTag tag, BuffTag other);
    }
}