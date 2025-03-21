using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager Character SO")]
public class ManagerSO : ScriptableObject
{
    [SerializeField] private List<CharacterSO> listCharacterSO = new List<CharacterSO> ();
    [SerializeField] private List<Tower> listTower = new List<Tower> ();
    [SerializeField] private List<CharacterModel> listModel = new List<CharacterModel> ();
    [SerializeField] private List<WeaponSO> listWeaponInfo = new List<WeaponSO> ();
    [SerializeField] private List<BulletSO> listBulletInfo = new List<BulletSO> ();
    [SerializeField] private List<Bullet> listBullet = new List<Bullet> ();
    [SerializeField] private List<BulletModel> listBulletModel = new List<BulletModel> ();
    [SerializeField] private List<AnimatorOverrideSO> listAnimatorOverride = new List<AnimatorOverrideSO> ();
    [SerializeField] private List<MapSO> listMapDataSO = new List<MapSO> ();
    [SerializeField] private List<Grid> listGridMap = new List<Grid>();
    [SerializeField] private List<GridObjectOnMap> listBuildingLevel = new List<GridObjectOnMap> ();
    [SerializeField] private List<LevelSO> listLevelSO = new List<LevelSO>();
    [SerializeField] private List<VFXSO> listEffect = new List<VFXSO> ();
    public List<CharacterSO> ListCharacterSO => listCharacterSO;
    public List<Tower> ListTower => listTower;
    public List<CharacterModel> ListModel => listModel;
    public List<WeaponSO> ListWeaponInfo => listWeaponInfo;
    public List<BulletSO> ListBulletSO => listBulletInfo;
    public List<MapSO> ListMapDataSO => listMapDataSO;
    public List<Grid> ListGridMap => listGridMap;
    public List<GridObjectOnMap> ListBuildingLevel => listBuildingLevel;
    public List<LevelSO> ListLevelSO => listLevelSO;
    public List<VFXSO> ListEffect => listEffect;
    public void SetValueForCharacter(Character character, int index)
    {
        character.SetPoolType(listCharacterSO[index].PoolTypeObject);
        character.SetGameObjectType(listCharacterSO[index].GOType);
        if (PlayerData.Ins.data.DicDataPlayer.ContainsKey(listCharacterSO[index].GOType))
        {
            character.SetHP(listCharacterSO[index].HP + PlayerData.Ins.data.DicDataPlayer[listCharacterSO[index].GOType].HPBonus);
            character.SetDamage(listCharacterSO[index].Damage + PlayerData.Ins.data.DicDataPlayer[listCharacterSO[index].GOType].DamageBonus);
            character.SetMoveSpeed(listCharacterSO[index].MoveSpeed + PlayerData.Ins.data.DicDataPlayer[listCharacterSO[index].GOType].MoveSpeedBonus);
            character.SetAtkSpeed(listCharacterSO[index].AtkSpeed + PlayerData.Ins.data.DicDataPlayer[listCharacterSO[index].GOType].AttackSpeedBonus);
        }
        else
        {
            character.SetHP(listCharacterSO[index].HP);
            character.SetDamage(listCharacterSO[index].Damage);
            character.SetMoveSpeed(listCharacterSO[index].MoveSpeed);
            character.SetAtkSpeed(listCharacterSO[index].AtkSpeed);
        }
        character.SetName(listCharacterSO[index].NameInfo);
        character.SetMana(listCharacterSO[index].Mana);
        character.SetCooldownSpawn(listCharacterSO[index].CooldownSpawn);
        character.SetSight(listCharacterSO[index].Sight);
        character.SetAtkRange(listCharacterSO[index].AttackRange);
    }
    public void SetValueForTower(Tower tower, int index)
    {
        tower.SetPoolType(listCharacterSO[index].PoolTypeObject);
        tower.SetGameObjectType(listCharacterSO[index].GOType);
        tower.SetHP(listCharacterSO[index].HP);
        tower.SetDamage(listCharacterSO[index].Damage);
        tower.SetAtkSpeed(listCharacterSO[index].AtkSpeed);
        tower.SetMoveSpeed(listCharacterSO[index].MoveSpeed);
        tower.SetName(listCharacterSO[index].NameInfo);
        tower.SetSight(listCharacterSO[index].Sight);
        tower.SetAtkRange(listCharacterSO[index].AttackRange);
    }

