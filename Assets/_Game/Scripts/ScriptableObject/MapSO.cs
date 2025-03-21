using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Map Data")]
public class MapSO : ScriptableObject
{
    [SerializeField] MapType mapType;
    [SerializeField] MapObjectType mapObjectType;
    public MapType TypeOfMap => mapType;
    public MapObjectType MapObjectOnGroundType => mapObjectType;

    public void SetValueForGrid(Grid grid)
    {
        grid.SetMapType(mapType);
        grid.SetMapObjectType(mapObjectType);
    }    
}
