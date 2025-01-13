using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Create Character Info")]
public class CharacterSO : ScriptableObject

{
    [SerializeField] PoolType poolType;
    [SerializeField] GameObjectType gameObjectType;
    [SerializeField] private float hp;
    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float atkSpeed;
    [SerializeField] private string nameInfo;
    [SerializeField] private Sprite icon;
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
    public Sprite Icon => icon;
    public float Mana => mana;
    public float CooldownSpawn => cooldownSpawn;
    public float Sight => sight;
    public float AttackRange => atkRange;
}

