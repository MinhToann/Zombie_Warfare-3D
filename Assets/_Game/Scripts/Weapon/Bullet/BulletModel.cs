using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModel : MonoBehaviour
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

    [SerializeField] GameObjectType gameObjectType;
    public GameObjectType GOType => gameObjectType;

    public void SetModelType(GameObjectType gameObjectType)
    {
        this.gameObjectType = gameObjectType;
    }
    public void OnInit()
    {
        
    }
}
