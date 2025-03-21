using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    //[SerializeField] Level[] levels;
    //public Level currentLevel;
    [SerializeField] List<LevelSO> listLevels = new List<LevelSO>();
    private LevelSO currentLevel;
    public LevelSO CurLevel => currentLevel;
    int level = 0;
    [SerializeField] Transform parentBG;
    public bool IsEndAllWave => curNumberWave >= currentLevel.WavesInLevel.Count - 1;
    private bool isWin;
    public bool IsWin => isWin;
    [SerializeField] Image background;
    [SerializeField] GridObjectOnMap currentBuilding;
    public GridObjectOnMap CurrentBuildingLevel => currentBuilding;
    private GridObjectOnMap lastBuilding;
    public GridObjectOnMap LastBuilding => lastBuilding;

    [SerializeField] private int currentIdBuilding;
    public int CurrentIdBuilding => currentIdBuilding;
    [SerializeField] ManagerSO managerSO;
    private Vector3 cameraOffset;
    [SerializeField] Camera mainCam;
    int indexMapLevel;
    [SerializeField] StarSpawned stars;
    [SerializeField] CameraController cameraController;
    [SerializeField] List<GameplayIcon> listIconCharacter = new List<GameplayIcon>();
    public List<GameplayIcon> ListIcon = new List<GameplayIcon>();
    int countStar;
    [SerializeField] Map map;
    private Map curMap;
    public Map CurrentMap => curMap;
    private Waves currentWave;
    public Waves CurrentWave => currentWave;
    private int curNumberWave = 0;
    public int CurrentNumberWave => curNumberWave;

    private bool isMainMenu;
    public bool IsMainMenu => isMainMenu;
    public void Start()
    {
        indexMapLevel = PlayerPrefs.GetInt(Constant.INDEX_MAP_LEVEL, 3);
        OnInit();
    }

    //khoi tao trang thai bat dau game
    public void OnInit()
    {
        isWin = false;
        UIManager.Ins.OpenUI<CanvasLoadingGame>();
        cameraOffset = new Vector3(0, 3, -6);
        countStar = 0;
        OnUnlockLevel();
    }

    public void OnLoadLevel(int level)
    {
        EntitiesManager.Ins.SetBoolEmptyZombie(false);
        currentLevel = listLevels[level];
        CreateLevel(parentBG);
    }
    public void CreateLevel(Transform parentBG)
    {
        curNumberWave = 0;
        if (curMap != null)
        {
            Destroy(curMap.gameObject);
        }
        curMap = Instantiate(map);       
        OnLoadWave(curNumberWave);
    }
    public void OnLoadWave(int index)
    {
        if (index < currentLevel.WavesInLevel.Count)
        {           
            currentWave = currentLevel.WavesInLevel[index];
            if (currentWave.HasBoss)
            {
                EntitiesManager.Ins.WarningSpawnBoss();
            }
        }
        
    }
    public void NextWave()
    {
        CanvasGameplay canvas = UIManager.Ins.GetUI<CanvasGameplay>();
        //if (EntitiesManager.Ins.IsEmpty || canvas.WaveTime <= 0)
        //{
        //    //OnLoadWave(++curNumberWave);

        //}
        OnLoadWave(++curNumberWave);
        canvas.SpawnWaveText();
        canvas.SetWaveTime(currentWave.TimeInWave);
        EntitiesManager.Ins.DelaySpawnZombies();
        EntitiesManager.Ins.SetBoolEmptyZombie(false);
    }

    public void OnDespawnMap()
    {
        if (curMap != null)
        {
            Destroy(curMap.gameObject);
        }
        //curNumberWave = 0;
    }
    public void SetDefaultWave(int wave)
    {
        curNumberWave = wave;
    }
    public void DeactiveBackground()
    {
        background.gameObject.SetActive(false);
    }    
    public void OnNextWave()
    {
        if (EntitiesManager.Ins.IsEmpty || UIManager.Ins.GetUI<CanvasGameplay>().WaveTime <= 0)
        {
            //currentLevel.NextWave();
            NextWave();
            //if(CurrentWave.HasBoss)
            //{
            //    EntitiesManager.Ins.WarningSpawnBoss();
            //}    
        }

    }    
    public void SetWinState(bool isWin)
    {
        this.isWin = isWin;
    }    
    public void OnWin()
    {
        isWin = true;
        UIManager.Ins.OpenUI<CanvasWin>();
        EntitiesManager.Ins.GoBackToCar();
        curNumberWave = 0;
        for(int i = 0; i < managerSO.ListBuildingLevel.Count; i++)
        {
            if (currentIdBuilding == managerSO.ListBuildingLevel[i].IdLevel)
            {
                if (managerSO.ListBuildingLevel[i + 1].gameObject.layer == LayerMask.NameToLayer(Constant.LAYER_LOCKED_LEVEL))
                {
                    indexMapLevel++;
                    PlayerPrefs.SetInt(Constant.INDEX_MAP_LEVEL, indexMapLevel);
                    OnUnlockLevel();
                }
            }
        }    
    }
    public void OnLose()
    {
        isWin = false;
        UIManager.Ins.OpenUI<CanvasLose>();
    }
    public void SetOriginalCameraOffset()
    {
        mainCam.transform.position = cameraOffset;
    }    
    public void ResetAll()
    {
        EntitiesManager.Ins.OnClearAllCharacter();
        EntitiesManager.Ins.ClearBlood();
        ClearIcon();
        background.gameObject.SetActive(false);
        OnDespawnMap();
        //SetBoolIsMenu(false);
    }    
    public void OnUnlockLevel()
    {
        for (int i = 0; i < managerSO.ListBuildingLevel.Count; i++)
        {
            if (i < indexMapLevel)
            {
                managerSO.ListBuildingLevel[i].SetStateLevel(Constant.LAYER_UNLOCKED_LEVEL);
            }
            else
            {
                managerSO.ListBuildingLevel[i].SetStateLevel(Constant.LAYER_LOCKED_LEVEL);
            }
        }
    }
    private void HandleSpawnStarReward(int countList,CanvasWin canvas)
    {
        float currentHP = EntitiesManager.Ins.CurrentCar.CurrentHealth;
        if(currentHP >= 0.8f)
        {
            countStar = countList;
        }
        else if ((currentHP >= 0.5f) && (currentHP < 0.8f))
        {
            countStar = countList - 1;
        }
        else if(currentHP < 0.5f)
        {
            countStar = countList - 2;
        }
    }
    public void HandleSpawnStar(int countList, CanvasWin canvas)
    {
        float delay = 0f;
        HandleSpawnStarReward(countList, canvas);
        for (int i = 0; i < countStar; i++)
        {
            StarProgress starProgress = canvas.ListStarProgress[i];
            Vector3 starPos = starProgress.TF.position;
            starProgress.GoldStarImage.DOFade(1, 0.5f).SetDelay(delay);
            starProgress.GoldStarImage.transform.DOMove(starPos, 0.25f).SetDelay(delay);
            starProgress.CurrentGoldStar.SetBoolShining(true);
            if(MapData.Ins.data.CurrentBuilding != null)
            {
                MapData.Ins.data.CurrentBuilding.SaveStarsForLevel(countStar);
            }              
            delay += 0.5f;
        }
        
    }
    public void SetCurrentBuilding(GridObjectOnMap building)
    {
        if (currentBuilding != null)
        {
            lastBuilding = currentBuilding;
            lastBuilding.DeactiveOutline();
            currentBuilding = null;
        }
        currentBuilding = building;
        //currentBuilding.ShowOutline();
    }
    public void ShakeCamera()
    {
        StartCoroutine(cameraController.Shake(0.15f, 0.4f));
    }    
    public void AddIcon(GameplayIcon icon)
    {
        listIconCharacter.Add(icon);
    }
    public void ClearIcon()
    {
        for(int i = 0; i < listIconCharacter.Count; i++)
        {
            Destroy(listIconCharacter[i].gameObject);
        }
        listIconCharacter.Clear();
    }
    public void SetCurrentIdBuilding(int id)
    {
        currentIdBuilding = id;
    }    
    public void SetBoolIsMenu(bool isMainMenu)
    {
        this.isMainMenu = isMainMenu;
    }
}
