using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(int Level, int Kills, string WeaponName)
    {
        try
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            string Path = Application.persistentDataPath + "/save.dat";
            FileStream Stream = new FileStream(Path, FileMode.Create);

            SaveData Data = new SaveData(Level, Kills, WeaponName);

            Formatter.Serialize(Stream, Data);
            Stream.Close();
        }
        catch (System.Exception Exception)
        {
            Debug.LogError("Failed to save data: " + Exception.Message);
        }
    }
    
    public static SaveData LoadData()
    {
        try
        {
            string Path = Application.persistentDataPath + "/save.dat";
            if (File.Exists(Path))
            {
                BinaryFormatter Formatter = new BinaryFormatter();
                FileStream Stream = new FileStream(Path, FileMode.Open);

                SaveData Data = Formatter.Deserialize(Stream) as SaveData;
                Stream.Close();

                return Data;
            }
            else
            {
                Debug.LogError("Save file does not exist");
                return null;
            }
        }
        catch (System.Exception Exception)
        {
            Debug.LogError("Failed to load data: " + Exception.Message);
            return null;
        }
    }
    
    public static void DeleteData()
    {
        string Path = Application.persistentDataPath + "/save.dat";
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }
    }
    
    public static bool DataExists()
    {
        string Path = Application.persistentDataPath + "/save.dat";
        return File.Exists(Path);
    }
}
