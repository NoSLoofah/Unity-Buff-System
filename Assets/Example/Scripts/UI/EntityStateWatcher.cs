using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityStateWatcher : MonoBehaviour
{
    [SerializeField] private Entity1 entity1;
    [SerializeField] TMP_Text text;
    private void Update()
    {
        string s = "生命值：" + entity1.Health + "/" + entity1.MaxHealth +
            "\n\r受伤倍率：" + entity1.DamageMultiplier + "/" + entity1.StartDamageMultiplier;
        text.text = s;
    }
}
