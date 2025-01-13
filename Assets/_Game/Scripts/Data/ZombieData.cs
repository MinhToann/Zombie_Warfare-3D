using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
public class ZombieData : Singleton<ZombieData>
{
    [field: SerializeField] public DataOfZombie data = new DataOfZombie();
    const string path = "ZombieData.abc";
    private void Awake()
    {
        data = LoadData();
    }
    public void SaveData()
    {
        SaveGame.Save(path, data);
    }
    public DataOfZombie LoadData()
    {
        return SaveGame.Load(path, new DataOfZombie());
    }
    public void AddZombieInWave(Zombie zombie)
    {
        data.AddZombieInWave(zombie);
        SaveData();
    }
    public void RemoveZombieInWave(Zombie zombie)
    {
        data.RemoveZombieInWave(zombie);
        SaveData();
    }
    public void ClearListZombieInWave()
    {
        data.ClearListZombieInWave();
        SaveData();
    }
}
[System.Serializable]
public class DataOfZombie
{
    [SerializeField] private List<Zombie> listZombieInWave = new List<Zombie>();

    public DataOfZombie()
    {
        listZombieInWave = null;
        //listZombieInWave.Capacity = 0;
    }
    public void AddZombieInWave(Zombie zombie)
    {
        listZombieInWave.Add(zombie);
    }
    public void RemoveZombieInWave(Zombie zombie)
    {
        listZombieInWave.Remove(zombie);
    }
    public void ClearListZombieInWave()
    {
        for(int i = 0; i < listZombieInWave.Count; i++)
        {
            SimplePool.Despawn(listZombieInWave[i]);
        }
        listZombieInWave.Clear();
    }
}
