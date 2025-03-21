using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : Singleton<GridManager>
{
    [SerializeField] GridGround gridGround;
    [SerializeField] GridObjectOnMap gridObject;
    private Grid[,] gridArr = new Grid[20, 20];
    private GridGround[,] gridGroundArr = new GridGround[20, 20];
    private GridObjectOnMap[,] gridObjectsArr = new GridObjectOnMap[20, 20];

    public Grid[,] GridArray => gridArr;
    public GridGround[,] GridGround => gridGroundArr;
    public GridObjectOnMap[,] GridObjects => gridObjectsArr;
    [SerializeField] private List<GridGround> listGridGround = new List<GridGround>();
    [SerializeField] private List<GridObjectOnMap> listGridObject = new List<GridObjectOnMap>();
    public List<GridGround> ListGridGround => listGridGround;
    public List<GridObjectOnMap> ListGridObject => listGridObject;
    Scene currentScene;
    [SerializeField] Transform parentOfGrid;
    private int height;
    private int width;
    private float x;
    private float y;
    public float X => x;
    public float Y => Y;
    [SerializeField] ManagerSO managerSO;
    private void Awake()
    {
        managerSO.SetValueForMapObject();
        managerSO.SetValueForBuildingLevel();
        //DontDestroyOnLoad(gameObject);
        currentScene = SceneManager.GetActiveScene();
    }
    private void Start()
    {
        
        GameManager.ChangeState(GameState.MapChanging);
        //SpawnGridGround();
    }
    public void AddGridGround(GridGround grid)
    {
        listGridGround.Add(grid);
    }    
    public void AddGridObject(GridObjectOnMap grid)
    {
        listGridObject.Add(grid);
    }    
    public void RemoveGridObject(GridObjectOnMap grid)
    {
        listGridObject.Remove(grid);
    }    
    public void RemoveGridGround(GridGround grid)
    {
        listGridGround.Remove(grid);
    }    

    public void SpawnGridGround()
    {
        //ClearGridObjectArray();
        width = gridArr.GetLength(0);
        height = gridArr.GetLength(1);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (currentScene.name != "SampleScene")
                {
                    x = i + 0.5f;
                    y = j + 0.5f;
                    GridGround bgGrid = Instantiate(gridGround, new Vector3(x, y), Quaternion.identity);
                    bgGrid.ChangeGridProperty();
                    //bgGrid.name = grid.name;
                    bgGrid.TF.SetParent(parentOfGrid);
                    gridGroundArr[i, j] = bgGrid;
                    listGridGround.Add(bgGrid);
                    
                }
                else
                {
                    ClearGrid();
                }
            }
        }
        Debug.Log("Scene name: " + currentScene.name);
    }
    public void ClearGridArray()
    {
        for (int x = 0; x < gridGroundArr.GetLength(0); x++)
        {
            for (int y = 0; y < gridGroundArr.GetLength(1); y++)
            {
                // Check if there is an existing grid
                GridGround existingGrid = gridGroundArr[x, y];
                
                if (existingGrid != null)
                {
                    // Destroy the old grid object
                    Destroy(existingGrid.gameObject);
                    gridGroundArr[x, y] = null;
                }
            }
        }
    }
    public void ClearGrid()
    {
        if (listGridGround.Count > 0)
        {
            for (int i = 0; i < listGridGround.Count; i++)
            {
                Destroy(listGridGround[i].gameObject);
            }
            listGridGround.Clear();
        }

    }

    public void SpawnGridObject()
    {
        //ClearGridArray();
        width = gridArr.GetLength(0);
        height = gridArr.GetLength(1);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (currentScene.name != "SampleScene")
                {
                    x = i + 0.5f;
                    y = j + 0.5f;
                    GridObjectOnMap bgGrid = Instantiate(gridObject, new Vector3(x, y), Quaternion.identity);
                    //bgGrid.name = grid.name;
                    bgGrid.TF.SetParent(parentOfGrid);
                    gridObjectsArr[i, j] = bgGrid;
                    listGridObject.Add(bgGrid);
                }
                else
                {
                    ClearGridObject();
                }
            }
        }
        Debug.Log("Scene name: " + currentScene.name);
    }
    public void ClearGridObjectArray()
    {
        for (int x = 0; x < gridObjectsArr.GetLength(0); x++)
        {
            for (int y = 0; y < gridObjectsArr.GetLength(1); y++)
            {
                // Check if there is an existing grid
                GridObjectOnMap existingGrid = gridObjectsArr[x, y];
                if (existingGrid != null)
                {
                    // Destroy the old grid object
                    Destroy(existingGrid.gameObject);
                    gridObjectsArr[x, y] = null;
                }
            }
        }
    }
    public void ClearGridObject()
    {
        if (listGridObject.Count > 0)
        {
            for (int i = 0; i < listGridObject.Count; i++)
            {
                Destroy(listGridObject[i].gameObject);
            }
            listGridObject.Clear();
        }

    }
}
