using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : GameUnit
{
    [SerializeField] Image healthBar;
    private float health;
    private float maxHealth;
    public bool isDestroyedd => health <= 0;
    private bool isTakeDamage;
    [SerializeField] CanvasHealthBar canvasHealth;
    [SerializeField] UICombatText combatTextUI;
    private float currentHealth;
    public float CurrentHealth => currentHealth;
    private float timeTakeDamage;
    [SerializeField] ParticleSystem smokeEffect;
    [SerializeField] List<ParticleSystem> fireEffect;
    [SerializeField] Transform spawnPosSmoke;
    [SerializeField] Transform spawnPosFire;
    int countEffectSmoke;
    int countEffectFire;
    [SerializeField] ParticleSystem boomEffect;
    private ParticleSystem explosionEffect;
    //[SerializeField] Transform boomPos;
    private void Start()
    {
        isTakeDamage = false;
        canvasHealth.gameObject.SetActive(false);
        health = maxHealth = HP;
        healthBar.fillAmount = health / maxHealth;
        currentHealth = healthBar.fillAmount;
        timeTakeDamage = 5f;
        countEffectSmoke = 0;
        countEffectFire = 0;
    }
    public virtual void OnInit()
    {

    }
    public virtual void Update()
    {
        if(!isDestroyedd)
        {
            if(countEffectSmoke <= 0)
            {
                if (currentHealth >= 0.5f && currentHealth <= 0.8f)
                {
                    SpawnSmoke(spawnPosSmoke.position);
                    if(countEffectSmoke < 2)
                    {
                        countEffectSmoke++;
                    }    
                    
                }
            }    
            if(countEffectFire <= 0)
            {
                if (currentHealth < 0.5f)
                {
                    SpawnFire(spawnPosFire.position);
                    
                    if (countEffectFire < 2)
                    {
                        countEffectFire++;
                    }    
                    
                }
            }       
        }
        else
        {
            DespawnEffect();
        }
        if (timeTakeDamage > 0)
        {
            timeTakeDamage -= Time.deltaTime;
        }    
        else
        {
            canvasHealth.gameObject.SetActive(false);
        }    
    }
    public void DespawnEffect()
    {       
        SpawnBoom(TF.position);
        OnDespawn();
    }    
    protected void OnDespawn()
    {
        //ParticlePool.Release(boomEffect);
        ParticlePool.Release(smokeEffect);
        for (int i = 0; i < fireEffect.Count; i++)
        {
            ParticlePool.Release(fireEffect[i]);
        }
        EntitiesManager.Ins.OnDespawnTower(this);
    }
    public virtual void OnHit(float damage)
    {
        if (GameManager.IsState(GameState.Gameplay))
        {
            timeTakeDamage = 5f;
            canvasHealth.gameObject.SetActive(true);
            health -= damage;
            healthBar.fillAmount = health / maxHealth;
            currentHealth = healthBar.fillAmount;
            
            SpawnCombatText(TF.position, $"-{damage}", Color.red);
        }
        
    }

    public void SpawnSmoke(Vector3 spawnPos)
    {
        ParticlePool.Play(smokeEffect, spawnPos, Quaternion.identity);
    }    
    public void SpawnFire(Vector3 spawnPos)
    {
        for(int i = 0; i < fireEffect.Count; i++)
        {
            ParticlePool.Play(fireEffect[i], spawnPos, Quaternion.identity);
        }    
        
    }    
    public void SpawnBoom(Vector3 spawnPos)
    {
        //ParticlePool.Play(boomEffect, spawnPos, Quaternion.identity);
        if(explosionEffect != null)
        {
            Destroy(explosionEffect.gameObject);
        }    
        explosionEffect = Instantiate(boomEffect, spawnPos, Quaternion.identity);
    }    
    public void SpawnCombatText(Vector3 tf, string text, Color color)
    {
        UICombatText combatText = Instantiate(combatTextUI, TF);
        combatText.OnInit(tf, text, color);
    }
}
