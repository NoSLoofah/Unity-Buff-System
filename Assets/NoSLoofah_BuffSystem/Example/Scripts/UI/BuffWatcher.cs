using NoSLoofah.BuffSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BuffWatcher : MonoBehaviour
{
    [SerializeField] private BuffHandler buffHandler;
    [SerializeField] private GameObject displayer;
    private List<BuffDisplayer> displayers = new List<BuffDisplayer>();

    private void Update()
    {
        for (int i = displayers.Count - 1; i >= 0; i--)
        {
            if (!displayers[i].isWorking)
            {
                var g = displayers[i].gameObject;
                displayers.RemoveAt(i);
                Destroy(g);
            }
        }

        var b = buffHandler.GetBuffs;
        var c = displayers.Select(d => d.Buff.GetHashCode()).ToList();
        var newBuffs = b.Where(bf => !c.Contains(bf.GetHashCode())).ToList();
        foreach (var bf in newBuffs)
        {
            var g = Instantiate(displayer).GetComponent<BuffDisplayer>();
            g.SetBuff(bf);
            displayers.Add(g);
            g.transform.SetParent(transform);
        }

    }
}
