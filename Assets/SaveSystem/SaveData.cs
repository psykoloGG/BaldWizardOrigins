using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int Level;
    public int Kills;
    public string WeaponName;
    
    public SaveData(int Level, int Kills, string WeaponName)
    {
        this.Level = Level;
        this.Kills = Kills;
        this.WeaponName = WeaponName;
    }
}
