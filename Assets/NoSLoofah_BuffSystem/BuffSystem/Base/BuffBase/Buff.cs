using NoSLoofah.BuffSystem.Dependence;
using UnityEngine;
using Newtonsoft.Json;
using System;
namespace NoSLoofah.BuffSystem
{
    [SerializeField]
    //重复添加同一种Buff时的行为
    public enum BuffMutilAddType
    {
        resetTime,                     //重置Buff时间
        multipleLayer,                 //增加Buff层数
        multipleLayerAndResetTime,     //增加Buff层数且重置Buff时间
        multipleCount                  //同种Buff同时存在多个，互不影响
    }
    /// <summary>
    /// 所有Buff类的基类
    /// </summary>
    [System.Serializable]
    public abstract class Buff : ScriptableObject, IBuff, IComparable<Buff>
    {
        /// <summary>
        /// 创建一个新的Buff对象
        /// </summary>
        /// <param name="buffName">具体的Buff子类名</param>
        /// <param name="id">Buff对象的ID</param>
        /// <returns>Buff对象</returns>
        public static Buff CreateInstance(string buffName, int id)
        {
            var b = (Buff)ScriptableObject.CreateInstance(buffName);
            b.id = id;
            return b;
        }
        /// <summary>
        /// 克隆当前的Buff
        /// </summary>
        /// <returns>克隆的Buff</returns>
        public Buff Clone()
        {
            return GameObject.Instantiate(this);
        }



        [JsonIgnore] public BuffHandler Target { get; private set; }
        [JsonIgnore] public GameObject Caster { get; private set; }
        [JsonIgnore] public int Layer { get; private set; }

        [ReplaceLabel("图标")][SerializeField] private Sprite icon;
        [ReplaceLabel("名称（必填）")][SerializeField] private string buffName;
        //层级
        [ReplaceLabel("重复添加方式")][SerializeField] private BuffMutilAddType mutilAddType;
        [ReplaceLabel("计时结束时层-1/层全清空")][SerializeField] private bool removeOneLayerOnTimeUp;

        [ReplaceLabel("Tag")][SerializeField] private BuffTag buffTag;
        [SerializeField][HideInInspector] private int id;
        private int tmpLayer = 0;
        private bool layerModified = false;
        private bool firstFrame;
        public bool RemoveOneLayerOnTimeUp => removeOneLayerOnTimeUp;
        public BuffMutilAddType MutilAddType => mutilAddType;
        public BuffTag BuffTag => buffTag;
        public string BuffName => buffName;
        public Sprite Icon => icon;
        public int ID => id;
        #region 管理器接手的生命周期函数
        public virtual void OnBuffAwake()
        {
            timer = duration;
            isEffective = true;
            Layer = 0;
            ModifyLayer(1);
            firstFrame = true;
        }
        public abstract void OnBuffRemove();

        public virtual void OnBuffDestroy()
        {
            if (mutilAddType == BuffMutilAddType.multipleLayer || mutilAddType == BuffMutilAddType.multipleLayerAndResetTime)
            {
                ModifyLayer(-Layer);
            }
            RealModifyLayer();
        }

        public abstract void OnBuffStart();
        public abstract void OnBuffModifyLayer(int change);
        public abstract void Reset();
        #region 定时器效果
        //buff时间
        private float timer;
        [ReplaceLabel("是永久的")][SerializeField] private bool isPermanent;
        [ReplaceLabel("Buff持续时间")][SerializeField] private float duration;
        private bool isEffective;
        [JsonIgnore] public bool IsEffective => isEffective;
        [JsonIgnore] public bool RunTickTimer => runTickTimer;

        //定时效果
        private float tickTimer;
        private float tickInterval;
        private bool runTickTimer = false;  //时候开始了周期性计时

        //Buff剩余时间
        public float RemainingTime => timer;
        //Buff总时长
        public float Duration => duration;
        public float TickRemainingTime => tickTimer;

        public bool IsPermanent => isPermanent;

        public void ResetTimer()
        {
            timer = duration;
        }
        public void SetEffective(bool ef)
        {
            isEffective = ef;
        }
        protected abstract void OnBuffTickEffect();
        public void StartBuffTickEffect(float interval)
        {
            runTickTimer = true;
            tickTimer = interval;
            this.tickInterval = interval;
        }
        public void StopBuffTickEffect()
        {
            runTickTimer = false;
        }
        public void OnBuffUpdate()
        {
            if (firstFrame)
            {
                firstFrame = false;
                return;
            }
            if (!IsEffective) return;
            if (!isPermanent)
            {
                timer -= Time.deltaTime;
                while (timer <= 0 && isEffective)
                {
                    if (removeOneLayerOnTimeUp)
                    {
                        timer += duration;
                        ModifyLayer(-1);
                    }
                    else
                    {
                        isEffective = false;
                        timer = 0;
                    }

                }
            }
            RealModifyLayer();
            if (!runTickTimer) return;
            tickTimer -= Time.deltaTime;
            while (tickTimer <= 0)
            {
                tickTimer += tickInterval;
                OnBuffTickEffect();
            }
        }
        #endregion
        #endregion
        public void Initialize(IBuffHandler target, GameObject caster)
        {
            this.Target = (BuffHandler)target;
            this.Caster = caster;
        }
        private void RealModifyLayer()
        {
            Layer += tmpLayer;
            if (layerModified) OnBuffModifyLayer(Layer < 0 ? -Layer : tmpLayer);
            if (Layer <= 0) isEffective = false;
            tmpLayer = 0;
            layerModified = false;
        }
        public void ModifyLayer(int i)
        {
            if (Layer + i != 0 && Layer + i != 1 && mutilAddType != BuffMutilAddType.multipleLayer && mutilAddType != BuffMutilAddType.multipleLayerAndResetTime)
                throw new System.Exception("试图修改非层级Buff的层数");
            tmpLayer += i;
            layerModified = true;
        }
        #region 重写
        public override string ToString()
        {

            return string.Concat(ID, ". ", buffName, ":", timer, "/", duration, "\tEffective: ", IsEffective);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(ID, base.GetHashCode());
        }
        public override bool Equals(object other)
        {
            if (!(other is Buff)) return false;
            return ID == ((Buff)other).ID;
        }

        public int CompareTo(Buff other)
        {
            return ID.CompareTo(other.ID);
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(buffName);
        }


        #endregion
    }
}