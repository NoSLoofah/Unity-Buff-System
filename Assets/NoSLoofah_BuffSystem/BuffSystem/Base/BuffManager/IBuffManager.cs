using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoSLoofah.BuffSystem.Manager
{
    /// <summary>
    /// Buff管理器的接口
    /// </summary>
    public interface IBuffManager
    {
        /// <summary>
        /// 通过序号获得Buff对象
        /// </summary>
        /// <param name="id">Buff的id</param>
        /// <returns>对应的Buff对象</returns>
        public IBuff GetBuff(int id);
        /// <summary>
        /// 注册BuffTagManager
        /// </summary>
        /// <param name="mgr">BuffTagManager</param>
        public void RegisterBuffTagManager(IBuffTagManager mgr);
    }
}