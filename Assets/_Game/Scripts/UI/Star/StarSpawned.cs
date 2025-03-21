using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawned : MonoBehaviour
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
    public void OnDespawn()
    {
        Debug.Log("Despawn star");
        Destroy(this.gameObject);
    }
}
