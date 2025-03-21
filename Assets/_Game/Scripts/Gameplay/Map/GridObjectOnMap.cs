
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectOnMap : Grid
{
    [SerializeField] GridObjectOnMap currentObjectGrid;
    public GridObjectOnMap CurrentObjectGrid => currentObjectGrid;
    private int countClick = 0;

    private int indexLayer;
    [SerializeField] private int idLevel;
    [SerializeField] private int rewardCoin;
    [SerializeField] private int rewardStar;
    [SerializeField] private int energyCost;
    public int IdLevel => idLevel;
    public int RewardCoin => rewardCoin;
    public int RewardStar => rewardStar;
    public int EnergyCost => energyCost;
    [SerializeField] ChoosingOutline outline;
    private ChoosingOutline currentOutline;
    [SerializeField] StarCanvas stars;
    private StarCanvas currentCanvasStar;
    public StarCanvas CurrentStarCanvas => currentCanvasStar;
    private List<StarData> starDataList = new List<StarData>();
    public List<StarData> StarDataList => starDataList;
    private void Start()
    {
        //OnInit();
    }
    public void OnInit()
    {
        ShowStar();
    }
    private void OnMouseDown()
    {
        if (GameManager.IsState(GameState.MainMenu))
        {
            if (gameObject.layer == LayerMask.NameToLayer(Constant.LAYER_UNLOCKED_LEVEL))
            {
                LevelManager.Ins.SetCurrentBuilding(this);
                ShowOutline();
                UIManager.Ins.OpenUI<CanvasChoosingMap>();
                UIManager.Ins.GetUI<CanvasChoosingMap>().SetValueForLevel(idLevel, rewardCoin, rewardStar, energyCost);
            }
        }
        if(GameManager.IsState(GameState.MapChanging))
        {
            ChangeGridProperty();
        }    
    }
    public void UpdateStarCount(int starCount)
    {
        if (currentCanvasStar != null)
        {
            for (int i = 0; i < starCount; i++)
            {
                currentCanvasStar.ChangeColorStar(i, 255, 247, 0, 255);
            }
        }

        // Save the updated data
        //SaveLoadMapHandler.Ins.SaveMap();
    }
    public void DeactiveOutline()
    {
        if (currentOutline != null)
        {
            Destroy(currentOutline.gameObject);
        }
    }
    public void ShowOutline()
    {
        if (currentOutline != null)
        {
            Destroy(currentOutline.gameObject);
        }
        currentOutline = Instantiate(outline, TF);
    }
    public void ShowStar()
    {
        if (currentCanvasStar != null)
        {
            Destroy(currentCanvasStar.gameObject);
        }
        if (gameObject.layer == LayerMask.NameToLayer(Constant.LAYER_UNLOCKED_LEVEL))
        {
            currentCanvasStar = Instantiate(stars, TF);
            //for (int i = 0; i < 3; i++)
            //{
            //    currentCanvasStar.ChangeColorStar(i, 50, 50, 50, 255);
            //}
            //CheckShining();
        }

        //currentCanvasStar.ChangeColorStar();
    }
    public void SaveStarsForLevel(int starsEarned)
    {      
        List<StarData> starDataList = SaveLoadMapHandler.LoadStars();

        bool found = false;
        for (int i = 0; i < starDataList.Count; i++)
        {
            if (starDataList[i].levelId == idLevel)
            {
                if (starsEarned > starDataList[i].starsEarned)
                {
                    starDataList[i].starsEarned = starsEarned;
                }
                found = true;
                break;
            }
        }

        if (!found)
        {
            starDataList.Add(new StarData { levelId = idLevel, starsEarned = starsEarned });
        }

        SaveLoadMapHandler.SaveStars(starDataList);
    }
    public void SetStateLevel(string state)
    {
        int LayerIgnoreRaycast = LayerMask.NameToLayer(state);
        gameObject.layer = LayerIgnoreRaycast;
    }
    public void SetIndexLevel(int id)
    {
        idLevel = id;
    }
    public void SetRewardCoin(int coin)
    {
        rewardCoin = coin;
    }
    public void SetRewardStar(int star)
    {
        rewardStar = star;
    }
    public void SetEnergyCost(int cost)
    {
        energyCost = cost;
    }
    public void ChangeGridProperty()
    {
        if (countClick > 0)
        {
            IncreaseNextMapObjectType();
        }
        countClick++;
        if (currentObjectGrid != null)
        {
            Destroy(currentObjectGrid.gameObject);
            GridManager.Ins.RemoveGridObject(currentObjectGrid);
        }
        if ((int)MapObjectOnGroundType < managerSO.ListMapDataSO.Count)
        {
            for (int i = 1; i < managerSO.ListGridMap.Count; i++)
            {
                if (MapObjectOnGroundType == managerSO.ListGridMap[i].MapObjectOnGroundType)
                {
                    currentObjectGrid = Instantiate((GridObjectOnMap)managerSO.ListGridMap[i], TF);
                }
            }

        }
        else
        {
            SetMapObjectType(MapObjectType.BigTree);
            //Destroy(currentObjectGrid.gameObject);
            currentObjectGrid = Instantiate((GridObjectOnMap)managerSO.ListGridMap[1], TF);
            countClick = 0;
        }
        //MapData.Ins.AddGridObject(currentObjectGrid);
        GridManager.Ins.AddGridObject(currentObjectGrid);
    }
}
