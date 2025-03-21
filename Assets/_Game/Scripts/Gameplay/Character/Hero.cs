using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : Character
{
    private int countWave = 0;
    [SerializeField] LaurelWreath laurel;
    private LaurelWreath currentLaurel;
    public LaurelWreath CurrentLaurel => currentLaurel;
    public override void OnInit()
    {
        base.OnInit();
        SetOwnTown(EntitiesManager.Ins.CurrentCar);
        //ResetCharacter();
    }
    public override void Update()
    {
        base.Update();
        if (GameManager.IsState(GameState.Gameplay))
        {         
            if (EntitiesManager.Ins.CurrentBarrier == null)
            {
                LevelManager.Ins.OnWin();
            }
        }
    }

    public void ResetCharacter()
    {
        if(currentLaurel != null)
        {
            Destroy(currentLaurel.gameObject);
        }    
    }
    public void CreateLaurel()
    {
        if(currentLaurel != null)
        {
            Destroy(currentLaurel.gameObject);
        }
        currentLaurel = Instantiate(laurel, TF);
    }    
    public override void OnDeath()
    {
        base.OnDeath();
    }
    private void BecomeZombie()
    {
        EntitiesManager.Ins.ChangeHeroToZombie();
        OnDespawn();
        //Vector3 yPos = CurrentModel.TF.position;
        //yPos.y = -1;
        //CurrentModel.TF.position = yPos;    
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
        destination = LevelManager.Ins.CurrentMap.TargetPosHero.position;
        base.OnWalkEnter();

    }
    public override void OnWalkExecute()
    {
        base.OnWalkExecute();
        
    }
    public override void OnAttackExecute()
    {            
        base.OnAttackExecute();
        if (timer <= 0)
        {
            if (target == null || (target != null && target.isDeath))
            {
                ChangeWalkState();
            }
        }
    }
    public override void OnRunEnter()
    {
        if (GameManager.IsState(GameState.Win))
        {
            destination = EntitiesManager.Ins.CurrentCar.SpawnPosHero.position;
            SetMoveSpeed(MoveSpeed * 3f);
            agent.speed = MoveSpeed;          
            CurrentModel.TF.DORotate(new Vector3(0, -90, 0), 1f);
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
                StopSetDestination();
                OnDespawn();
            }
        }

    }
    public override void OnDeathEnter()
    {
        base.OnDeathEnter();
        Invoke(nameof(BecomeZombie), 2.9f);
    }
}
