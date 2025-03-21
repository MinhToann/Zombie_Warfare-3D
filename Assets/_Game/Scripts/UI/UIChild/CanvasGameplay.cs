using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameplay : UICanvas
{
    [SerializeField] GameplayIcon icon;
    [SerializeField] ManagerSO managerSO;
    [SerializeField] Transform parentIcon;
    [SerializeField] WaveText waveText;
    private WaveText currentWaveText;
    [SerializeField] Image sliderMana;
    private float currentMana;
    public float CurrentMana => currentMana;
    private float maxMana;
    [SerializeField] Text manaText;
    [SerializeField] Image sliderBlood;
    private float currentBlood;
    private float maxBlood;
    [SerializeField] Text bloodText;
    private float waveTime;
    public float WaveTime => waveTime;
    private float timer;
    public float Timer => timer;
    public override void Setup()
    {
        base.Setup();
        GameManager.ChangeState(GameState.Gameplay);
        SaveLoadMapHandler.Ins.ClearMap();
        LevelManager.Ins.OnLoadLevel(LevelManager.Ins.CurrentBuildingLevel.IdLevel - 1);
        waveTime = LevelManager.Ins.CurrentWave.TimeInWave;
        LevelManager.Ins.SetOriginalCameraOffset();
        CreateGameIcon();
        
        currentMana = 28f;
        maxMana = 100f;
        currentBlood = 0f;
        maxBlood = 100f;
    }
    public override void Open()
    {
        base.Open();
        EntitiesManager.Ins.OnInit();
        SpawnWaveText();
        Debug.Log("Wave time: " + waveTime);
    }
    public override void Update()
    {
        base.Update();
        if (waveTime > 0)
        {
            waveTime -= Time.deltaTime;
            timer = waveTime;
        }    
        
        SetValueSliderMana((int)currentMana);
        SetValueSliderBlood((int)currentBlood);
        if (currentMana < maxMana)
        {
            currentMana += Time.deltaTime;           
        }    
        if(GameManager.IsState(GameState.Win))
        {
            Close(0);
        }    
    }
    private void CreateGameIcon()
    {
        if(LevelManager.Ins.ListIcon.Count <= 0)
        {
            for (int i = 0; i < managerSO.ListCharacterSO.Count - 7; i++)
            {
                GameplayIcon newIcon = Instantiate(icon, parentIcon);
                managerSO.SetValueForIcon(newIcon, i);
                newIcon.OnInit();
                LevelManager.Ins.AddIcon(newIcon);
                
            }
        }
        
    }
    public void SpawnWaveText()
    {
        if(currentWaveText != null)
        {
            Destroy(currentWaveText.gameObject);
        }
        currentWaveText = Instantiate(waveText, TF);
        currentWaveText.OnInit();
    }
    public void MinusMana(int value)
    {
        currentMana -= value;
    }    
    public void SetValueSliderMana(int value)
    {
        sliderMana.fillAmount = value / maxMana;
        manaText.text = value.ToString();
    }    
    public void SetValueSliderBlood(int value)
    {
        sliderBlood.fillAmount = value / maxBlood;
        bloodText.text = value.ToString();
    }    
    public void SetWaveTime(float waveTime)
    {
        this.waveTime = waveTime;
    }    
}
