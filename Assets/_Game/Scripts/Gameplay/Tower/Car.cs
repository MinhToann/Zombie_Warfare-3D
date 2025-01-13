using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Tower
{
    [SerializeField] Transform spawnPosHero;
    public Transform SpawnPosHero => spawnPosHero;
    private bool canSpawn;
    public bool CanSpawn => canSpawn;

    private void SetBoolCanSpawn()
    {
        canSpawn = true;
    }    
    public override void OnInit()
    {
        base.OnInit();
        managerSO.SetValueForTower(this, managerSO.ListCharacterSO.Count - 1);
        canSpawn = false;
        Invoke(nameof(SetBoolCanSpawn), 2f);
        //TF.DOMove(LevelManager.Ins.CurLevel.CurrentMap.PosStartCar.position, 2f).OnComplete(() =>
        //{
        //    canSpawn = true;
        //});
        //TF.Translate(LevelManager.Ins.CurLevel.CurrentMap.PosStartCar.position * MoveSpeed * Time.deltaTime);
        //
    }

    private void Update()
    {
        if(GameManager.IsState(GameState.Gameplay))
        {
            TF.position = Vector3.MoveTowards(TF.position, LevelManager.Ins.CurLevel.CurrentMap.PosStartCar.position, MoveSpeed * Time.deltaTime);   
        }    
        if(GameManager.IsState(GameState.Win))
        {
            if(EntitiesManager.Ins.ListHero.Count <= 0)
            {
                //SetMoveSpeed(MoveSpeed * 2);
                TF.position = Vector3.MoveTowards(TF.position, LevelManager.Ins.CurLevel.CurrentMap.FinishPos.position, MoveSpeed * Time.deltaTime);
            }    
            
        }    
    }
    private void CollideZombie(Character zombie)
    {
        if(zombie != null && !zombie.isDeath)
        {
            zombie.OnHit(Damage);
        }    
    }    
    private void OnTriggerEnter(Collider other)
    {
        if(Cache.CollideWithCharacter(other))
        {
            if(Cache.CollideWithCharacter(other) is Zombie)
            {
                CollideZombie(Cache.CollideWithCharacter(other));
            }    
            
        }    
    }
}
