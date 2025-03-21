using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLevel : MonoBehaviour
{
    //private int indexLayer;
    //[SerializeField] private int idLevel;
    //[SerializeField] private int rewardCoin;
    //[SerializeField] private int rewardStar;
    //[SerializeField] private int energyCost;
    //public int IdLevel => idLevel;
    //public int RewardCoin => rewardCoin;
    //public int RewardStar => rewardStar;
    //public int EnergyCost => energyCost;
    //[SerializeField] ChoosingOutline outline;
    //private ChoosingOutline currentOutline;
    //[SerializeField] StarCanvas stars;
    //private StarCanvas currentCanvasStar;
    //private Transform tf;
    //public Transform TF
    //{
    //    get
    //    {
    //        if(tf == null)
    //        {
    //            tf = transform;
    //        }
    //        return tf;
    //    }
    //}
    //private void Start()
    //{
    //    OnInit();
    //}
    //public void OnInit()
    //{
    //    ShowStar();
    //}    
    //private void OnMouseDown()
    //{
    //    if(GameManager.IsState(GameState.MainMenu))
    //    {
    //        //if (gameObject.layer == LayerMask.NameToLayer(Constant.LAYER_UNLOCKED_LEVEL))
    //        //{              
    //        //    LevelManager.Ins.SetCurrentBuilding(this);
    //        //    ShowOutline();
    //        //    UIManager.Ins.OpenUI<CanvasChoosingMap>();
    //        //    UIManager.Ins.GetUI<CanvasChoosingMap>().SetValueForLevel(idLevel, rewardCoin, rewardStar, energyCost);
    //        //}               
    //    }      
    //}
    //public void DeactiveOutline()
    //{
    //    if (currentOutline != null)
    //    {
    //        Destroy(currentOutline.gameObject);
    //    }
    //}    
    //public void ShowOutline()
    //{
    //    if(currentOutline != null)
    //    {
    //        Destroy(currentOutline.gameObject);
    //    }
    //    currentOutline = Instantiate(outline, TF);
    //}    
    //public void ShowStar()
    //{
    //    if(currentCanvasStar != null)
    //    {
    //        Destroy(currentCanvasStar.gameObject);
    //    }
    //    currentCanvasStar = Instantiate(stars, TF);
    //    currentCanvasStar.ChangeColorStar();
    //}    
    //public void SetStateLevel(string state)
    //{
    //    int LayerIgnoreRaycast = LayerMask.NameToLayer(state);
    //    gameObject.layer = LayerIgnoreRaycast;
    //}    
    //public void SetIndexLevel(int id)
    //{
    //    idLevel = id;
    //}    
    //public void SetRewardCoin(int coin)
    //{
    //    rewardCoin = coin;
    //}    
    //public void SetRewardStar(int star)
    //{
    //    rewardStar = star;
    //}    
    //public void SetEnergyCost(int cost)
    //{
    //    energyCost = cost;
    //}    
}
