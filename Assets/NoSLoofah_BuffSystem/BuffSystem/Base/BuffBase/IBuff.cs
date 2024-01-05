using UnityEngine;
namespace NoSLoofah.BuffSystem
{
    /// <summary>
    /// Buff的接口，定义了生命周期函数
    /// </summary>
    [UnityEngine.SerializeField]
    public interface IBuff
    {
        /// <summary>
        /// Buff启用时，生效前（即便该Buff不可作用于对象也会先执行）
        /// </summary>
        public void OnBuffAwake();
        /// <summary>
        /// Buff开始生效时
        /// </summary>
        public void OnBuffStart();
        /// <summary>
        /// Buff移除时（用于移除效果）
        /// </summary>
        public void OnBuffRemove();
        /// <summary>
        /// Buff销毁时（用于执行移除时效果）
        /// </summary>
        public void OnBuffDestroy();
        /// <summary>
        /// 更新周期性效果计时
        /// </summary>
        public void OnBuffUpdate();
        /// <summary>
        /// Buff层数变化时
        /// </summary>
        /// <param name="change"></param>
        public void OnBuffModifyLayer(int change);
        /// <summary>
        /// 开始周期性效果
        /// 如果已经开启过(无论是否在之后停止了)，则重置计时器并重新开始
        /// </summary>
        /// <param name="interval">周期时间</param>
        public void StartBuffTickEffect(float interval);
        /// <summary>
        /// 停止周期性效果
        /// </summary>
        public void StopBuffTickEffect();
        /// <summary>
        /// 重置Buff以复用
        /// </summary>
        public void Reset();
        /// <summary>
        /// 重置总体时间
        /// </summary>
        public void ResetTimer();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="target">Buff目标</param>
        /// <param name="caster">Buff来源</param>
        public void Initialize(IBuffHandler target, GameObject caster);
        /// <summary>
        /// 让Buff层级+=i
        /// </summary>
        /// <param name="i">改变的层数，可以为负</param>
        public void ModifyLayer(int i);
        /// <summary>
        /// 设置Buff是否生效。
        /// 不生效时，Buff的所有计时器也会暂停
        /// </summary>
        /// <param name="ef"></param>
        public void SetEffective(bool ef);

        /// <summary>
        /// 如果Buff名为空，就会视为不可使用的空Buff
        /// </summary>
        public bool IsEmpty();
    }
}