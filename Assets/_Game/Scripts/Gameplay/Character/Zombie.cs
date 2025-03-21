using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Character
{
    protected bool isWalking;
    protected bool isRunning;
    protected float timeMoving = 3.5f;
    protected float timeWaiting = 2f;
    public override void OnInit()
    {
        base.OnInit();
        SetOwnTown(EntitiesManager.Ins.CurrentBarrier);
        TF.localScale = Vector3.one;
    }
    public override void Update()
    {
        base.Update();
        if (GameManager.IsState(GameState.Gameplay))
        {
            if (EntitiesManager.Ins.CurrentCar == null)//EntitiesManager.Ins.CurrentCar.isDestroyedd || )
            {
                LevelManager.Ins.OnLose();
            }
        }    
        if(GameManager.IsState(GameState.Lose))// || GameManager.IsState(GameState.Win))
        {
            ChangeState(Constant.IDLE_STATE);
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
    public override void OnIdleEnter()
    {
        timeWaiting = 2f;
        base.OnIdleEnter();
    }
    public override void OnIdleExecute()
    {
        base.OnIdleExecute();
        //if(timeWaiting > 0)
        //{
        //    timeWaiting -= Time.deltaTime;
        //}    
        //else
        //{
        //    if(isWalking)
        //    {
        //        ChangeWalkState();
        //    }    
        //    else if(isRunning)
        //    {
        //        ChangeRunState();
        //    }    
        //}    
    }
    public override void OnWalkEnter()
    {
        destination = LevelManager.Ins.CurrentMap.TargetPosZombie.position;
        //isWalking = true;
        //isRunning = false;
        //timeMoving = 3.5f;
        //timeWaiting = 2f;
        base.OnWalkEnter();
        
    }
    public override void OnWalkExecute()
    {
        base.OnWalkExecute();
        //if (timeMoving >= 0)
        //{
        //    timeMoving -= Time.deltaTime;
        //}
        //else
        //{
        //    ChangeState(Constant.IDLE_STATE);
        //}
    }
    public override void OnRunEnter()
    {
        destination = LevelManager.Ins.CurrentMap.TargetPosZombie.position;
        //isWalking = false;
        //isRunning = true;
        //timeMoving = 3.5f;
        base.OnRunEnter();
        
    }
    public override void OnRunExecute()
    {
        base.OnRunExecute();
        //if (timeMoving >= 0)
        //{
        //    timeMoving -= Time.deltaTime;
        //}
        //else
        //{
        //    ChangeState(Constant.IDLE_STATE);
        //}
    }
    public override void OnStandUpExecute()
    {
        base.OnStandUpExecute();
        Debug.Log("ok");
        //ChangeState(Constant.WALK_STATE);
    }
    public override void OnAttackExecute()
    {
        base.OnAttackExecute();
        
    }
    public override void OnDeathEnter()
    {
        base.OnDeathEnter();
        Invoke(nameof(OnDespawn), 3f);
    }
    public override void OnFlyingBackDeathEnter()
    {
        base.OnFlyingBackDeathEnter();
        Invoke(nameof(OnDespawn), 3f);
    }
}
