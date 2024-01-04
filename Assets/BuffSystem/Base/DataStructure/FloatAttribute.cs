/// <summary>
/// 支持2层增益变值的浮点数属性
/// </summary>
public class FloatAttribute
{
    public FloatAttribute(float baseValue = 0f)
    {
        Reset();
        BaseValue = baseValue;
    }

    /// <summary>
    /// 结算值
    /// </summary>
    public float Value { get; private set; }

    /// <summary>
    /// 基础值
    /// </summary>
    public float BaseValue { get; private set; }

    /// <summary>
    /// 0层加值
    /// </summary>
    public float Addition0 { get; private set; }

    /// <summary>
    /// 0层百分比值
    /// </summary>
    public int PctAddion0 { get; private set; }

    /// <summary>
    /// 1层加值
    /// </summary>
    public float Addition1 { get; private set; }

    /// <summary>
    /// 1层百分比值
    /// </summary>
    public int PctAddion1 { get; private set; }

    /// <summary>
    /// 重置所有属性值为0
    /// </summary>
    public void Reset()
    {
        Value = BaseValue = Addition0 = Addition1 = PctAddion0 = PctAddion1 = 0;
    }

    /// <summary>
    /// 设置基础值
    /// </summary>
    public void SetBaseValue(float value)
    {
        BaseValue = value;
        UpdateValue();
    }

    /// <summary>
    /// 修改基础值
    /// </summary>
    public void ModifyValue(float value)
    {
        BaseValue += value;
        UpdateValue();
    }

    /// <summary>
    /// 修改0层加值
    /// </summary>
    public void ModifyAddition0(float value)
    {
        Addition0 += value;
        UpdateValue();
    }

    /// <summary>
    /// 修改1层加值
    /// </summary>
    public void ModifyAddition1(float value)
    {
        Addition1 += value;
        UpdateValue();
    }

    /// <summary>
    /// 修改0层百分比值
    /// </summary>
    public void ModifyPctAddition0(int value)
    {
        PctAddion0 += value;
        UpdateValue();
    }

    /// <summary>
    /// 修改1层百分比值
    /// </summary>
    public void ModifyPctAddition1(int value)
    {
        PctAddion1 += value;
        UpdateValue();
    }

    private void UpdateValue()
    {
        float v1 = (BaseValue + Addition0) * (100f + PctAddion0) / 100f;
        float v2 = (v1 + Addition1) * (100f + PctAddion1) / 100f;
        Value = v2;
    }
}