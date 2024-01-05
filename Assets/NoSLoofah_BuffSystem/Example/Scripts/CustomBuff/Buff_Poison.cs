using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoSLoofah.BuffSystem;
public class Buff_Poison : Buff
{
    [SerializeField] private int poisonDamage;
    [SerializeField] private float poisonTimeInterval;
    [SerializeField] private GameObject effect;
    private Entity1 targetEntity;
    public override void OnBuffDestroy() { base.OnBuffDestroy(); }

    public override void OnBuffModifyLayer(int change) { }

    public override void OnBuffRemove() { }

    public override void OnBuffStart()
    {
        targetEntity = Target.GetComponent<Entity1>();
        StartBuffTickEffect(poisonTimeInterval);
    }

    public override void Reset() { }

    protected override void OnBuffTickEffect()
    {
        targetEntity.ModifyHealth(-Layer * poisonDamage);
        var g = Instantiate(effect);
        g.transform.position = Target.transform.position;
    }
}
