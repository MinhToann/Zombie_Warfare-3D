using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntitiesManager : Singleton<EntitiesManager>
{
    [SerializeField] List<Hero> listHeroes = new List<Hero>();
    public List<Hero> ListHero => listHeroes;
    [SerializeField] List<Zombie> listZombies = new List<Zombie>();
    public List<Zombie> ListZombie => listZombies;
    [SerializeField] Car carHero;
    private Car curCar;
    public Car CurrentCar => curCar;
    [SerializeField] Barrier barrierZombie;
    private Barrier curBarrier;
    public Barrier CurrentBarrier => curBarrier;
    [SerializeField] ManagerSO managerSO;
    private int quantityZombie = 2;
    private int countInWave = 2;
    int countZombie = 0;
    private bool isEmptyZombie = false;
    public bool IsEmpty => isEmptyZombie;
    [SerializeField] List<Blood> bloodEffect = new List<Blood>();
    private List<Blood> listBloodOnGround = new List<Blood>();
    [SerializeField] Transform parentBlood;
    private Hero currentUpgHero;
    public Hero CurrentUpgHero => currentUpgHero;
    private int maxBoss = 1;
    private int countBoss = 0;
    float rate;
    int randNum;
    public void OnInit()
    {
        randNum = Random.Range(50, 53);
        countZombie = 0;
        managerSO.SetValueModelWhenInit();
        managerSO.SetValueBulletModelWhenInit();
        SpawnTower();
        SpawnZombies((GameObjectType)randNum);
    }
    private void RateSpawnHero(Hero hero)
    {
        rate = Random.Range(0, 100);
        if (rate <= 10)
        {
            hero.CreateLaurel();
            hero.SetHP(hero.HP * 1.5f);
            hero.SetAtkSpeed(hero.AtkSpeed * 1.75f);
            hero.SetDamage(hero.Damage * 1.5f);
        }
    }
    public void SpawnHeroes(GameObjectType GOType)
    {
        if (curCar != null && curCar.CanSpawn)
        {
            Hero hero = SimplePool.Spawn<Hero>(PoolType.Heroes, curCar.SpawnPosHero.position, Quaternion.identity);

            hero.ChangeModel(GOType, 90);
            hero.ResetCharacter();
            for (int i = 0; i < managerSO.ListCharacterSO.Count; i++)
            {
                if (hero.CurrentModel.GOType == managerSO.ListCharacterSO[i].GOType)
                {
                    managerSO.SetValueForCharacter(hero, i);
                }
            }

            //hero.SetDirection(hero.TF.right);
            hero.name = hero.NameInfo;
            managerSO.SetVFXCharacter(hero);
            //if (rate <= 10)
            //{
            //    hero.CreateLaurel();
            //    hero.SetHP(hero.HP * 1.5f);
            //    hero.SetAtkSpeed(hero.AtkSpeed * 1.75f);
            //    hero.SetDamage(hero.Damage * 1.5f);
            //}
            hero.OnInit();
            listHeroes.Add(hero);
            //hero.SetMoveSpeed(hero.MoveSpeed);
        }

    }
    public void SpawnHeroes(GameplayIcon icon)
    {
        //int index = 0;
        rate = Random.Range(0, 100);
        if (curCar != null && curCar.CanSpawn)
        {
            Hero hero = SimplePool.Spawn<Hero>(PoolType.Heroes, curCar.SpawnPosHero.position, Quaternion.identity);
            
            hero.ChangeModel(icon.GOType, 90);
            hero.ResetCharacter();
            for (int i = 0; i < managerSO.ListCharacterSO.Count; i++)
            {
                if (hero.CurrentModel.GOType == managerSO.ListCharacterSO[i].GOType)
                //&& hero.CurrentModel.GOType == PlayerData.Ins.data.GOType)
                {
                    managerSO.SetValueForCharacter(hero, i);
                }
            }
            
            //hero.SetDirection(hero.TF.right);
            hero.name = hero.NameInfo;
            managerSO.SetVFXCharacter(hero);
            if (rate <= 10)
            {
                hero.CreateLaurel();
                hero.SetHP(hero.HP * 1.5f);
                hero.SetAtkSpeed(hero.AtkSpeed * 1.75f);
                hero.SetDamage(hero.Damage * 1.5f);
            }
            hero.OnInit();
            listHeroes.Add(hero);
            //hero.SetMoveSpeed(hero.MoveSpeed);
        }

    }
    public void SpawnZombies(GameObjectType goType)
    {
        if(GameManager.IsState(GameState.Gameplay))
        {
            rate = Random.Range(0, 100);
            if (countZombie < LevelManager.Ins.CurrentWave.MaxQuantity)//CurLevel.CurrentWave.MaxQuantity)
            {
                float zRandom = Random.Range(-LevelManager.Ins.CurrentMap.SpawnPosZombies.position.z, LevelManager.Ins.CurrentMap.SpawnPosZombies.position.z);//CurLevel.CurrentMap.SpawnPosZombies.position.z, LevelManager.Ins.CurLevel.CurrentMap.SpawnPosZombies.position.z);
                Vector3 spawnPos = new Vector3(LevelManager.Ins.CurrentMap.SpawnPosZombies.position.x, LevelManager.Ins.CurrentMap.SpawnPosZombies.position.y, zRandom);
                //int randomTypeZombie = Random.Range(50, 53);
                //Debug.Log(randomTypeZombie);
                Debug.Log(randNum);
                Zombie zombie = SimplePool.Spawn<Zombie>(PoolType.Zombies, spawnPos, Quaternion.identity);
                zombie.ChangeModel(goType, -90);//((GameObjectType)randomTypeZombie, -90);
                for (int i = 0; i < managerSO.ListCharacterSO.Count; i++)
                {
                    if (zombie.CurrentModel.GOType == managerSO.ListCharacterSO[i].GOType)
                    {
                        managerSO.SetValueForCharacter(zombie, i);
                    }
                }
                
                //zombie.SetDirection(-zombie.TF.right);
                zombie.name = zombie.NameInfo;
                managerSO.SetVFXCharacter(zombie);                  
                zombie.OnInit();
                
                if (goType == GameObjectType.ZombieBoss)
                {
                    zombie.TF.localScale *= 2;
                }    
                else
                {
                    zombie.TF.localScale = Vector3.one;
                }    
                listZombies.Add(zombie);
                //zombie.SetMoveSpeed(zombie.MoveSpeed);
                
                countZombie++;
                StartCoroutine(IncreaseIndex(zombie));
            }
                
        }    
        
    }
    
    public void WarningSpawnBoss()
    {
        UIManager.Ins.OpenUI<CanvasWarning>();
        //Invoke(nameof(SpawnZombieBoss), 1f);
        StartCoroutine(SpawnBoss());
    }    
    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(1f);
        SpawnZombies(GameObjectType.ZombieBoss);
    }    
    public void CreateBloodEffect(Vector3 targetPos)
    {
        int randomBlood = Random.Range(0, bloodEffect.Count);
        Blood blood = Instantiate(bloodEffect[randomBlood], parentBlood);
        Vector3 posY = targetPos;
        posY.y = 0;
        blood.TF.position = posY;
        listBloodOnGround.Add(blood);
        StartCoroutine(DelayDeactiveBlood(blood));
    }
    public void CreateSmoke(Vector3 spawnPos)
    {

    }    
    public void OnDepsawnBlood(Blood blood)
    {
        if (blood != null)
        {
            Destroy(blood.gameObject);
        }
        listBloodOnGround.Remove(blood);
    }
    public void ClearBlood()
    {
        for (int i = 0; i < listBloodOnGround.Count; i++)
        {
            Destroy(listBloodOnGround[i].gameObject);
        }
        listBloodOnGround.Clear();
    }
    private IEnumerator DelayDeactiveBlood(Blood blood)
    {
        yield return new WaitForSeconds(8f);
        OnDepsawnBlood(blood);
    }
    public void GoBackToCar()
    {
        for (int i = 0; i < listHeroes.Count; i++)
        {
            if (!listHeroes[i].isDeath)
            {
                listHeroes[i].ChangeRunState();
            }
        }
    }
    public void OnDespawnTower(Tower tower)
    {

        if (tower is Car)
        {
            curCar = null;
        }
        else
        {
            curBarrier = null;
        }
        SimplePool.Despawn(tower);
    }
    public void DelaySpawnZombies()
    {
        //countZombie = 0;
        //Invoke(nameof(SpawnZombies), 1.5f);
        StartCoroutine(DelayZombieSpawn());
    }

    IEnumerator DelayZombieSpawn()
    {
        countZombie = 0;
        randNum = Random.Range(50, 53);
        yield return new WaitForSeconds(1.5f);
        SpawnZombies((GameObjectType)randNum);
    }
    IEnumerator IncreaseIndex(Zombie zombie)
    {
        randNum = Random.Range(50, 53);
        yield return new WaitForSeconds(zombie.CooldownSpawn);
        if (countZombie < LevelManager.Ins.CurrentWave.MaxQuantity)
        {
            SpawnZombies((GameObjectType)randNum);
        }

    }
    public void ChangeHeroToZombie()
    {
        for (int j = 0; j < listHeroes.Count; j++)
        {
            if (listHeroes[j].isDeath)
            {
                Transform newTF = listHeroes[j].TF;
                int randomTypeZombie = Random.Range(50, 52);
                Zombie zombie = SimplePool.Spawn<Zombie>(PoolType.Zombies, newTF.position, Quaternion.identity);
                //zombie.TF.rotation = newTF.rotation;
                zombie.ChangeModel((GameObjectType)randomTypeZombie, 90);
                for (int i = 0; i < managerSO.ListCharacterSO.Count; i++)
                {
                    if (zombie.CurrentModel.GOType == managerSO.ListCharacterSO[i].GOType)
                    {
                        managerSO.SetValueForCharacter(zombie, i);
                    }
                }
                //zombie.OnInit();
                //zombie.SetDirection(-zombie.TF.right);
                zombie.name = zombie.NameInfo;
                managerSO.SetVFXCharacter(zombie);
                listZombies.Add(zombie);
                //zombie.SetMoveSpeed(zombie.MoveSpeed);
                zombie.ChangeState(Constant.STANDUP_STATE);
                //zombie.ChangeAnim(Constant.ANIM_STANDUP);
            }
        }

    }
    private void SpawnTower()
    {
        //int index = 0;
        //for (int j = managerSO.ListCharacterSO.Count - 2; j < managerSO.ListCharacterSO.Count; j++)
        //{
        //    Tower tower = SimplePool.Spawn<Tower>(PoolType.Towers, LevelManager.Ins.CurLevel.CurrentMap.ListSpawnPos[index].position, Quaternion.identity, 1);
        //    managerSO.SetValueForTower(tower, j);
        //    index++;
        //}

        Transform parent = LevelManager.Ins.CurrentMap.transform;//CurLevel.CurrentMap.transform;
        curCar = Instantiate(carHero, parent);
        curCar.TF.position = LevelManager.Ins.CurrentMap.PosCar.position;//CurLevel.CurrentMap.PosCar.position;
        curCar.OnInit();

        curBarrier = Instantiate(barrierZombie, parent);
        curBarrier.TF.position = LevelManager.Ins.CurrentMap.PosBarrier.position;//CurLevel.CurrentMap.PosBarrier.position;
        curBarrier.OnInit();
    }
    public void OnDeathCharacter(Character c)
    {
        SimplePool.Despawn(c);
        RemoveDeathCharacter(c);
    }
    public void RemoveDeathCharacter(Character c)
    {
        if (c is Zombie)
        {
            listZombies.Remove((Zombie)c);
            //if (EntitiesManager.Ins.ListZombie.Count <= 0)
            //{
            //    EntitiesManager.Ins.SetBoolEmptyZombie(true);
            //    Debug.Log("Count < 0");
            //}
            if (listZombies.Count <= 0)
            {
                SetBoolEmptyZombie(true);
            }
        }
        else
        {
            listHeroes.Remove((Hero)c);
        }

    }
    public void OnClearAllCharacter()
    {
        
        listHeroes.Clear();
        listZombies.Clear();
        SimplePool.CollectAll();
    }
    public void SetBoolEmptyZombie(bool isEmpty)
    {
        isEmptyZombie = isEmpty;
    }

    public void SpawnUpgradeHero(GameObjectType goType, Transform parent)
    {
        if (currentUpgHero != null)
        {
            SimplePool.Despawn(currentUpgHero);
            currentUpgHero = null;
        }
        Hero hero = SimplePool.Spawn<Hero>(PoolType.Heroes, Vector3.zero, Quaternion.identity);
        hero.TF.SetParent(parent);
        hero.TF.localPosition = new Vector3(0, 0, 2f);
        hero.ChangeModel(goType, -150);
        for (int i = 0; i < managerSO.ListCharacterSO.Count; i++)
        {
            if (hero.CurrentModel.GOType == managerSO.ListCharacterSO[i].GOType)
            {
                managerSO.SetValueForCharacter(hero, i);
            }
        }
        hero.name = hero.NameInfo;
        currentUpgHero = hero;
        //PlayerData.Ins.SetCurrentHero(hero);
        //SaveLoadCharacter.Ins.SaveCharacterData();


       
        PlayerData.Ins.SetGOType(hero.GOType);
        //PlayerData.Ins.AddData(hero.GOType, hero.HP, hero.Damage, hero.AtkSpeed, hero.MoveSpeed, 0, 0, 0, 0);

        PlayerData.Ins.SetHP(hero.HP);
        PlayerData.Ins.SetDamage(hero.Damage);
        PlayerData.Ins.SetAtkSpeed(hero.AtkSpeed);
        PlayerData.Ins.SetMoveSpeed(hero.MoveSpeed);
        

        if (!PlayerData.Ins.data.DicDataPlayer.ContainsKey(hero.GOType))
        {
            PlayerData.Ins.AddData(hero.GOType, hero.HP, hero.Damage, hero.AtkSpeed, hero.MoveSpeed, 0, 0, 0, 0);

        }
        else
        {
            var data = PlayerData.Ins.data.DicDataPlayer[hero.GOType];
            PlayerData.Ins.SetHPBonus(data.HPBonus);
            PlayerData.Ins.SetAtkSpeedBonus(data.AttackSpeedBonus);
            PlayerData.Ins.SetDamageBonus(data.DamageBonus);
            PlayerData.Ins.SetMoveSpeedBonus(data.MoveSpeedBonus);

            //PlayerData.Ins.SetHP(hero.GOType, hero.HP);
            //PlayerData.Ins.SetDamage(hero.GOType, hero.Damage);
            //PlayerData.Ins.SetAtkSpeed(hero.GOType, hero.AtkSpeed);
            //PlayerData.Ins.SetMoveSpeed(hero.GOType, hero.MoveSpeed);

            

            //PlayerData.Ins.SetHP(hero.GOType, PlayerData.Ins.data.DicDataPlayer[hero.GOType].HP);
            //PlayerData.Ins.SetDamage(hero.GOType, PlayerData.Ins.data.DicDataPlayer[hero.GOType].Damage);
            //PlayerData.Ins.SetAtkSpeed(hero.GOType, PlayerData.Ins.data.DicDataPlayer[hero.GOType].AttackSpeed);
            //PlayerData.Ins.SetMoveSpeed(hero.GOType, PlayerData.Ins.data.DicDataPlayer[hero.GOType].MoveSpeed);

            PlayerData.Ins.SetHPBonus(hero.GOType, data.HPBonus);
            PlayerData.Ins.SetAtkSpeedBonus(hero.GOType, data.AttackSpeedBonus);
            PlayerData.Ins.SetDamageBonus(hero.GOType, data.DamageBonus);
            PlayerData.Ins.SetMoveSpeedBonus(hero.GOType, data.MoveSpeedBonus);
        }


    }
}
