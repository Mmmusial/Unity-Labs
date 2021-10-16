using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witcher : MonoBehaviour
{
    public int health = 50;
    public int armor = 100;
    public int gold = 1000;
    public int silver = 450;

    public void SaveWitcher()
    {
        SaveSystem.SaveWitcher(this);
    }
    public void LoadWitcher()
    {
        
        WitcherData data = SaveSystem.LoadWitcher();
        health = data.health;
        armor = data.armor;
        gold = data.gold;
        silver = data.silver;
        transform.position = new Vector3(data.position[0],data.position[1],data.position[2]);
        Debug.Log(new Vector3(data.position[0], data.position[1], data.position[2]));
        Debug.Log(gameObject.name);
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

    public void ChangeSilver(int amount)
    {
        silver += amount;
    }
    #endregion

}
