using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class Character : GameUnit
{
    private string currentAnim;
    [SerializeField] protected Animator anim;
    [SerializeField] CharacterModel model;
    [SerializeField] CharacterModel currentModel;
    public CharacterModel CurrentModel => currentModel;
    protected IState<Character> state;
    [SerializeField] protected Tower towerTarget;
    public Tower TowerTarget => towerTarget;
    [SerializeField] protected Character target;
    public Character Target => target;
    [SerializeField] List<Character> listTarget = new List<Character>();
    //private CharacterModel curModel;
    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask towerMask;
    [SerializeField] protected NavMeshAgent agent;
    protected Vector3 destination;
    public bool isDestination => Vector3.Distance(TF.position, destination + (TF.position.y - destination.y) * Vector3.up) < 0.1f;
    private bool isMoving;
    [SerializeField] Image healthBar;
    private float health;
    private float maxHealth;
    public bool isDeath => health <= 0;

    [SerializeField] WeaponBase currentWeapon;
    private bool isAttack;
    public bool IsAttack => isAttack;
    protected float cooldownAttack;
    public float CoolDownAttack => cooldownAttack;
    protected float delayWalk;
    protected float timer = 0;
    [SerializeField] Collider chaCollider;
    public Collider CharacterCollider => chaCollider;
    RaycastHit hit;
    [SerializeField] Transform shootRay;
    [SerializeField] protected Tower ownTown;
    public Tower OwnTown => ownTown;
    private bool isTakeDamage;
    public bool IsTakeDamage => isTakeDamage;
    [SerializeField] CanvasHealthBar canvasHealth;
    private CanvasHealthBar currentHealthBar;
    private float normalMS;
    public float NormalMS => normalMS;
    float timerStandup = 7f;
    float timeMinus;
    [SerializeField] Blood blood;
    private Blood chaBlood;
    [SerializeField] UICombatText combatTextUI;
    [SerializeField] Transform laydownPos;
    private float deadTimer;

    public virtual void OnInit()
    {
        if (currentWeapon != null)
        {
            currentWeapon.OnInit(this);
            currentWeapon.SetOwner(this);
        }
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].name.Contains("Attack"))
            {
                cooldownAttack = clips[i].length / AtkSpeed;
            }
            if (clips[i].name.Contains("Walk") || clips[i].name.Contains("Run"))
            {
                delayWalk = clips[i].length / MoveSpeed;
            }    
        }
        anim.SetFloat(Constant.ATTACK_SPEED, AtkSpeed);
        anim.SetFloat(Constant.MOVE_SPEED, MoveSpeed);
        //canvasHealth.gameObject.SetActive(false);
        InitHealthBar();
        isTakeDamage = false;
        target = null;
        towerTarget = null;
        isAttack = false;
        health = maxHealth = HP;
        healthBar.fillAmount = health / maxHealth;
        normalMS = MoveSpeed;
        //agent.speed = MoveSpeed;
        ChangeWalkState();

    }
    public void InitHealthBar()
    {
        if(currentHealthBar != null)
        {
            Destroy(currentHealthBar.gameObject);
        }
        currentHealthBar = Instantiate(canvasHealth, TF);
        currentHealthBar.gameObject.SetActive(false);
    }    
    public virtual void SetDirection(Vector3 tf)
    {
        TF.forward = tf;
    }

    public virtual void Update()
    {
        if(!isDeath)
        {
            TF.rotation = Quaternion.identity;
            agent.speed = MoveSpeed;
            if (state != null)
            {
                state.OnExecute(this);
            }
        }    
        if (GameManager.IsState(GameState.Gameplay))
        {
            if (EntitiesManager.Ins.CurrentBarrier != null)
            {
                if (!LevelManager.Ins.IsEndAllWave)
                {
                    LevelManager.Ins.OnNextWave();
                }
            }
            if (target != null)
            {
                if (target.isDeath)
                {
                    target = null;
                }
            }
            if(towerTarget != null)
            {
                if(towerTarget.isDestroyedd)
                {
                    towerTarget = null;
                }    
            }    
            if (!isDeath)
            {
                //if (target == null && towerTarget == null)
                //{
                //    DelayChangeWalkState();
                //}
                if (towerTarget == null)
                {
                    if (target == null)
                    {
                        RaycastHit[] hit = new RaycastHit[10];

                        int numberCharacter = Physics.SphereCastNonAlloc(TF.position, Sight, TF.right, hit, Sight, layerMask, QueryTriggerInteraction.UseGlobal);

                        for (int i = 0; i < numberCharacter; i++)
                        {

                            Character cha = hit[i].transform.gameObject.GetComponent<Character>();
                            if (cha != null && !cha.isDeath && cha.PoolTypeObject != PoolTypeObject)
                            {
                                bool isFacingEachOther =
                    (TF.position.x < cha.TF.position.x && PoolTypeObject == PoolType.Heroes) ||
                    (TF.position.x > cha.TF.position.x && PoolTypeObject == PoolType.Zombies);
                                if (isFacingEachOther && Vector3.Distance(TF.position, cha.TF.position) <= this.Sight)
                                {
                                    SetTarget(cha);
                                }
                            }

                        }

                    }
                    else
                    {
                        towerTarget = null;
                        if (Vector3.Distance(TF.position, target.TF.position) <= this.Sight)
                        {
                            if(timer <= 0)
                            {
                                Vector3 newDestination = target.TF.position;
                                //newDestination.x += Target.TF.forward.x - 0.5f;
                                newDestination.z = target.TF.position.z;
                                SetDestination(newDestination);
                            }    
                            
                        }
                        bool noLongerFacingEachOther =
            (TF.position.x > target.TF.position.x && PoolTypeObject == PoolType.Heroes) ||
            (TF.position.x < target.TF.position.x && PoolTypeObject == PoolType.Zombies);

                        // Clear target if no longer facing or out of sight range
                        if (noLongerFacingEachOther)
                        {
                            target = null;
                        }
                    }
                }    
                
                if(target == null)
                {
                    if (towerTarget == null)
                    {
                        RaycastHit[] hitTower = new RaycastHit[2];
                        int numberTower = Physics.SphereCastNonAlloc(TF.position, AttackRange, TF.right, hitTower, AttackRange, towerMask, QueryTriggerInteraction.UseGlobal);
                        for (int i = 0; i < numberTower; i++)
                        {
                            Tower tower = hitTower[i].transform.gameObject.GetComponent<Tower>();
                            if (tower != null && tower != ownTown)
                            {
                                if (Vector3.Distance(TF.position, tower.TF.position) < this.Sight)
                                {
                                    SetTargetTower(tower);
                                }
                            }

                        }
                    }
                    else
                    {
                        target = null;
                    }
                }    
                
            }
        }
    }
    public virtual bool CheckTargetRaycast(Vector3 forward)
    {
        Physics.Raycast(TF.position, forward, out hit, AttackRange, layerMask);
        return hit.collider != null;
    }
    public void RemoveTarget(Character target)
    {
        if (listTarget.Contains(target))
            listTarget.Remove(target);
    }
    public virtual void OnDeath()
    {
        ChangeState(Constant.DEATH_STATE);
        //Invoke(nameof(SetPosCurrentModel), 0.5f);
    }
    public virtual void OnDespawn()
    {        
        EntitiesManager.Ins.OnDeathCharacter(this);
        
    }
    public virtual void Attack()
    {
        currentWeapon.Attack();
    }
    public void ChangeAnimAttack()
    {
        ChangeAnim(Constant.ANIM_ATTACK);
        isAttack = true;
        //Invoke(nameof(SetTrueAttack), 0.2f);
    }
    public void ChangeRunState()
    {
        ChangeState(Constant.RUN_STATE);
    }    
    private void SetTrueAttack()
    {
        isAttack = true;
    }    
    public void SetOwnTown(Tower tower)
    {
        ownTown = tower;
    }
    public virtual void OnIdleEnter()
    {
        StopSetDestination();
        ChangeAnim(Constant.ANIM_IDLE);
    }   
    public virtual void OnIdleExecute()
    {
       
    }
    public virtual void OnWalkEnter()
    {
        isAttack = false;
        if (!isDestination || target == null || towerTarget == null)
        {
            SetDestination(destination); //override destination o cac class child
            ChangeAnim(Constant.ANIM_WALK);
        }
         
    }    
    public virtual void OnWalkExecute()
    {
        if (isDestination || (target != null && Vector3.Distance(TF.position, target.TF.position) <= AttackRange) || (towerTarget != null && Vector3.Distance(TF.position, towerTarget.TF.position) <= AttackRange))
        {
            
            ChangeState(Constant.ATK_STATE);
        }
    }

    public virtual void OnRunEnter()
    {
        if (!isDestination || target == null || towerTarget == null)
        {
            SetDestination(destination);
            ChangeAnim(Constant.ANIM_RUN);
        }
        
    }
    public virtual void OnRunExecute()
    {
        if(GameManager.IsState(GameState.Gameplay))
        {
            if (isDestination || (target != null && Vector3.Distance(TF.position, target.TF.position) <= AttackRange) || (towerTarget != null && Vector3.Distance(TF.position, towerTarget.TF.position) <= AttackRange))
            {
                ChangeState(Constant.ATK_STATE);
            }
        }
    }
    public virtual void OnStandUpEnter()
    { 
        ChangeAnim(Constant.ANIM_STANDUP);
        
        //Invoke(nameof(SetNormalStateForZombie), timerStandup);
    }   
    public void SetNormalStateForZombie()
    {
        CurrentModel.RotateModel(-90, 0.75f);
        OnInit();
        
    }
    public void KneelRotate()
    {
        CurrentModel.RotateModel(-90, 0.75f);
        destination = EntitiesManager.Ins.CurrentCar.SpawnPosCarHero.position;
        if (!isDestination)
        {
            SetDestination(destination); //override destination o cac class child
            ChangeAnim(Constant.ANIM_WALK);
        }
        else
        {
            SimplePool.Despawn(this);
        }    
    }    
    public virtual void OnStandUpExecute()
    {
        if (timerStandup > 0)
        {
            timerStandup -= Time.deltaTime;
        }    
    }    
    private void SetNewTarget()
    {
        float minDistance = GetDistanceClosetTarget();
        if (listTarget.Count > 0)
        {
            for (int j = 0; j < listTarget.Count; j++)
            {
                if (Vector3.Distance(TF.position, listTarget[j].TF.position) <= minDistance)// && minDistance <= AttackRange)
                {
                    if (target == null)
                        SetTarget(listTarget[j]);
                }
            }
        }
    }
    public float GetDistanceClosetTarget()
    {
        if (listTarget.Count > 0)
        {
            //SetTarget(listTarget[0]);
            float min = Vector3.Distance(TF.position, listTarget[0].TF.position);
            for (int i = 0; i < listTarget.Count; i++)
            {
                if (min > Vector3.Distance(TF.position, listTarget[i].TF.position))
                {
                    min = Vector3.Distance(TF.position, listTarget[i].TF.position);
                }
            }
            return min;
        }
        return 0;
    }
    public void SetTarget(Character target)
    {
        this.target = target;
    }
    public void SetBoolAttack(bool isAttack)
    {
        this.isAttack = isAttack;
    }
    public virtual void OnAttackEnter()
    {
        if(GameManager.IsState(GameState.Gameplay))
        {
            StopSetDestination();
            isAttack = true;
            //timer = 0;
            timer = cooldownAttack;
            ChangeAnimAttack();
        }    
         
    }
    public virtual void OnAttackExecute()
    {
          
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (target == null || (target != null && target.isDeath))
            {
                ChangeWalkState();
            }
        }
        if (towerTarget != null)
        {
            if(timer <= 0)
            {
                if (Vector3.Distance(TF.position, towerTarget.TF.position) <= AttackRange)
                {
                    if (!towerTarget.isDestroyedd)
                    {
                        OnAttackEnter();
                    }
                }                
            }
        }
        if (target != null)
        {
            if (Vector3.Distance(TF.position, target.TF.position) > Sight)
            {
                target = null;
            }
            if (timer <= 0)
            {
                if (Vector3.Distance(TF.position, target.TF.position) <= AttackRange)
                {
                    if (!target.isDeath)
                    {
                        OnAttackEnter();
                    }  
                }      
                else
                {
                    ChangeWalkState();
                }    
            }
        }
    }
    
    public void SetTargetTower(Tower tower)
    {
        towerTarget = tower;
    }
    public void ChangeWalkState()
    {
        ChangeState(Constant.WALK_STATE);
    }
    public virtual void OnDeathEnter()
    {
        StopSetDestination();
        ChangeAnim(Constant.ANIM_DEATH);
    }
    public virtual void OnDeathExecute()
    {

    }
    public virtual void OnFlyingBackDeathEnter()
    {
        StopSetDestination();
        ChangeAnim(Constant.ANIM_FLYING_BACK);
    }    
    public virtual void OnFlyingBackDeathExecute()
    {

    }    
    public void OnHit(float damage)
    {
        isTakeDamage = true;
        currentHealthBar.gameObject.SetActive(true);
        health -= damage;
        currentHealthBar.HealthImg.fillAmount = health / maxHealth;
        EntitiesManager.Ins.CreateBloodEffect(TF.position);
        SpawnCombatText(TF.position, $"-{damage}", Color.red);
        Invoke(nameof(SetBoolTakeDamage), 1f);
        BloodOnHit();
        if (isDeath)
        {
            isTakeDamage = false;
            OnDeath();
        }
    }
    private void BloodOnHit()
    {
        if(chaBlood == null)
        {
            chaBlood = Instantiate(blood, TF);
            chaBlood.transform.localPosition = Vector3.zero;
            //chaBlood.TF.DOMove(-TF.forward, 0.085f);
        }
        else
        {
            Invoke(nameof(DespawnBlood), 0.15f);
        }

    }
    private void DespawnBlood()
    {
        Destroy(chaBlood.gameObject);
    }
    private void SetBoolTakeDamage()
    {
        isTakeDamage = false;
        Invoke(nameof(DelayTurnOffHealthBar), 2f);
    }    
    private void DelayTurnOffHealthBar()
    {
        if(!isTakeDamage && (target == null || target.isDeath))
        {
            currentHealthBar.gameObject.SetActive(false);
        }    
        
    }    
    public void ChangeModel(GameObjectType characterType, float deg)
    {
        if (currentModel != null)
        {
            Destroy(currentModel.gameObject);
        }
        currentModel = Instantiate(managerSO.ChangeCharacterModel(characterType), TF);
        for (int i = 0; i < managerSO.ListCharacterSO.Count; i++)
        {
            if (characterType == managerSO.ListCharacterSO[i].GOType)
            {
                managerSO.SetValueModel(currentModel, i);
            }
        }
        managerSO.SetAnimatorCharacter(characterType, currentModel);
        currentModel.OnInit(deg);
        currentModel.SetCharacterForModel(this);
        currentWeapon = currentModel.CurrentWeapon;
        anim = currentModel.Anim;
    }
    public void SetDestination(Vector3 destination)
    {
        agent.isStopped = false;
        isMoving = true;
        ChangeAnim(Constant.ANIM_WALK);
        this.destination = destination;
        agent.SetDestination(destination);
    }
    public void StopSetDestination()
    {
        agent.isStopped = true;
        isMoving = false;
    }
    public void ChangeAnim(string newAnim)
    {
        if (currentAnim != newAnim)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = newAnim;
            anim.SetTrigger(currentAnim);
        }
    }
    public void ChangeState(IState<Character> state)
    {
        if (this.state != null)
        {
            this.state.OnExit(this);
        }
        this.state = state;
        if (this.state != null)
        {
            this.state.OnEnter(this);
        }
    }
    public void SpawnCombatText(Vector3 tf, string text, Color color)
    {
        UICombatText combatText = Instantiate(combatTextUI, TF);
        combatText.OnInit(tf, text, color);
    } 
}
