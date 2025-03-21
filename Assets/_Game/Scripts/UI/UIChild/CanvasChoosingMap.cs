using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasChoosingMap : UICanvas
{
    private RawImage currrentImg;
    [SerializeField] Text levelText;
    [SerializeField] Text rewardCoin;
    [SerializeField] Text rewardStar;
    [SerializeField] Text costEnergy;
    public override void Setup()
    {
        base.Setup();
        GameManager.ChangeState(GameState.Map);
    }
    public void SetValueLevel(int level)
    {
        levelText.text = "Mission " + level.ToString();
    }    
    public void SetValueCoin(int coin)
    {
        rewardCoin.text = coin.ToString();
    }    
    public void SetValueStar(int star)
    {
        rewardStar.text = star.ToString();
    }    
    public void SetValueCostEnergy(int energy)
    {
        costEnergy.text = energy.ToString();
    }    
    public void SetValueForLevel(int level, int coin, int star, int energy)
    {
        levelText.text = "Mission " + level.ToString();
        rewardCoin.text = coin.ToString();
        rewardStar.text = star.ToString();
        costEnergy.text = energy.ToString();
    }    
    public void PlayGame()
    {
        CanvasMainMenu canvas = UIManager.Ins.GetUI<CanvasMainMenu>();
        UIManager.Ins.CloseAll();
        
        LevelManager.Ins.SetCurrentIdBuilding(LevelManager.Ins.CurrentBuildingLevel.IdLevel);
        //UIManager.Ins.OpenUI<CanvasGameplay>();
        if (canvas.CountEnergy >= LevelManager.Ins.CurrentBuildingLevel.EnergyCost)
        {
            canvas.MinusEnergy(LevelManager.Ins.CurrentBuildingLevel.EnergyCost);
            UIManager.Ins.OpenUI<CanvasGameplay>();
        }
    }    
    public void CloseUI()
    {
        GameManager.ChangeState(GameState.MainMenu);
        Close(0);
        
    }    
}
