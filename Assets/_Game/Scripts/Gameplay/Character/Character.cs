using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEditor.UIElements;
using System;
public class Character : GameUnit
{
    private string currentAnim;
    [SerializeField] Animator anim;
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
    float cooldownAttack;
    public float CoolDownAttack => cooldownAttack;
    float timer = 0;
    [SerializeField] Collider chaCollider;
    public Collider CharacterCollider => chaCollider;
    RaycastHit hit;
    [SerializeField] Transform shootRay;
    [SerializeField] protected Tower ownTown;
    private bool isTakeDamage;
    public bool IsTakeDamage => isTakeDamage;
    [SerializeField] CanvasHealthBar canvasHealth;
    [SerializeField] List<AnimatorOverrideController> listAnimatorOverride = new List<AnimatorOverrideController>();
    private void Start()
    {
        //OnInit();
    }
    public virtual void OnInit()
    {
        //managerSO.SetValueModel();
        //anim = model.Anim;
        //healthBar.rectTransform.rect.width = maxHealth * healthBarWidth;
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
        }
        canvasHealth.gameObject.SetActive(false);
        isTakeDamage = false;
        target = null;
        towerTarget = null;
        isAttack = false;
        health = maxHealth = HP;
        healthBar.fillAmount = health / maxHealth;
        agent.speed = MoveSpeed;
        ChangeState(Constant.WALK_STATE);

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
            if (state != null)
            {
                state.OnExecute(this);
            }
        }    
        if (GameManager.IsState(GameState.Gameplay))
        {
            
            if (target != null)
            {
                if (target.isDeath)
                {
                    //RemoveTarget(target);
                    target = null;
                }
            }
            if (!isDeath)
            {
                //if(!isTakeDamage)
                //{
                //    DelayTurnOffHealthBar(Time.deltaTime);
                //}    
                
                if (towerTarget == null)
                {
                    if (target == null)
                    {
                        RaycastHit[] hit = new RaycastHit[10];
                        RaycastHit[] hitTower = new RaycastHit[2];
                        int numberCharacter = Physics.SphereCastNonAlloc(TF.position, Sight, TF.right, hit, Sight, layerMask, QueryTriggerInteraction.UseGlobal);
                        int numberTower = Physics.SphereCastNonAlloc(TF.position, AttackRange, TF.right, hitTower, AttackRange, towerMask, QueryTriggerInteraction.UseGlobal);
                        for (int i = 0; i < numberCharacter; i++)
                        {

                            Character cha = hit[i].transform.gameObject.GetComponent<Character>();
                            if (cha != null && !cha.isDeath && cha.PoolTypeObject != PoolTypeObject)
                            {
                                if (Vector3.Distance(TF.position, cha.TF.position) <= this.Sight)
                                {
                                    SetTarget(cha);
                                }
                            }

                        }
                        for (int i = 0; i < numberTower; i++)
                        {
                            Tower tower = hitTower[i].transform.gameObject.GetComponent<Tower>();
                            if(tower != null && tower != ownTown)
                            {
                                if(Vector3.Distance(TF.position, tower.TF.position) <= this.Sight)
                                {
                                    SetTargetTower(tower);
                                }    
                            }    
                           
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(TF.position, target.TF.position) > AttackRange)
                        {
                            Vector3 newDestination = target.TF.position;
                            //newDestination.x += Target.TF.forward.x - 0.5f;
                            newDestination.z = target.TF.position.z;
                            SetDestination(newDestination);
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
        Invoke(nameof(SetPosCurrentModel), 0.5f);
        //EntitiesManager.Ins.RemoveDeathCharacter(this);
        //currentModel.TF.position = onGround;

    }
    private void SetPosCurrentModel()
    {
        Vector3 onGround = currentModel.TF.position;
        onGround.y = -0.55f;
        currentModel.TF.DOMoveY(onGround.y, 2.5f);
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
    public virtual void OnWalkEnter()
    {
        isAttack = false;
        if (listTarget.Count <= 0)
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
        SetDestination(destination);
        ChangeAnim(Constant.ANIM_RUN);
    }
    public virtual void OnRunExecute()
    {
        if (isDestination)
        {
            StopSetDestination();
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
                    //Vector3 targetPos = listTarget[i].TF.position;
                    //SetDestination(targetPos);
                    //if (isDestination || Vector3.Distance(TF.position, targetPos) < Sight / 2 && (Mathf.Abs(TF.position.z - targetPos.z) < 0.1f))
                    //{
                    //    StopSetDestination();
                    //    //ChangeState(Constant.ATK_STATE);
                    //}
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
            if (timer <= 0)
            {
                Invoke(nameof(Attack), 0.1f);
                anim.SetFloat(Constant.ATTACK_SPEED, AtkSpeed);
                ChangeAnimAttack();
                timer = cooldownAttack;
            }
        }    
         
    }
    public virtual void OnAttackExecute()
    {
        if (towerTarget != null)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                //isAttack = false;
            }
            else
            {
                if (!towerTarget.isDestroyed)
                {
                    OnAttackEnter();
                }
            }
        }
        if (target != null)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                //isAttack = false;
            }
            else
            {
                if (!target.isDeath)
                {
                    OnAttackEnter();
                }
                else
                {
                    DelayChangeWalkState();
                }
            }
        }

        else if(target == null && towerTarget == null)
        {
            DelayChangeWalkState();
        }
    }
    private void DelayChangeWalkState()
    {
        Invoke(nameof(ChangeWalkState), cooldownAttack - cooldownAttack / 3);
    }
    public void SetTargetTower(Tower tower)
    {
        towerTarget = tower;
    }
    private void ChangeWalkState()
    {
        ChangeState(Constant.WALK_STATE);
    }
    public virtual void OnDeathEnter()
    {
        Debug.Log("Death enter");
        StopSetDestination();
        SetTarget(null);
        ChangeAnim(Constant.ANIM_DEATH);        
        Invoke(nameof(OnDespawn), 3f);
        
    }
    public virtual void OnDeathExecute()
    {

    }
    public virtual void OnStandUpEnter()
    {

    }
    public virtual void OnStandUpExecute()
    {

    }
    public void OnHit(float damage)
    {
        isTakeDamage = true;
        canvasHealth.gameObject.SetActive(true);
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        Invoke(nameof(SetBoolTakeDamage), 1f);
        if (isDeath)
        {
            isTakeDamage = false;
            OnDeath();
        }
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
            canvasHealth.gameObject.SetActive(false);
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
    private void OnTriggerEnter(Collider other)
    {

    }
}
