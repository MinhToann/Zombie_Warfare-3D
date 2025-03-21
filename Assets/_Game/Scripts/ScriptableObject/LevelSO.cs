using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations;


[CreateAssetMenu(menuName = "Create New Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] int id;
    [SerializeField] int rewardCoin;
    [SerializeField] int rewardStar;
    [SerializeField] int energyCost;

    [SerializeField] List<Waves> wavesInLevel = new List<Waves>();
    public List<Waves> WavesInLevel => wavesInLevel;
    public int ID => id;
    public int RewardCoin => rewardCoin;
    public int RewardStar => rewardStar;
    public int EnergyCost => energyCost;
    
    public void SetValueBuilding(GridObjectOnMap building)
    {
        building.SetIndexLevel(id);
        building.SetRewardCoin(rewardCoin);
        building.SetRewardStar(rewardStar);
        building.SetEnergyCost(energyCost);
    }    
    
}
