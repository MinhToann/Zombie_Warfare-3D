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
}
