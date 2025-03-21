using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riffle : WeaponBase
{
    public override void Attack()
    {
        base.Attack();
        //if (Owner.Target != null && !Owner.Target.isDeath)
        //{
            
        //}
        SpawnBullet();
    }
}