    public void SetValueForIcon(GameplayIcon icon, int index)
    {
        icon.SetPoolType(listCharacterSO[index].PoolTypeObject);
        icon.SetGameObjectType(listCharacterSO[index].GOType);
        icon.SetName(listCharacterSO[index].NameInfo);
        icon.SetIcon(listCharacterSO[index].Icon);
        icon.SetMana(listCharacterSO[index].Mana);
        icon.SetCooldownSpawn(listCharacterSO[index].CooldownSpawn);
    }
    public void SetValueForWeapon(WeaponBase weapon, int index)
    {
        weapon.SetDamage(listWeaponInfo[index].Damage);
        //weapon.SetEffectForWeapon(listWeaponInfo[index].EffectOfWeapon);
    }
    public void SetValueForBullet(Bullet bullet, int index)
    {
        bullet.SetMoveSpeed(listBulletInfo[index].MoveSpeed);
        bullet.SetDamage(listBulletInfo[index].Damage);
    }
    public void SetValueBulletModelWhenInit()
    {
        for(int i = 0; i < listBullet.Count; i++)
        {
            //listBulletModel[i].SetPoolType(listBulletInfo[i].PoolTypeObject);
            listBulletModel[i].SetModelType(listBulletInfo[i].GOType);
        }    
    }    
    public void SetValueForBulletModel(BulletModel model, int index)
    {
        model.SetModelType(listBulletInfo[index].GOType);
    }     
    public void SetValueModelWhenInit()
    {
        for (int i = 0; i < listCharacterSO.Count - 2; i++)
        {
            listModel[i].SetModelType(listCharacterSO[i].GOType);
        }
    }
    public void SetValueModel(CharacterModel model, int index)
    {
        model.SetModelType(listCharacterSO[index].GOType);
        
    }    
    public CharacterModel ChangeCharacterModel(GameObjectType characterType)
    {
        CharacterModel model = new CharacterModel();
        for(int i = 0; i < listModel.Count; i++)
        {
            if (listModel[i].GOType == characterType)
            {
                model = listModel[i];
            }    
        }
        return model;
    }
    public BulletModel ChangeBulletModel(GameObjectType characterType)
    {
        BulletModel model = new BulletModel();
        for (int i = 0; i < listBulletModel.Count; i++)
        {
            if (listBulletModel[i].GOType == characterType)
            {
                model = listBulletModel[i];
            }
        }
        return model;
    }
    public Grid SpawnGridObject(MapObjectType mapObjectType)
    {
        Grid grid = new Grid();
        for(int i = 0; i < listGridMap.Count; i++)
        {
            if (listGridMap[i].MapObjectOnGroundType == mapObjectType)
            {
                grid = listGridMap[i];
            }    
        }
        return grid;
    }    
    public void SetAnimatorCharacter(GameObjectType characterType, CharacterModel model)
    {
        for(int i = 0; i < listAnimatorOverride.Count; i++)
        {
            if(characterType  == listAnimatorOverride[i].GOType)
            {
                model.SetAnimatorOverride(listAnimatorOverride[i].Anim);
            }
        }
    }
    public void SetValueForMapObject()
    {
        for(int i = 0; i < listMapDataSO.Count; i++)
        {
            listMapDataSO[i].SetValueForGrid(listGridMap[i]);
        }    
    }    
    public void SetValueForBuildingLevel()
    {
        for(int i = 0; i < listLevelSO.Count; i++)
        {
            listLevelSO[i].SetValueBuilding(listBuildingLevel[i]);
        }    
    }    
    public void SetVFXCharacter(Character character)
    {
        for(int i = 0; i < listEffect.Count; i++)
        {
            if(character.GOType == listEffect[i].GOType)
            {
                character.CurrentModel.CurrentWeapon.SetEffectForWeapon(listEffect[i].Effect);
            }    
        }    
    }

}
