using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : Character
{
    private int countWave = 0;
    public override void OnInit()
    {
        base.OnInit();
        SetOwnTown(EntitiesManager.Ins.CurrentCar);
    }
    public override void Update()
    {
        base.Update();
        if (GameManager.IsState(GameState.Gameplay))
        {
            if (!isDeath)
            {
                if (!EntitiesManager.Ins.CurrentBarrier.isDestroyed)
                {
                    if (!LevelManager.Ins.IsEndAllWave)
                    {
                        LevelManager.Ins.OnNextWave();
                    }
                }
            }
            if (EntitiesManager.Ins.CurrentBarrier.isDestroyed)
            {
                LevelManager.Ins.OnWin();
                SetTarget(null);
                SetTargetTower(null);
            }    
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
        destination = LevelManager.Ins.CurLevel.CurrentMap.TargetPosHero.position;
        base.OnWalkEnter();
        
    }
    public override void OnWalkExecute()
    {
        base.OnWalkExecute();
        if(GameManager.IsState(GameState.Win))
        {
            ChangeRunState();
        }    
    }
    public override void OnAttackExecute()
    {
        base.OnAttackExecute();
        if (GameManager.IsState(GameState.Win))
        {
            ChangeRunState();
        }
    }
    public override void OnRunEnter()
    {
        //destination = EntitiesManager.Ins.CurrentCar.SpawnPosHero.position;
        if (GameManager.IsState(GameState.Win))
        {
            SetMoveSpeed(MoveSpeed * 2);
            agent.speed = MoveSpeed;
            //TF.DORotate(new Vector3(0, 180, 0), 1.5f);
            destination = EntitiesManager.Ins.CurrentCar.SpawnPosHero.position;
            CurrentModel.TF.DORotate(new Vector3(0, -90, 0), 1.5f);
            SetDestination(destination);
            //EntitiesManager.Ins.GoIntoCar(destination, this);
        }
        base.OnRunEnter();
        
    }
    public override void OnRunExecute()
    {
        base.OnRunExecute();
        if (GameManager.IsState(GameState.Win))
        {
            if (isDestination)
            {
                OnDespawn();
            }
        }

    }
}
