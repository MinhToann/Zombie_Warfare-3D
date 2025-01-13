using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class PlayerData : Singleton<PlayerData> 
{
    [field: SerializeField] public DataOfPlayer data = new DataOfPlayer();
    const string path = "PlayerData.abc";
    private void Awake()
    {
        data = LoadData();
    }
    public void SaveData()
    {
        SaveGame.Save(path, data);
    }
    public DataOfPlayer LoadData()
    {
        return SaveGame.Load(path, new DataOfPlayer());
    }
}
public class DataOfPlayer
{
    
}
