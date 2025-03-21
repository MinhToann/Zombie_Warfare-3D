using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class Car : Tower
{
    [SerializeField] Transform spawnPosHero;
    public Transform SpawnPosHero => spawnPosHero;

    [SerializeField] Transform spawnPosCarHero;
    public Transform SpawnPosCarHero => spawnPosCarHero;
    [SerializeField] Transform targetPosCarHero;
    public Transform TargetPosCarHero => targetPosCarHero;
    private bool canSpawn;
    public bool CanSpawn => canSpawn;
    private Character target;
    public Character Target => target;
    [SerializeField] LayerMask layerMask;
    int count = 0;
    float intensity = 0;
    [SerializeField] PostProcessVolume volume;
    Vignette vignette;
    private void SetBoolCanSpawn()
    {
        canSpawn = true;
    }    
    public override void OnInit()
    {
        base.OnInit();
        count = 0;
        managerSO.SetValueForTower(this, managerSO.ListCharacterSO.Count - 1);
        canSpawn = false;
        Invoke(nameof(SetBoolCanSpawn), 1.5f);
        volume.profile.TryGetSettings<Vignette>(out vignette);
        if(!vignette)
        {
            Debug.Log("Error vignette");
        }
        else
        {
            vignette.enabled.Override(false);
        }
        //TF.DOMove(LevelManager.Ins.CurLevel.CurrentMap.PosStartCar.position, 2f).OnComplete(() =>
        //{
        //    canSpawn = true;
        //});
        //TF.Translate(LevelManager.Ins.CurLevel.CurrentMap.PosStartCar.position * MoveSpeed * Time.deltaTime);
        //
    }

    public override void Update()
    {
        base.Update();
        if(GameManager.IsState(GameState.Gameplay))
        {
            TF.position = Vector3.MoveTowards(TF.position, LevelManager.Ins.CurrentMap.PosStartCar.position, MoveSpeed * Time.deltaTime);
            CheckZombieInRange(); 
        }    
        if(GameManager.IsState(GameState.Win))
        {
            if(EntitiesManager.Ins.ListHero.Count <= 0)
            {
                //SetMoveSpeed(MoveSpeed * 2);
                TF.position = Vector3.MoveTowards(TF.position, LevelManager.Ins.CurrentMap.FinishPos.position, MoveSpeed * Time.deltaTime);
            }    
            
        }    
    }
    private void CheckZombieInRange()
    {
        if (target == null)
        {
            RaycastHit[] hit = new RaycastHit[10];

            int numberCharacter = Physics.SphereCastNonAlloc(TF.position, Sight, TF.right, hit, Sight, layerMask, QueryTriggerInteraction.UseGlobal);

            for (int i = 0; i < numberCharacter; i++)
            {
                Character cha = hit[i].transform.gameObject.GetComponent<Character>();
                if (cha != null && !cha.isDeath)
                {
                    if (Vector3.Distance(TF.position, cha.TF.position) <= this.Sight)
                    {
                        target = cha;
                    }
                }

            }

        }
        else
        {
            if (count < 1)
            {
                EntitiesManager.Ins.SpawnHeroes(GameObjectType.RiffleCarHero);
                count++;
            }
        }
    }    
    IEnumerator TakeDamageEffect()
    {
        intensity = 0.4f;
        vignette.enabled.Override(true);
        vignette.intensity.Override(0.4f);
        yield return new WaitForSeconds(0.4f);
        while(intensity > 0)
        {
            intensity -= 0.1f;
            if(intensity < 0)
            {
                intensity = 0;
            }
            vignette.intensity.Override(intensity);
            yield return new WaitForSeconds(0.1f);
        }
        vignette.enabled.Override(false);
        yield break;
    }
    public override void OnHit(float damage)
    {
        base.OnHit(damage);
        TF.DOShakePosition(0.3f, 0.3f);
        StartCoroutine(TakeDamageEffect());
    }

    private void CollideZombie(Character zombie)
    {
        if(zombie != null && !zombie.isDeath)
        {
            zombie.OnHit(Damage);
            zombie.ChangeState(Constant.FLYINGBACKDEATH_STATE);
            
        }    
    }    
    private void OnTriggerEnter(Collider other)
    {
        if(Cache.CollideWithCharacter(other))
        {
            if(Cache.CollideWithCharacter(other) is Zombie)
            {
                if(GameManager.IsState(GameState.Win))
                {
                    CollideZombie(Cache.CollideWithCharacter(other));
                }    
                
            }    
            
        }    
    }
}
