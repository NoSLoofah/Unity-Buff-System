using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoSLoofah.BuffSystem
{
    [CreateAssetMenu(fileName = "NewBuffEffect", menuName = "BuffEffect")]
    public abstract class BuffEffect : ScriptableObject, IBuffEffect
    {
        public abstract void Fire(Buff sourceBuff);
    }
}