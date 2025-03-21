using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] ManagerSO managerSO;
    [SerializeField] Text timerText;
    [SerializeField] TextMeshProUGUI textCountEnergy;
    protected float elapsedTime = 120f;
    private int countMaxEnergy = 10;
    private int countEnergy = 10;
    public int CountMaxEnergy => countMaxEnergy;
    public int CountEnergy => countEnergy;
    private int totalCoin;
    private int totalStar;
    [SerializeField] Text coinText;
    [SerializeField] Text starText;
    [SerializeField] Image fillEnergy;
    [SerializeField] Image coinReward;
    [SerializeField] Image starReward;
    [SerializeField] Transform parentRewards;
    [SerializeField] Transform coinPos;
    [SerializeField] Transform starPos;

    public override void Setup()
    {
        base.Setup();
        GameManager.ChangeState(GameState.MainMenu);
        //totalCoin = PlayerPrefs.GetInt(Constant.COIN, 0);
        //SetCoinText(totalCoin);
        //totalStar = PlayerPrefs.GetInt(Constant.STAR, 0);
        //SetStarText(totalStar);
        SaveLoadMapHandler.Ins.LoadMap();
        LevelManager.Ins.DeactiveBackground();
    }
    public override void Open()
    {
        base.Open();
        fillEnergy.fillAmount = countEnergy / countMaxEnergy;
        //TakeTheReward();
    }
    public override void Update()
    {
        base.Update();
        //textCountEnergy.text = countEnergy.ToString() + "/" + countMaxEnergy.ToString();

        //if (countEnergy < countMaxEnergy)
        //{
        //    //elapsedTime = 120f;
        //    if (elapsedTime > 0)
        //    {
        //        elapsedTime -= Time.deltaTime;
        //        float clampedCountdown = Mathf.Max(0, elapsedTime);
        //        int minutes = Mathf.FloorToInt(clampedCountdown / 60);
        //        int seconds = Mathf.FloorToInt(clampedCountdown % 60);
        //        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //    }
        //    else
        //    {
        //        countEnergy++;
        //        elapsedTime = 120f;
        //    }
        //}    
        //else
        //{
        //    elapsedTime = 0;
        //    timerText.text = string.Format("{0:00}:{1:00}", 0, 0);
        //}    
    }
    public void OpenUpgradeUI()
    {
        UIManager.Ins.OpenUI<CanvasCharacterInfo>();
    }    
    public void SetTextEnergy(int energy)
    {
        textCountEnergy.text = energy.ToString() + "/" + countMaxEnergy.ToString();
    }    
    public void MinusEnergy(int energy)
    {
        countEnergy -= energy;
    }    
    public void OpenManageTeamUI()
    {
        UIManager.Ins.OpenUI<CanvasManageTeam>();
    }    
    //private void TakeTheReward()
    //{
    //    if (LevelManager.Ins.IsWin)
    //    {
    //        float delay = 0f;
    //        for(int i = 0; i < managerSO.ListLevelSO.Count; i++)
    //        {
    //            if(LevelManager.Ins.CurrentIdBuilding == managerSO.ListLevelSO[i].ID)
    //            {
    //                for(int m = 0; m < managerSO.ListLevelSO[i].RewardCoin; m++)
    //                {
    //                    Image coin = Instantiate(coinReward, parentRewards);
    //                    coin.transform.localPosition = Vector3.zero;
    //                    coin.transform.DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
    //                    coin.transform.DOMove(coinPos.position, 1f).SetDelay(delay).SetEase(Ease.InBack).OnComplete(() =>
    //                    {
    //                        Destroy(coin.gameObject);
    //                        totalCoin++;
    //                        PlayerPrefs.SetInt(Constant.COIN, totalCoin);
    //                        SetCoinText(totalCoin);
    //                    });
    //                }
    //                for (int n = 0; n < managerSO.ListLevelSO[i].RewardStar; n++)
    //                {
    //                    Image star = Instantiate(starReward, parentRewards);
    //                    star.transform.localPosition = Vector3.zero;
    //                    star.transform.DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
    //                    star.transform.DOMove(starPos.position, 1f).SetDelay(delay).SetEase(Ease.InBack).OnComplete(() =>
    //                    {
    //                        Destroy(star.gameObject);
    //                        totalStar++;
    //                        PlayerPrefs.SetInt(Constant.STAR,totalStar);
    //                        SetStarText(totalStar);
    //                    });
    //                    delay += 0.1f;
    //                }                 
    //            }    
    //        }
    //        LevelManager.Ins.SetWinState(false);
    //    }
    //}
    private void SetCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }    
    private void SetStarText(int star)
    {
        starText.text = star.ToString();
    }    
}
