using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    private void Awake()
    {
        GameUnit[] gameUnits = Resources.LoadAll<GameUnit>("Pool/");
        //load tu resources
        for (int i = 0; i < gameUnits.Length; i++)
        {
            SimplePool.Preload(gameUnits[i], 0, transform);
        }
    }
}
