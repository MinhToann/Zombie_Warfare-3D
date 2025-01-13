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
    public void OnInit()
    {
        managerSO.SetValueModelWhenInit();
        managerSO.SetValueBulletModelWhenInit();
        SpawnTower();
        SpawnZombies();
    }
    public void SpawnHeroes(GameplayIcon icon)
    {
        //int index = 0;
        if(curCar != null && curCar.CanSpawn)
        {
            Hero hero = SimplePool.Spawn<Hero>(PoolType.Heroes, curCar.SpawnPosHero.position, Quaternion.identity);
            
            hero.ChangeModel(icon.GOType, 90);
            for (int i = 0; i < managerSO.ListCharacterSO.Count; i++)
            {
                if (hero.CurrentModel.GOType == managerSO.ListCharacterSO[i].GOType)
                {
                    managerSO.SetValueForCharacter(hero, i);
                }
            }
            hero.OnInit();
            //hero.SetDirection(hero.TF.right);
            hero.name = hero.NameInfo;
            listHeroes.Add(hero);
        }    
        
    }    
    public void SpawnZombies()
    {
        if(countZombie < LevelManager.Ins.CurLevel.CurrentWave.MaxQuantity)
        {
            float zRandom = Random.Range(-LevelManager.Ins.CurLevel.CurrentMap.SpawnPosZombies.position.z, LevelManager.Ins.CurLevel.CurrentMap.SpawnPosZombies.position.z);
            Vector3 spawnPos = new Vector3(LevelManager.Ins.CurLevel.CurrentMap.SpawnPosZombies.position.x, LevelManager.Ins.CurLevel.CurrentMap.SpawnPosZombies.position.y, zRandom);
            int randomTypeZombie = Random.Range(50, 52);
            Zombie zombie = SimplePool.Spawn<Zombie>(PoolType.Zombies, spawnPos, Quaternion.identity);
            
            zombie.ChangeModel((GameObjectType)randomTypeZombie, -90);
            for (int i = 0; i < managerSO.ListCharacterSO.Count; i++)
            {
                if (zombie.CurrentModel.GOType == managerSO.ListCharacterSO[i].GOType)
                {
                    managerSO.SetValueForCharacter(zombie, i);
                }
            }
            zombie.OnInit();
            //zombie.SetDirection(-zombie.TF.right);
            zombie.name = zombie.NameInfo;
            listZombies.Add(zombie);
            countZombie++;
            StartCoroutine(IncreaseIndex(zombie));
        }    
        else
        {
            return;
        }    
    }    
    public void GoIntoCar(Vector3 destination, Character character)
    {
        destination = curCar.SpawnPosHero.position;
        character.CurrentModel.TF.DORotate(new Vector3(0, -90, 0), 1.5f);
        character.SetDestination(destination);
        //for (int i = 0; i < listHeroes.Count; i++)
        //{
        //    destination = curCar.SpawnPosHero.position;
        //    listHeroes[i].CurrentModel.TF.DORotate(new Vector3(0, -90, 0), 1.5f);
        //    listHeroes[i].SetDestination(destination);
        //}    
    }    
    public void OnDespawnTower(Tower tower)
    {
        SimplePool.Despawn(tower);
        if(tower is Car)
        {
            curCar = null;
        }    
        else
        {
            curBarrier = null;
        }    
    }    
    public void DelaySpawnZombies()
    {
        countZombie = 0;
        Invoke(nameof(SpawnZombies), 1.5f);
    }
    IEnumerator IncreaseIndex(Zombie zombie)
    {
        yield return new WaitForSeconds(zombie.CooldownSpawn);
        if(countZombie < LevelManager.Ins.CurLevel.CurrentWave.MaxQuantity)
        {
            SpawnZombies();
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
       
        Transform parent = LevelManager.Ins.CurLevel.CurrentMap.transform;
        curCar = Instantiate(carHero, parent);
        curCar.TF.position = LevelManager.Ins.CurLevel.CurrentMap.PosCar.position;
        curCar.OnInit();
        
        curBarrier = Instantiate(barrierZombie, parent);
        curBarrier.TF.position = LevelManager.Ins.CurLevel.CurrentMap.PosBarrier.position;
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
    public void OnClearAllCharacter(Character c)
    {
        SimplePool.CollectAll();
        listHeroes.Clear();
        listZombies.Clear();
    }
    public void SetBoolEmptyZombie(bool isEmpty)
    {
        isEmptyZombie = isEmpty;
    }    
}
