using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ziemniak : MonoBehaviour,IMonseterFighter
{
    public int attack = 15;
    public int health = 100;
    public int armor = 1;
    public float overallCooldown = 5.0f;

    public event Action deathEvent;

    public void SaveZiemniak()
    {
        SaveSystem.SaveZiemniak(this);
    }
    public void LoadZiemniak()
    {
        ZiemniakData data = SaveSystem.LoadZiemniak();
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

    public int BlobAttack()
    {
        return (int)(this.attack * 1.1);
    }

    public int SplitAttack()
    {
        return (int)(this.attack * 0.7);
    }

    int internalCounter = 0;
    public MonsterAttack getNextMonsterAttack()
    {
        MonsterAttack blobAttack = new MonsterAttack
        {
            attackDelay = 5,
            damage = attack * 1.1f,
            attackffset = 0.1f,
            targetCoverage = 0.15f
        };

        MonsterAttack splitAttack = new MonsterAttack
        {
            attackDelay = 4,
            damage = attack * 0.7f,
            attackffset = 0.2f,
            targetCoverage = 0.2f
        };

        return internalCounter++ % 2 == 0 ? blobAttack : splitAttack;

    }

    public void recieveDamage(float damage)
    {
        health-=(int)damage;
        if (health <= 0)
        {
            deathEvent?.Invoke();
        }
    }

    public float getMonsterHealth()
    {
        return health;
    }

    #endregion

}
