using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create a Wave in Level")]
public class Waves : ScriptableObject
{
    [SerializeField] private float waveTime;
    [SerializeField] private int quantityZombieInWave;

    public float TimeInWave => waveTime;
    public int MaxQuantity => quantityZombieInWave;
}
