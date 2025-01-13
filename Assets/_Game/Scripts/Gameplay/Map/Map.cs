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
    public Transform PosCar => posCar;
    public Transform PosStartCar => posStartCar;
    public Transform PosBarrier => posBarrier;
    public Transform SpawnPosZombies => spawnPosZombie;
    public Transform TargetPosZombie => targetPosZombie;
    public Transform TargetPosHero => targetPosHero;
    public Transform FinishPos => finishCarPos;

    [SerializeField] List<Transform> listSpawnTransform = new List<Transform>();
    public List<Transform> ListSpawnPos => listSpawnTransform;
}
