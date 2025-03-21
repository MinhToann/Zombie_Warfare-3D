using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    [SerializeField] List<ParticleSystem> listEffectHit = new List<ParticleSystem>();
    public List<ParticleSystem> ListEffectHit => listEffectHit;

}
