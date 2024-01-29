using JetBrains.Annotations;
using NoSLoofah.BuffSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private BuffHandler buffHandler;
    public void AddBuff()
    {
        int i;
        if (!int.TryParse(inputField.text, out i)) return;
        buffHandler.AddBuff(i, null);
    }
    public void RemoveBuff()
    {
        int i;
        if (!int.TryParse(inputField.text, out i)) return;
        buffHandler.RemoveBuff(i);
    }
    public void InteruptBuff()
    {
        int i;
        if (!int.TryParse(inputField.text, out i)) return;
        buffHandler.InterruptBuff(i);
    }
}
