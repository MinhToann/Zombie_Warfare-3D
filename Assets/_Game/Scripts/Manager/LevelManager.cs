using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LevelManager : Singleton<LevelManager>
{
    //[SerializeField] Level[] levels;
    //public Level currentLevel;
    [SerializeField] List<LevelSO> listLevels = new List<LevelSO>();
    private LevelSO currentLevel;
    public LevelSO CurLevel => currentLevel;
    int level = 0;
    [SerializeField] Transform parentBG;
    public bool IsEndAllWave => currentLevel.IsEndAllWave;
    private bool isWin;
    public bool IsWin => isWin;
    public void Start()
    {
        OnLoadLevel(level);
        OnInit();
    }

    //khoi tao trang thai bat dau game
    public void OnInit()
    {
        isWin = false;
        EntitiesManager.Ins.OnInit();
        //player.OnInit();
    }

    //goi khi bat dau gameplay
    public void OnPlay()
    {

    }

    //reset trang thai khi ket thuc game
    public void OnDespawn()
    {
        //player.OnDespawn();
        //for (int i = 0; i < bots.Count; i++)
        //{
        //    bots[i].OnDespawn();
        //}

        //bots.Clear();
        //SimplePool.CollectAll();
    }

    //tao prefab level moi
    public void OnLoadLevel(int level)
    {
        //if (currentLevel != null)
        //{
        //    Destroy(currentLevel.gameObject);
        //}
        //currentLevel = Instantiate(listLevels[level]);
        if (level < listLevels.Count)
        {
            currentLevel = listLevels[level];
            currentLevel.CreateLevel(parentBG);
            currentLevel.OnLoadWave(0);
            Debug.Log("curren wave: " + currentLevel.CurrentNumberWave);
        }
        
    }

    public void OnNextWave()
    {
        if (EntitiesManager.Ins.IsEmpty)
        {
            currentLevel.NextWave();

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
    }

    public void OnLose()
    {
        isWin = false;
        UIManager.Ins.OpenUI<CanvasLose>();
    }

    public void CollectItem(Item item)
    {

    }

    public void OnNextLevel()
    {
        OnDespawn();
        OnLoadLevel(++level);
        OnInit();
    }

}
