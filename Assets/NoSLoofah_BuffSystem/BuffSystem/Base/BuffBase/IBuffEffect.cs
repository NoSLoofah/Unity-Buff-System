using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoSLoofah.BuffSystem
{
    public interface IBuffEffect
    {
        public void Fire(Buff sourceBuff);
    }
}