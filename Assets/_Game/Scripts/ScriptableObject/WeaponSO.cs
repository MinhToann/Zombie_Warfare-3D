using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Weapon Attribute")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] PoolType poolType;
    [SerializeField] GameObjectType gameObjectType;

    [SerializeField] private float damage;
    [SerializeField] private float atkSpeed;

    public PoolType PoolTypeObject => poolType;
    public GameObjectType GOType => gameObjectType;
    public float Damage => damage;
    public float AtkSpeed => atkSpeed;
}
