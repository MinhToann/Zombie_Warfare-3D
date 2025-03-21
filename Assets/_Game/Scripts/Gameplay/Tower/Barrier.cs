using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : Tower
{
    public override void OnInit()
    {
        base.OnInit();
        managerSO.SetValueForTower(this, managerSO.ListCharacterSO.Count - 2);

    }
    public override void Update()
    {
        base.Update();
    }
    public override void OnHit(float damage)
    {
        base.OnHit(damage);
        TF.DOShakePosition(0.1f, 0.1f);
    }
}
