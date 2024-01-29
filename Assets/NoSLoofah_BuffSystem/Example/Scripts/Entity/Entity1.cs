using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NoSLoofah.BuffSystem;
public class Entity1 : MonoBehaviour
{
    [Tooltip("生命值")][SerializeField] private int maxHealth;
    [Tooltip("受伤倍率")][SerializeField] private float startDamageMultiplier;
    private int health;
    private float damageMultiplier;

    public int MaxHealth => maxHealth;
    public float StartDamageMultiplier => startDamageMultiplier;
    public int Health => health;
    public float DamageMultiplier => damageMultiplier;

    private void Awake()
    {
        health = maxHealth;
        damageMultiplier = startDamageMultiplier;
    }
    public void ModifyHealth(int changeValue)
    {
        health += (int)(changeValue * (changeValue < 0 ? damageMultiplier : 1));
        health = Math.Clamp(health, 0, maxHealth);
    }
    public void ModifyDamageMultiplier(float changeValue)
    {
        damageMultiplier += changeValue;
    }
}
