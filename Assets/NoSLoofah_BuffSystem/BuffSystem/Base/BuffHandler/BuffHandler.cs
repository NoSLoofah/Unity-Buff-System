using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using NoSLoofah.BuffSystem.Manager;
namespace NoSLoofah.BuffSystem
{
    /// <summary>
    /// Buff处理器类，需要挂载在接受Buff的游戏对象上
    /// </summary>
    public class BuffHandler : MonoBehaviour, IBuffHandler
    {
        private List<Buff> buffs = new List<Buff>();
        private Action onAddBuff;
        private Action onRemoveBuff;


        public List<Buff> GetBuffs => new List<Buff>(buffs);
        public void RegisterOnAddBuff(Action act) { onAddBuff += act; }
        public void RemoveOnAddBuff(Action act) { onRemoveBuff -= act; }
        public void RegisterOnRemoveBuff(Action act) { onRemoveBuff += act; }
        public void RemoveOnRemoveBuff(Action act) { onRemoveBuff -= act; }
        #region 私有方法
        private void AddBuff(IBuff buff, GameObject caster)
        {
            if (!updated) Update();
            Buff bf = (Buff)buff;
            if (bf.IsEmpty())
            {
                Debug.LogError("尝试加入空Buff");
                return;
            }
            //无论是否可以添加都执行初始化和BuffAwake
            bf.Initialize(this, caster);
            bf.OnBuffAwake();

            //确定能添加Buff时
            onAddBuff?.Invoke();
            //检查是否已有同样的Buff
            Buff previous = buffs.Find(p => p.Equals(bf));
            //没有则直接添加
            if (previous == null)
            {
                //结算Tag效果
                if (bf.BuffTag != BuffTag.none)
                {
                    //首先：如果有已有buff能抵消新buff，则直接抵消
                    if (buffs.Any(b => BuffManager.GetInstance().TagManager.IsTagCanAddWhenHaveOther(bf.BuffTag, b.BuffTag)))
                    {
                        bf.SetEffective(false);
                        bf.OnBuffDestroy();
                        return;
                    }
                    for (int i = buffs.Count - 1; i >= 0; i--)
                    {
                        //之后：如果新buff没有被抵消，则新buff抵消已有的buff
                        //Debug.Log("Running:" + bf.BuffTag + ":" + buffs[i].BuffTag);
                        if (BuffManager.GetInstance().TagManager.IsTagRemoveOther(bf.BuffTag, buffs[i].BuffTag))
                        {
                            RemoveBuff(buffs[i]);
                        }
                    }
                }
                buffs.Add(bf);
                forOnBuffStart += bf.OnBuffStart;
                return;
            }
            //有则根据重复添加的类型处理。
            //一个Buff对象的Start不会重复执行
            //只有mutilCount类型会同时存在多个同id Buff
            switch (previous.MutilAddType)
            {
                case BuffMutilAddType.resetTime:
                    previous.ResetTimer();
                    //forOnBuffStart += previous.OnBuffStart;
                    break;
                case BuffMutilAddType.multipleLayer:
                    previous.ModifyLayer(1);
                    //forOnBuffStart += previous.OnBuffStart;
                    break;
                case BuffMutilAddType.multipleLayerAndResetTime:
                    previous.ResetTimer();
                    previous.ModifyLayer(1);
                    //forOnBuffStart += previous.OnBuffStart;
                    break;
                case BuffMutilAddType.multipleCount:
                    buffs.Add(bf);
                    forOnBuffStart += bf.OnBuffStart;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 移除一个Buff，移除后执行OnBuffRemove
        /// </summary>
        /// <param name="buff">要移除的Buff</param>
        private void RemoveBuff(IBuff buff)
        {
            Buff bf = (Buff)buff;
            bf.SetEffective(false);
        }
        /// <summary>
        /// 移除一个Buff，移除后不执行OnBuffRemove
        /// </summary>
        /// <param name="buff">要移除的Buff</param>
        private void InteruptBuff(IBuff buff)
        {
            Buff bf = (Buff)buff;
            bf.SetEffective(false);
            buffs.Remove(bf);
            forOnBuffDestroy += ((Buff)buff).OnBuffDestroy;
        }
        #endregion
        public void AddBuff(int buffId, GameObject caster)
        {
            var b = BuffManager.GetInstance().GetBuff(buffId);
            AddBuff(b, caster);
        }

        public void RemoveBuff(int buffId, bool removeAll = true)
        {
            var b = buffs.FirstOrDefault(b => b.ID == buffId);
            if (b == null)
            {
                Debug.Log("尝试从" + this.name + "移除没有添加的Buff。 id:" + buffId);
                return;
            }
            else if (b.MutilAddType == BuffMutilAddType.multipleCount && removeAll)
            {
                var bs = buffs.Where(b => b.ID == buffId);
                foreach (var bf in bs)
                {
                    RemoveBuff(bf);
                }
            }
            else RemoveBuff(b);
        }

        public void InterruptBuff(int buffId, bool removeAll = true)
        {
            var b = buffs.FirstOrDefault(b => b.ID == buffId);
            if (b == null)
            {
                Debug.Log("尝试从" + this.name + "打断没有添加的Buff。 id:" + buffId);
                return;
            }
            else if (b.MutilAddType == BuffMutilAddType.multipleCount && removeAll)
            {
                var bs = buffs.Where(b => b.ID == buffId);
                foreach (var bf in bs)
                {
                    InteruptBuff(bf);
                }
            }
            else InteruptBuff(b);
        }

        private bool updated = false;
        private Action forOnBuffDestroy;    //用于在下一帧执行onBuffDestory
        private Action forOnBuffStart;
        private void Update()
        {
            if (updated) return;
            updated = true;
            forOnBuffDestroy?.Invoke();
            forOnBuffStart?.Invoke();
            forOnBuffDestroy = null;
            forOnBuffStart = null;
        }
        private void LateUpdate()
        {
            updated = false;
            Buff bf;
            bool buffRemoved = false;
            for (int i = buffs.Count - 1; i >= 0; i--)
            {
                bf = buffs[i];
                //Debug.Log(bf);
                bf.OnBuffUpdate();
                if (!bf.IsEffective)
                {
                    bf.OnBuffRemove();
                    buffRemoved = true;
                    buffs.Remove(bf);
                    forOnBuffDestroy += bf.OnBuffDestroy;
                }
            }
            if (buffRemoved) onRemoveBuff?.Invoke();
        }
    }
}