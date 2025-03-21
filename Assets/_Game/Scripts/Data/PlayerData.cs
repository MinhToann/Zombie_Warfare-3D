using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using System.Linq;
using Sirenix.OdinInspector;

public class PlayerData : Singleton<PlayerData> 
{
    [field: SerializeField] public DataOfPlayer data = new DataOfPlayer();
    const string path = "PlayerData.abc";
    //[field: SerializeField] public DataInfoPlayer data = new DataInfoPlayer();

    private void Awake()
    {        
        data = LoadData();
        data.OnAfterDeserialize();
    }
    public void SaveData()
    {
        data.OnBeforeSerialize();
        SaveGame.Save(path, data);
    }
    public DataOfPlayer LoadData()
    {
        return SaveGame.Load(path, new DataOfPlayer());
    }
    //public DataInfoPlayer LoadData()
    //{
    //    return SaveGame.Load(path, new DataInfoPlayer());
    //}

    public void AddData(GameObjectType goType, float hp, float damage, float atkSpeed,
        float moveSpeed, float hpBonus, float damageBonus, float atkSpeedBonus, float moveSpeedBonus)
    {
        data.AddPlayerDataToDictionary(goType, hp, damage, atkSpeed, moveSpeed,
            hpBonus, damageBonus, atkSpeedBonus, moveSpeedBonus);
        SaveData();
    }
    public void SetGOType(GameObjectType objType)
    {
        data.SetGOType(objType);
        SaveData();
    }
    public void SetHP(float hp)
    {
        data.SetHP(hp);
        SaveData();
    }
    public void SetDamage(float damage)
    {
        data.SetDamage(damage); 
        SaveData();
    }
    public void SetAtkSpeed(float atkSpeed)
    {
        data.SetAtkSpeed(atkSpeed);
        SaveData();
    }
    public void SetMoveSpeed(float moveSpeed)
    {
        data.SetMoveSpeed(moveSpeed);
        SaveData();
    }

    public void SetHPBonus(float hp)
    {
        data.SetHPBonus(hp);
        SaveData();
    }
    public void SetDamageBonus(float damage)
    {
        data.SetDamageBonus(damage);
        SaveData();
    }
    public void SetAtkSpeedBonus(float atkSpeed)
    {
        data.SetAtkSpeedBonus(atkSpeed);
        SaveData();
    }
    public void SetMoveSpeedBonus(float moveSpeed)
    {
        data.SetMoveSpeedBonus(moveSpeed);
        SaveData();
    }
    public void SetHP(GameObjectType objType, float hp)
    {
        data.SetHP(objType, hp);
        SaveData();
    }
    public void SetDamage(GameObjectType objType, float damage)
    {
        data.SetDamage(objType, damage);
        SaveData();
    }
    public void SetAtkSpeed(GameObjectType objType, float speed)
    {
        data.SetAtkSpeed(objType, speed);
        SaveData();
    }
    public void SetMoveSpeed(GameObjectType objType, float speed)
    {
        data.SetMoveSpeed(objType, speed);
        SaveData();
    }

    public void SetHPBonus(GameObjectType objType, float hp)
    {
        data.SetHPBonus(objType, hp);
        SaveData();
    }
    public void SetDamageBonus(GameObjectType objType, float damage)
    {
        data.SetDamageBonus(objType, damage);
        SaveData();
    }
    public void SetAtkSpeedBonus(GameObjectType objType, float speed)
    {
        data.SetAtkSpeedBonus(objType, speed);
        SaveData();
    }
    public void SetMoveSpeedBonus(GameObjectType objType, float speed)
    {
        data.SetMoveSpeedBonus(objType, speed);
        SaveData();
    }
}


[System.Serializable]
public class DataOfPlayer
{
    
    [SerializeField] private Hero currentHero;
    
    [SerializeField] GameObjectType goType;
    [SerializeField] float hp;
    [SerializeField] float damage;
    [SerializeField] float atkSpeed;
    [SerializeField] float moveSpeed;

    [SerializeField] float hpBonus;
    [SerializeField] float damageBonus;
    [SerializeField] float atkSpeedBonus;
    [SerializeField] float moveSpeedBonus;

    public GameObjectType GOType => goType;
    public float HP => hp;
    public float Damage => damage;
    public float AttackSpeed => atkSpeed;
    public float MoveSpeed => moveSpeed;

