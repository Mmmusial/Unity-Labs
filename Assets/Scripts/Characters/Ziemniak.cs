using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ziemniak : MonoBehaviour
{
    public int health = 50;
    public int armor = 100;

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

    #endregion

}
