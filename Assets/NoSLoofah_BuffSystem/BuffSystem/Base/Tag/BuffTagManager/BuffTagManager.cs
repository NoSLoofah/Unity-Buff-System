using NoSLoofah.BuffSystem;
using NoSLoofah.BuffSystem.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// BuffTag管理器的抽象基类
/// 目前只有位Tag的一种实现
/// </summary>
public abstract class BuffTagManager : MonoBehaviour, IBuffTagManager
{
    public abstract bool IsTagRemoveOther(BuffTag tag, BuffTag other);
    public abstract bool IsTagCanAddWhenHaveOther(BuffTag tag, BuffTag other);
    protected virtual void Start()
    {
        BuffManager.GetInstance().RegisterBuffTagManager(this);
    }
}
