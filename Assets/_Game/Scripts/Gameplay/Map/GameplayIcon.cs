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
    public void OnInit()
    {
        SetManaText(this.Mana);
        SetCooldown(0);
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
    }
    public void SpawnHero()
    {
        CanvasGameplay canvas = UIManager.Ins.GetUI<CanvasGameplay>();
        if(cooldownFill.fillAmount <= 0 && EntitiesManager.Ins.CurrentCar.CanSpawn && canvas.CurrentMana >= this.Mana)
        {
            EntitiesManager.Ins.SpawnHeroes(this);
            canvas.MinusMana((int)this.Mana);
            //canvas.SetValueSliderMana((int)(canvas.CurrentMana - this.Mana));
            SetCooldown(1);
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
