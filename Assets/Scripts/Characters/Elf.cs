using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : MonoBehaviour
{
    public int health = 50;
    public int armor = 100;
    public int gold = 500;

    public void SaveElf()
    {
        SaveSystem.SaveElf(this);
    }
    public void LoadElf()
    {
        ElfData data = SaveSystem.LoadElf();
        health = data.health;
        armor = data.armor;
        gold = data.gold;
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

    public void ChangeGold(int amount)
    {
        gold += amount;
    }

    #endregion

}
