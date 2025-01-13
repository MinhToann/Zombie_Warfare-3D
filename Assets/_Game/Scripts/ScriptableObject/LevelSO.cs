using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Create New Level")]
public class LevelSO : ScriptableObject
{
    [SerializeField] Map map;
    [SerializeField] BackgroundMap backgroundMap;
    private Map curMap;
    public Map CurrentMap => curMap;
    private BackgroundMap bgMap;
    [SerializeField] List<Waves> wavesInLevel = new List<Waves>();
    public List<Waves> WavesInLevel => wavesInLevel;
    private Waves currentWave;
    public Waves CurrentWave => currentWave;
    private int curNumberWave;// = 0;
    public int CurrentNumberWave => curNumberWave;
    public bool IsEndAllWave => curNumberWave >= wavesInLevel.Count;

    public void CreateLevel(Transform parentBG)
    {
        if(curMap != null)
        {
            Destroy(curMap.gameObject);
        }
        if(bgMap != null)
        {
            Destroy(bgMap.gameObject);
        }
        curMap = Instantiate(map);
        bgMap = Instantiate(backgroundMap, parentBG);
        curNumberWave = 0;
        Debug.Log("current wave: " + curNumberWave);
        Debug.Log("wave count: " + wavesInLevel.Count);
        //OnLoadWave(curNumberWave);
    }
    public void OnLoadWave(int index)
    {
        if(index < wavesInLevel.Count)
        {
            currentWave = wavesInLevel[index];           
        }
    }
    public void NextWave()
    {
        if(EntitiesManager.Ins.IsEmpty)
        {
            OnLoadWave(++curNumberWave);
            
        }                  
        UIManager.Ins.GetUI<CanvasGameplay>().SpawnWaveText();
        EntitiesManager.Ins.DelaySpawnZombies();
        EntitiesManager.Ins.SetBoolEmptyZombie(false);
    }    
}
