using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoSLoofah.BuffSystem
{    
    public abstract class BuffEffect : ScriptableObject, IBuffEffect
    {
        public abstract void Fire(Buff sourceBuff);
    }
}