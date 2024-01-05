using NoSLoofah.BuffSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffDisplayer : MonoBehaviour
{
    private Buff buff;
    public bool isWorking => buff != null && buff.IsEffective;

    public Buff Buff => buff;

    //UI
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text text;

    public void SetBuff(Buff bf)
    {
        buff = bf;
        if (!isWorking) canvasGroup.alpha = 0;
    }
    private void Update()
    {
        if (!isWorking) return;
        Show();
    }
    private void Show()
    {
        canvasGroup.alpha = 1;
        icon.sprite = buff.Icon;
        string t = buff.BuffName + "\t剩余时间：" + buff.RemainingTime + "\n\r周期效果时间：" + buff.TickRemainingTime
            + "\n\r层数：" + buff.Layer;
        text.text = t;
    }
}
