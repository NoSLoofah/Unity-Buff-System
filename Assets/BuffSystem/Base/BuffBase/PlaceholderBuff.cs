using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoSLoofah.BuffSystem
{
    /// <summary>
    /// 用来占位的Buff类，即编辑器中的[NONE]
    /// 无视即可
    /// </summary>
    public class PlaceholderBuff : Buff
    {

        public override void OnBuffDestroy()
        {

        }

        public override void OnBuffModifyLayer(int change)
        {

        }

        public override void OnBuffRemove()
        {

        }

        public override void OnBuffStart()
        {

        }

        public override void Reset()
        {

        }

        protected override void OnBuffTickEffect()
        {

        }
    }
}