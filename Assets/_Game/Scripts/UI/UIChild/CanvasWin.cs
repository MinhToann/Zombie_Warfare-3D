using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWin : UICanvas
{
    [SerializeField] ManagerSO managerSO;
    [SerializeField] TextMeshProUGUI rewardCoinText;
    [SerializeField] TextMeshProUGUI rewardStarText;
    [SerializeField] List<StarProgress> listStarProgress = new List<StarProgress>();
    [SerializeField] Transform parentStar;
    public List<StarProgress> ListStarProgress => listStarProgress;
    public Transform ParentStar => parentStar;
    [SerializeField] Text levelText;
    private float timer;
    public override void Setup()
    {
        base.Setup();
        GameManager.ChangeState(GameState.Win);
        SetMissionText(LevelManager.Ins.CurrentBuildingLevel.IdLevel);
        ResetStar();
        timer = 1f;
    }
    public override void Open()
    {
        base.Open();
        CheckReward();
        CheckLevel();
    }
    public override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            LevelManager.Ins.HandleSpawnStar(listStarProgress.Count, this);
        }    
    }
    private void ResetStar()
    {
        for(int i = 0; i < listStarProgress.Count; i++)
        {
            listStarProgress[i].OnInit();
        }    
    }    
    public void OkButton()
    {
        
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ResetAll();
        //UIManager.Ins.OpenUI<CanvasMainMenu>();
        UIManager.Ins.OpenUI<CanvasChangeScene>();
    }    
    public void SetMissionText(int level)
    {
        levelText.text = "Mission " + level.ToString();
    }

    private void CheckReward()
    {
        for(int i = 0; i < managerSO.ListLevelSO.Count; i++)
        {
            if (LevelManager.Ins.CurrentIdBuilding == managerSO.ListLevelSO[i].ID)
            {
                SetReward(managerSO.ListLevelSO[i].RewardCoin, managerSO.ListLevelSO[i].RewardStar);
            }    
        }    
        
    }    
    private void CheckLevel()
    {
        for (int i = 0; i < managerSO.ListBuildingLevel.Count; i++)
        {
            if (LevelManager.Ins.CurrentIdBuilding == managerSO.ListBuildingLevel[i].IdLevel)
            {
                MapData.Ins.SetCurrentBuilding(managerSO.ListBuildingLevel[i]);
            }
        }

    }
    public void SetReward(int coin, int star)
    {
        rewardCoinText.text = coin.ToString();
        rewardStarText.text = star.ToString();
    }    
}
