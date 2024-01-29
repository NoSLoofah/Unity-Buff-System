using UnityEngine;
using NoSLoofah.BuffSystem;
public class Buff_Bleed : Buff
{
    [SerializeField] private int bleedDamage;
    [SerializeField] private float bleedTimeInterval;
    private Entity1 targetEntity;

    public override void OnBuffDestroy()
    {
        base.OnBuffDestroy();
    }

    public override void OnBuffModifyLayer(int change)
    {

    }

    public override void OnBuffRemove()
    {

    }

    public override void OnBuffStart()
    {
        targetEntity = Target.GetComponent<Entity1>();
        StartBuffTickEffect(bleedTimeInterval);
    }

    public override void Reset()
    {

    }

    protected override void OnBuffTickEffect()
    {        
        targetEntity.ModifyHealth(-Layer * bleedDamage);
    }

}
