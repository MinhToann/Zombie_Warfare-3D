using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using System.ComponentModel;

public class TeamData : Singleton<TeamData>
{
    [field: SerializeField] public SaveTeamData data = new SaveTeamData();
    const string dataPath = "TeamData.abc";
    private void Awake()
    {
        data = LoadData();
    }
    public void SaveData()
    {
        SaveGame.Save(dataPath, data);
    }
    public SaveTeamData LoadData()
    {
        return SaveGame.Load(dataPath, new SaveTeamData());
    }
    public void AddToDesk(GameObjectType icon)
    {
        data.AddToDesk(icon);
        SaveData();
    }
    public void RemoveFromDesk(GameObjectType icon)
    {
        data.RemoveFromDesk(icon);
        SaveData();
    }
    public void AddToCollection(GameObjectType icon)
    {
        data.AddToCollection(icon);
        SaveData();
    }
    public void RemoveFromCollection(GameObjectType icon)
    {
        data.RemoveFromCollection(icon);
        SaveData();
    }
    public void ClearListCollection()
    {
        data.ClearListCollection();
        SaveData();
    }
    public void ClearListInDesk()
    {
        data.ClearListInDesk();
        SaveData();
    }
}
[System.Serializable]
public class SaveTeamData
{
    [SerializeField] List<GameObjectType> listCollectionTeam;
    [SerializeField] List<GameObjectType> listInDeskTeam;

    public List<GameObjectType> ListCollection => listCollectionTeam;
    public List<GameObjectType> ListInDesk => listInDeskTeam;
    public SaveTeamData()
    {
        listCollectionTeam = null;
        listInDeskTeam = null;
    }
    public void AddToDesk(GameObjectType icon)
    {
        listInDeskTeam.Add(icon);
    }
    public void RemoveFromDesk(GameObjectType icon)
    {
        listInDeskTeam.Remove(icon);
    }
    public void ClearListCollection()
    {
        for(int i = 0; i < listCollectionTeam.Count; i++)
        {
            listCollectionTeam.Remove(listCollectionTeam[i]);
        }
        listCollectionTeam.Clear();
    }
    public void AddToCollection(GameObjectType icon)
    {
        listCollectionTeam.Add(icon);
    }
    public void RemoveFromCollection(GameObjectType icon)
    {
        listCollectionTeam.Remove(icon);
    }
    public void ClearListInDesk()
    {
        for (int i = 0; i < listInDeskTeam.Count; i++)
        {
            listInDeskTeam.Remove(listInDeskTeam[i]);
        }
        listInDeskTeam.Clear();
    }
    public bool IsCheckInCollection(GameObjectType goType)
    {
        for(int i = 0; i <  listCollectionTeam.Count; i++)
        {
            if (listCollectionTeam[i] == goType)
            {
                return true;
            }
            else return false;
        }
        return false;
    }
    public bool IsCheckInDesk(GameObjectType goType)
    {
        for (int i = 0; i < listInDeskTeam.Count; i++)
        {
            if (listInDeskTeam[i] == goType)
            {
                return true;
            }
            else return false;
        }
        return false;
    }
}
