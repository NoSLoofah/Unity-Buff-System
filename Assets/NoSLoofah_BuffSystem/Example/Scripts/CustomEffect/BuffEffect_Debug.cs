using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoSLoofah.BuffSystem;
[CreateAssetMenu(fileName = "BuffEffect", menuName = "Buffeffect/Debug")]
public class BuffEffect_Debug : BuffEffect
{
    public string msg;
    public override void Fire(Buff sourceBuff)
    {
        Debug.Log(string.Format("[{0}] {1}: {2}", Time.time, sourceBuff.name, msg));
    }
}
