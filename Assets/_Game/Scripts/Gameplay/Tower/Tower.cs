using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : GameUnit
{
    [SerializeField] Image healthBar;
    private float health;
    private float maxHealth;
    public bool isDestroyed => health <= 0;
    private bool isTakeDamage;
    [SerializeField] CanvasHealthBar canvasHealth;
    private void Start()
    {
        isTakeDamage = false;
        canvasHealth.gameObject.SetActive(false);
        health = maxHealth = HP;
        healthBar.fillAmount = health / maxHealth;
    }
    public virtual void OnInit()
    {
        //health = maxHealth = HP;
        //healthBar.fillAmount = health / maxHealth;
    }
    private void Update()
    {

    }
    protected void OnDespawn()
    {
        EntitiesManager.Ins.OnDespawnTower(this);
    }
    public void OnHit(float damage)
    {
        if (GameManager.IsState(GameState.Gameplay))
        {
            isTakeDamage = true;
            canvasHealth.gameObject.SetActive(true);
            health -= damage;
            healthBar.fillAmount = health / maxHealth;
            Invoke(nameof(SetBoolTakeDamage), 1f);
            if(isDestroyed)
            {
                OnDespawn();
            }
        }
        
    }
    private void SetBoolTakeDamage()
    {
        isTakeDamage = false;
        Invoke(nameof(DelayTurnOffHealthBar), 2f);
    }
    private void DelayTurnOffHealthBar()
    {
        if(!isTakeDamage)
        {
            canvasHealth.gameObject.SetActive(false);
        }    
        
    }
}
