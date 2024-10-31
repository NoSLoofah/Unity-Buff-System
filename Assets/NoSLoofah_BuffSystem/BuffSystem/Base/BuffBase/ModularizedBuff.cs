using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoSLoofah.BuffSystem
{
    public class ModularizedBuff : Buff
    {
        [SerializeField] private float tick;        
        [SerializeField] private List<BuffEffect> onStartEffects;
        [SerializeField] private List<BuffEffect> onRemoveEffects;
        [SerializeField] private List<BuffEffect> onTickEffects;
        [SerializeField] private List<BuffEffect> onModifyLayerEffects;
        public override void OnBuffModifyLayer(int change)
        {
            foreach (var effect in onModifyLayerEffects) { effect.Fire(this); }
        }

        public override void OnBuffRemove()
        {
            StopBuffTickEffect();
            foreach (var effect in onRemoveEffects) { effect.Fire(this); }
        }

        public override void OnBuffStart()
        {
            StartBuffTickEffect(tick);
            foreach (var effect in onStartEffects) { effect.Fire(this); }
        }

        public override void Reset() { }

        protected override void OnBuffTickEffect()
        {
            foreach (var effect in onTickEffects) { effect.Fire(this); }
        }
    }
}