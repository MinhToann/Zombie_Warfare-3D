using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private Transform tf;
    public Transform TF
    {
        get
        {
            if(tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
    [SerializeField] MapType mapType;
    [SerializeField] MapObjectType mapObjectType;
    public MapType TypeOfMap => mapType;
    public MapObjectType MapObjectOnGroundType => mapObjectType;
    [SerializeField] protected ManagerSO managerSO;
    [SerializeField] Transform parentObjectGrid;

    public void SetMapType(MapType mapType)
    {
        this.mapType = mapType;
    }    
    public void SetMapObjectType(MapObjectType mapObjectType)
    {
        this.mapObjectType = mapObjectType;
    }
    public void IncreaseNextMapObjectType()
    {
        mapObjectType++;
    }

}
