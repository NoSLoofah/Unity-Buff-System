/// <summary>
/// 支持2层增益变值的整数属性
/// </summary>
public class IntAttribute
{
    public IntAttribute(int baseValue = 0)
    {
        Reset();
        BaseValue = baseValue;
    }
    /// <summary>
    /// 结算值
    /// </summary>
    public int Value { get; private set; }
    /// <summary>
    /// 基础值
    /// </summary>
    public int BaseValue { get; private set; }
    /// <summary>
    /// 0层加值
    /// </summary>
    public int Addition0 { get; private set; }
    /// <summary>
    /// 0层百分比值
    /// </summary>
    public int PctAddion0 { get; private set; }
    /// <summary>
    /// 1层加值
    /// </summary>
    public int Addition1 { get; private set; }
    /// <summary>
    /// 1层百分比值
    /// </summary>
    public int PctAddion1 { get; private set; }
    public void Reset()
    {
        Value = BaseValue = Addition0 = Addition1 = PctAddion0 = PctAddion1 = 0;
    }
    public void SetBaseValue(int value)
    {
        BaseValue = value;
        UpdateValue();
    }
    public void ModifyValue(int value)
    {
        BaseValue += value;
        UpdateValue();
    }
    public void ModifyAddition0(int value)
    {
        Addition0 += value;
        UpdateValue();
    }
    public void ModifyAddition1(int value)
    {
        Addition1 += value;
        UpdateValue();
    }
    public void ModifyPctAddition0(int value)
    {
        PctAddion0 += value;
        UpdateValue();
    }

    public void ModifyPctAddition1(int value)
    {
        PctAddion1 += value;
        UpdateValue();
    }

    private void UpdateValue()
    {
        float v1 = (BaseValue + Addition0) * (100 + PctAddion0) / 100f;
        float v2 = (v1 + Addition1) * (100 + PctAddion1) / 100f;
        Value = (int)v2;
    }
}
