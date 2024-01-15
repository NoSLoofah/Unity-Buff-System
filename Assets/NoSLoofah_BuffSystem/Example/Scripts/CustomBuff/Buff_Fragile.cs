using System;
using NoSLoofah.BuffSystem;
using UnityEngine;

[System.Serializable]
public class Buff_Fragile : Buff
{
    [SerializeField] private float hurtIncreaseRate;
    private Entity1 targetEntity;

    public override void OnBuffDestroy()
    {
        base.OnBuffDestroy();
    }

    public override void OnBuffModifyLayer(int change)
    {
        targetEntity.ModifyDamageMultiplier(hurtIncreaseRate * change);
    }

    public override void OnBuffRemove()
    {
        Debug.Log("OnRemove");
    }

    public override void OnBuffStart()
    {
        targetEntity = Target.GetComponent<Entity1>();
    }

    public override void Reset()
    {

    }

    protected override void OnBuffTickEffect()
    {

    }
}
