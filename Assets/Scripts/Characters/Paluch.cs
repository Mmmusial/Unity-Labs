using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paluch : MonoBehaviour, IMonseterFighter
{
    public int attack = 30;
    public int health = 50;
    public int armor = 50;
    public float overallCooldown = 6.0f;

    public event Action deathEvent;

    public MonsterAttack[] attacks;
    private int attackConter;
    public MonsterAttack getNextMonsterAttack()
    {
        return attacks[attackConter++ % attacks.Length];
    }

    public void recieveDamage(float damage)
    {
        health -= (int)damage;
        StaticValues.Lapuszki += 10;
        StaticValues.HajsZloty += 2;
        StaticValues.HajsSrebrny += 1;
        if (health <= 0)
        {
            deathEvent?.Invoke();
        }
    }

    public float getMonsterHealth()
    {
        return health;
    }


    public void SavePaluch()
    {
        SaveSystem.SavePaluch(this);
    }
    public void LoadPaluch()
    {
        PaluchData data = SaveSystem.LoadPaluch();
        health = data.health;
        armor = data.armor;
        transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
    }


    #region UI Methods
    public void ChangeHealth(int amount)
    {
        health += amount;
    }

    public void ChangeArmor(int amount)
    {
        armor += amount;
    }

    public int SwordAttack()
    {
        return (int)(this.attack * 1.3);
    }

    public int QuickStabAttack()
    {
        return (int)(this.attack * 0.9);
    }


    #endregion

}
