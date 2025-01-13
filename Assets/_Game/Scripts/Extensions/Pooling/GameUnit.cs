using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUnit : MonoBehaviour
{
    private Transform tf;
    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }

    [SerializeField] protected ManagerSO managerSO;

    [SerializeField] PoolType poolType;
    [SerializeField] GameObjectType gameObjectType;
    [SerializeField] private float hp;
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float atkSpeed;
    [SerializeField] private string nameInfo;
    //[SerializeField] private Image icon;
    [SerializeField] private float mana;
    [SerializeField] private float cooldownSpawn;
    [SerializeField] private float sight;
    [SerializeField] private float atkRange;
    public PoolType PoolTypeObject => poolType;
    public GameObjectType GOType => gameObjectType;
    public float HP => hp;
    public float Damage => damage;
    public float MoveSpeed => moveSpeed;
    public float AtkSpeed => atkSpeed;
    public string NameInfo => nameInfo;
    //public Image Icon => icon;
    public float Mana => mana;
    public float CooldownSpawn => cooldownSpawn;
    public float Sight => sight;
    public float AttackRange => atkRange;
    public void SetPoolType(PoolType poolType)
    {
        this.poolType = poolType;
    }
    public void SetGameObjectType(GameObjectType gameObjectType)
    {
        this.gameObjectType = gameObjectType;
    }
    public void SetHP(float hp)
    {
        this.hp = hp;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
    public void SetAtkSpeed(float atkSpeed)
    {
        this.atkSpeed = atkSpeed;
    }
    public void SetName(string name)
    {
        this.nameInfo = name;
    }
    //public void SetImageIcon(Sprite img)
    //{
    //    icon.sprite = img;
    //}
    public void SetMana(float mana)
    {
        this.mana = mana;
    }
    public void SetCooldownSpawn(float cooldownSpawn)
    {
        this.cooldownSpawn = cooldownSpawn;
    }
    public void SetSight(float sight)
    {
        this.sight = sight;
    }    
    public void SetAtkRange(float atkRange)
    {
        this.atkRange = atkRange;
    }    
}
