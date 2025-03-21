using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class MapData : Singleton<MapData>
{
    [field: SerializeField] public DataOfMap data = new DataOfMap();
    const string path = "MapData.abc";
    //[field: SerializeField] public DataInfoPlayer data = new DataInfoPlayer();

    private void Awake()
    {
        data = LoadData();
    }
    public void SaveData()
    {
        SaveGame.Save(path, data);
    }
    public DataOfMap LoadData()
    {
        return SaveGame.Load(path, new DataOfMap());
    }
    public void SetCurrentBuilding(GridObjectOnMap building)
    {
        data.SetCurrentBuilding(building);
        SaveData();
    }    
    public void AddBuilding(GridObjectOnMap building)
    {
        data.AddBuilding(building);
        SaveData();
    }
    public void ClearBuildingList()
    {
        data.ClearBuildingList();
        SaveData();
    }
}
[System.Serializable]
public class DataOfMap
{
    [SerializeField] private List<GridObjectOnMap> listBuilding = new List<GridObjectOnMap> ();
    public List<GridObjectOnMap> ListBuilding => listBuilding;
    [SerializeField] private GridObjectOnMap currentBuilding;
    public GridObjectOnMap CurrentBuilding => currentBuilding;
    public void SetCurrentBuilding(GridObjectOnMap currentBuilding)
    {
        this.currentBuilding = currentBuilding;
    }
    public void AddBuilding(GridObjectOnMap building)
    {
        if(!listBuilding.Contains(building))
        {
            listBuilding.Add(building);
        }    
        
    }
    public void ClearBuildingList()
    {
        for(int i = 0; i < listBuilding.Count; i++)
        {
            listBuilding.Remove(listBuilding[i]);
        }
        listBuilding.Clear();
    }
}
