using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Bullet Attribute")]
public class BulletSO : ScriptableObject
{
    [SerializeField] PoolType poolType;
    [SerializeField] GameObjectType gameObjectType;

    [SerializeField] private float damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float cooldownSpawn;

    public PoolType PoolTypeObject => poolType;
    public GameObjectType GOType => gameObjectType;
    public float Damage => damage;
    public float MoveSpeed => moveSpeed;
    public float CooldownSpawn => cooldownSpawn;

}
