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
    public override void Setup()
    {
        base.Setup();
        GameManager.ChangeState(GameState.Gameplay);
        CreateGameIcon();
        SpawnWaveText();
        currentMana = 50f;
        maxMana = 100f;
        currentBlood = 0f;
        maxBlood = 100f;
        //SetValueSliderMana(currentMana);
    }
    public override void Open()
    {
        base.Open();
    }
    private void Update()
    {
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
        for(int i = 0; i < managerSO.ListCharacterSO.Count - 4; i++)
        {
            GameplayIcon newIcon = Instantiate(icon, parentIcon);
            managerSO.SetValueForIcon(newIcon, i);
            newIcon.OnInit();
            //newIcon.OnInit(i);
        }
    }
    public void SpawnWaveText()
    {
        if(currentWaveText != null)
        {
            Destroy(currentWaveText.gameObject);
        }
        currentWaveText = Instantiate(waveText, TF);
        //
        currentWaveText.OnInit();
        //currentWaveText.SetWaveText(LevelManager.Ins.CurLevel.CurrentNumberWave + 1);
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
}