    public float HPBonus => hpBonus;
    public float DamageBonus => damageBonus;
    public float AttackSpeedBonus => atkSpeedBonus;
    public float MoveSpeedBonus => moveSpeedBonus;
    public Hero CurrentHero => currentHero;
    [SerializeField] private Dictionary<GameObjectType, DataOfPlayer> dicDataPlayer = new Dictionary<GameObjectType, DataOfPlayer>();
    public Dictionary<GameObjectType, DataOfPlayer> DicDataPlayer => dicDataPlayer;
    [SerializeField] List<KeyValuePair<GameObjectType, DataOfPlayer>> SerializedData = new List<KeyValuePair<GameObjectType, DataOfPlayer>>();
    public void AddPlayerDataToDictionary(GameObjectType goType, float hp, float damage, float atkSpeed,
        float moveSpeed, float hpBonus, float damageBonus, float atkSpeedBonus, float moveSpeedBonus)
    {
        if (!dicDataPlayer.ContainsKey(goType))
        {
            //dicDataPlayer.Add(goType, new DataOfPlayer());
            var newData = new DataOfPlayer
            {
                goType = goType,
                hp = hp, // Set default values
                damage = damage,
                atkSpeed = atkSpeed,
                moveSpeed = moveSpeed,
                hpBonus = hpBonus,
                damageBonus = damageBonus,
                atkSpeedBonus = atkSpeedBonus,
                moveSpeedBonus = moveSpeedBonus,
            };
            dicDataPlayer.Add(goType, newData);
        }
    }
    public void OnBeforeSerialize()
    {
        SerializedData = dicDataPlayer.ToList();
    }

    public void OnAfterDeserialize()
    {
        dicDataPlayer = SerializedData.ToDictionary(pair => pair.Key, pair => pair.Value);
    }
    public void SetHP(GameObjectType objType, float hp)
    {
        if (dicDataPlayer.ContainsKey(goType))
        {
            dicDataPlayer[objType].SetHP(hp);
        }
    }
    public void SetDamage(GameObjectType objType, float damage)
    {
        if (dicDataPlayer.ContainsKey(goType))
        {
            dicDataPlayer[objType].SetDamage(damage);
        }
    }
    public void SetAtkSpeed(GameObjectType objType, float atkSpeed)
    {
        if (dicDataPlayer.ContainsKey(goType))
        {
            dicDataPlayer[objType].SetAtkSpeed(atkSpeed);
        }
    }
    public void SetMoveSpeed(GameObjectType objType, float moveSpeed)
    {
        if (dicDataPlayer.ContainsKey(goType))
        {
            dicDataPlayer[objType].SetMoveSpeed(moveSpeed);
        }
    }

    public void SetHPBonus(GameObjectType objType, float hp)
    {
        if (dicDataPlayer.ContainsKey(goType))
        {
            dicDataPlayer[objType].SetHPBonus(hp);
        }
    }
    public void SetDamageBonus(GameObjectType objType, float damage)
    {
        if (dicDataPlayer.ContainsKey(goType))
        {
            dicDataPlayer[objType].SetDamageBonus(damage);
        }
    }
    public void SetAtkSpeedBonus(GameObjectType objType, float atkSpeed)
    {
        if (dicDataPlayer.ContainsKey(goType))
        {
            dicDataPlayer[objType].SetAtkSpeedBonus(atkSpeed);
        }
    }
    public void SetMoveSpeedBonus(GameObjectType objType, float moveSpeed)
    {
        if (dicDataPlayer.ContainsKey(goType))
        {
            dicDataPlayer[objType].SetMoveSpeedBonus(moveSpeed);
        }
    }
    public void SetGOType(GameObjectType goType)
    {
        this.goType = goType;
    }
    public void SetHP(float hp)
    {
        this.hp = hp;       
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public void SetAtkSpeed(float atkSpeed)
    {
        this.atkSpeed = atkSpeed;
    }
    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void SetHPBonus(float hp)
    {
        this.hpBonus = hp;
    }
    public void SetDamageBonus(float damage)
    {
        this.damageBonus = damage;
    }
    public void SetAtkSpeedBonus(float atkSpeed)
    {
        this.atkSpeedBonus = atkSpeed;
    }
    public void SetMoveSpeedBonus(float moveSpeed)
    {
        this.moveSpeedBonus = moveSpeed;
    }
}

