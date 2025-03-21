using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Transform posCar;
    [SerializeField] Transform posBarrier;
    [SerializeField] Transform posStartCar;
    [SerializeField] Transform spawnPosZombie;
    [SerializeField] Transform targetPosZombie;
    [SerializeField] Transform targetPosHero;
    [SerializeField] Transform finishCarPos;
    [SerializeField] Transform deadPosY;
    public Transform PosCar => posCar;
    public Transform PosStartCar => posStartCar;
    public Transform PosBarrier => posBarrier;
    public Transform SpawnPosZombies => spawnPosZombie;
    public Transform TargetPosZombie => targetPosZombie;
    public Transform TargetPosHero => targetPosHero;
    public Transform FinishPos => finishCarPos;
    public Transform DeadPosY => deadPosY;

    [SerializeField] List<Transform> listSpawnTransform = new List<Transform>();
    public List<Transform> ListSpawnPos => listSpawnTransform;

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
}
