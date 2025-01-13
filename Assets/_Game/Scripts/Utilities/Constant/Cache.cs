using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour
{
    private static Dictionary<Collider, Character> dicCharacter = new Dictionary<Collider, Character>();
    private static Dictionary<Collider, Tower> dicTower = new Dictionary<Collider, Tower>();
    public static Character CollideWithCharacter(Collider collider)
    {
        if (!dicCharacter.ContainsKey(collider))
        {
            Character character = collider.GetComponent<Character>();
            dicCharacter.Add(collider, character);
        }
        return dicCharacter[collider];  
    }
    public static Tower CollideWithTower(Collider collider)
    {
        if(!dicTower.ContainsKey(collider))
        {
            Tower tower = collider.GetComponent<Tower>();
            dicTower.Add(collider, tower);
        }    
        return dicTower[collider];
    }    
}
