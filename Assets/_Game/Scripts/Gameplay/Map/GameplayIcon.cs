using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayIcon : GameUnit
{
    [SerializeField] private Image icon;
    LevelManager levelManager;
    public Image Icon => icon;
    [SerializeField] Image cooldownFill;
    [SerializeField] Text manaText;
    public Text ManaText => manaText;
    [SerializeField] Image imgCantBuy;
    public void OnInit()
    {
        SetManaText(this.Mana);
        SetCooldown(0);
        if (PoolTypeObject == PoolType.Heroes)
        {
            Color blueColor = new Color(51f / 255f, 172f / 255f, 255f / 255f, 255f / 255f);
            manaText.color = blueColor;
        }
    }
    public void SetIcon(Sprite icon)
    {
        this.icon.sprite = icon;
        
    }
    private void Update()
    {
        if(cooldownFill.fillAmount > 0)
        {
            cooldownFill.fillAmount -= 1.0f/this.CooldownSpawn  * Time.deltaTime;
        }
        HandleSpawnCharacter();
    }
    private void HandleSpawnCharacter()
    {
        CanvasGameplay canvas = UIManager.Ins.GetUI<CanvasGameplay>();
        if(canvas.CurrentMana < this.Mana)
        {
            imgCantBuy.gameObject.SetActive(true);
        }
        else
        {
            imgCantBuy.gameObject.SetActive(false);
        }
    }
    public void SpawnHero()
    {
        if(GameManager.IsState(GameState.Gameplay))
        {
            CanvasGameplay canvas = UIManager.Ins.GetUI<CanvasGameplay>();
            if (cooldownFill.fillAmount <= 0 && EntitiesManager.Ins.CurrentCar.CanSpawn && canvas.CurrentMana >= this.Mana)
            {
                EntitiesManager.Ins.SpawnHeroes(this);
                canvas.MinusMana((int)this.Mana);
                //canvas.SetValueSliderMana((int)(canvas.CurrentMana - this.Mana));
                SetCooldown(1);
                
            }
        }    
               
    }    
    public void ManageTeam()
    {
        if(GameManager.IsState(GameState.ManageTeam))
        {
            CanvasManageTeam canvas = UIManager.Ins.GetUI<CanvasManageTeam>();
            canvas.ManageCharacter(this);
        }    
        
    }    
    private void SetCooldown(float value)
    {
        cooldownFill.fillAmount = value;
        
    }       
    private void SetManaText(float mana)
    {
        manaText.text = mana.ToString();
    }    
}
