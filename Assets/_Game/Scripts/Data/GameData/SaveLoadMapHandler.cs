using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using System.IO;

public class SaveLoadMapHandler : Singleton<SaveLoadMapHandler>
{
    public List<GroundData> groundMatrix = new List<GroundData>();
    public List<ObjectData> objectMatrix = new List<ObjectData>();
    public Dictionary<MapObjectType, Grid> prefabLookup; // Map enum to prefab, assign in Inspector
    private MapObjectType currentType;
    [SerializeField] Transform groundParent;
    [SerializeField] Transform objectParent;
    [SerializeField] ManagerSO managerSO;
    [SerializeField] List<Grid> listGrid = new List<Grid>();
    private string fileNameGridGround = "groundData.json";
    private string filenameGridObject = "objectData.json";
    private List<StarData> starDataList = new List<StarData>();
    public List<StarData> StarDataList => starDataList;
    private static string filenameStarData = "starsData.json";
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        InitDictionaryGrid();
    }
    void Start()
    {
          
    }
    private void InitDictionaryGrid()
    {
        prefabLookup = new Dictionary<MapObjectType, Grid>();
        for (int i = 0; i < managerSO.ListGridMap.Count; i++)
        {
            if (!prefabLookup.ContainsKey(managerSO.ListGridMap[i].MapObjectOnGroundType))
                prefabLookup.Add(managerSO.ListGridMap[i].MapObjectOnGroundType, managerSO.ListGridMap[i]);
        }
    }    
    public void SaveMap()
    {
        List<GroundData> groundData = new List<GroundData>();
        for (int x = 0; x < GridManager.Ins.GridGround.GetLength(0); x++)
        {
            for (int y = 0; y < GridManager.Ins.GridGround.GetLength(1); y++)
            {
                GridGround ground = GridManager.Ins.GridGround[x, y];
                if (ground != null)
                {
                    groundData.Add(new GroundData
                    {
                        x = x,
                        y = y,
                        type = ground.MapObjectOnGroundType // Assuming `Type` is a MapObjectType
                    });
                }
            }
        }

        // Collect Object Data
        List<ObjectData> objectData = new List<ObjectData>();
        for (int x = 0; x < GridManager.Ins.GridObjects.GetLength(0); x++)
        {
            for (int y = 0; y < GridManager.Ins.GridObjects.GetLength(1); y++)
            {
                GridObjectOnMap obj = GridManager.Ins.GridObjects[x, y];
                if (obj != null && obj.CurrentObjectGrid != null)
                {
                    ObjectData data = new ObjectData
                    {
                        x = x,
                        y = y,
                        type = obj.MapObjectOnGroundType // Assuming Type is your MapObjectType enum
                    };
                    objectData.Add(data);
                    //objectData.Add(new ObjectData
                    //{
                    //    x = x,
                    //    y = y,
                    //    type = obj.MapObjectOnGroundType // Assuming `Type` is a MapObjectType
                    //});
                }
            }
        }

        // Save to JSON
        FileHandler.SaveToJSON(groundData, fileNameGridGround);
        FileHandler.SaveToJSON(objectData, filenameGridObject);
        Debug.Log("Map saved successfully!");

    }
    public void LoadMap()
    {
        ClearMap();
        if (prefabLookup == null || prefabLookup.Count == 0)
        {
            Debug.LogError("Prefab lookup is not initialized!");
            return;
        }
        List<GroundData> groundMatrix = FileHandler.ReadFromJSON<GroundData>(fileNameGridGround);
        List<ObjectData> objectMatrix = FileHandler.ReadFromJSON<ObjectData>(filenameGridObject);
        List<StarData> starDataList = LoadStars();
        // Recreate the scene
        foreach (var ground in groundMatrix)
        {
            if (prefabLookup.TryGetValue(ground.type, out Grid prefab))  // Now it's of type Grid
            {
                Grid newGround = Instantiate(prefab, new Vector3(ground.x, 0, ground.y), Quaternion.identity);
                newGround.TF.SetParent(groundParent); // Ensure it's parented correctly
                listGrid.Add(newGround);
            }
        }

        foreach (var obj in objectMatrix)
        {
            if (prefabLookup.TryGetValue(obj.type, out Grid prefab))  // Now it's of type Grid
            {
                Grid newObject = Instantiate(prefab, new Vector3(obj.x, 0, obj.y), Quaternion.identity);
                newObject.TF.SetParent(objectParent); // Ensure it's parented correctly
                listGrid.Add(newObject);
                GridObjectOnMap newGrid = (GridObjectOnMap)newObject;
                newGrid.ShowStar();
                foreach (var starData in starDataList)
                {
                    if (starData.levelId == newGrid.IdLevel)
                    {
                        newGrid.UpdateStarCount(starData.starsEarned);
                        //break;
                    }    
                }
            }
        }
    }
    public void ClearMap()
    {
        if(listGrid.Count > 0)
        {
            for (int i = 0; i < listGrid.Count; i++)
            {
                Destroy(listGrid[i].gameObject);
            }
            listGrid.Clear();
        }    
        
    }
    public static void SaveStars(List<StarData> starDataList)
    {
        string json = JsonUtility.ToJson(new StarDataWrapper(starDataList));
        File.WriteAllText(Application.persistentDataPath + "/" + filenameStarData, json);
    }

    public static List<StarData> LoadStars()
    {
        string path = Application.persistentDataPath + "/" + filenameStarData;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            StarDataWrapper dataWrapper = JsonUtility.FromJson<StarDataWrapper>(json);
            return dataWrapper.starDataList;
        }
        return new List<StarData>();
    }
    public void SetList(List<StarData> list)
    {
        starDataList = list;
    }    
}
[System.Serializable]
public class GroundData
{
    public int x;
    public int y;
    public MapObjectType type;
}

[System.Serializable]
public class ObjectData
{
    public int x;
    public int y;
    public MapObjectType type;
}
[System.Serializable]
public class StarData
{
    public int levelId;
    public int starsEarned;
}
[System.Serializable]
public class StarDataWrapper
{
    public List<StarData> starDataList;

    public StarDataWrapper(List<StarData> list)
    {
        starDataList = list;
    }
}

