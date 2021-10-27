using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WinnaBaba : MonoBehaviour ,IMonseterFighter
{
    public int attack = 20;
    public int health = 60;
    public int armor = 30;
    public float overallCooldown = 5.0f;


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
        StaticValues.WinoCzerwone += 10;
        StaticValues.WInoBiale += 10;
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

    public void SaveWinnaBaba()
    {
        SaveSystem.SaveWinnaBaba(this);
    }
    public void LoadWinnaBaba()
    {
        WinnaBabaData data = SaveSystem.LoadWinnaBaba();
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

    public int HandAttack()
    {
        return (int)(this.attack * 0.5);
    }

    public int HighkickAttack()
    {
        return (int)(this.attack * 1.2);
    }

    #endregion

}
