using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Character
{
    public override void OnInit()
    {
        base.OnInit();
        SetOwnTown(EntitiesManager.Ins.CurrentBarrier);
    }
    public override void Update()
    {
        base.Update();
        if (GameManager.IsState(GameState.Gameplay))
        {
            if (EntitiesManager.Ins.CurrentCar.isDestroyed)
            {
                LevelManager.Ins.OnLose();
            }
        }    
        if(GameManager.IsState(GameState.Win))
        {
            towerTarget = null;
        }    
    }
    public override void OnDeath()
    {
        
        base.OnDeath();
        
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        
    }
    public override void Attack()
    {
        base.Attack();
    }
    public override void OnWalkEnter()
    {
        destination = LevelManager.Ins.CurLevel.CurrentMap.TargetPosZombie.position;
        base.OnWalkEnter();
        
    }
    public override void OnWalkExecute()
    {
        base.OnWalkExecute();
    }
    public override void OnDeathEnter()
    {
        base.OnDeathEnter();
        
    }
}
