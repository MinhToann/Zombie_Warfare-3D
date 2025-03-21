using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create VFX Character")]
public class VFXSO : ScriptableObject
{
    [SerializeField] GameObjectType gameObjectType;
    [SerializeField] ParticleSystem effect;
    public GameObjectType GOType => gameObjectType;
    public ParticleSystem Effect => effect;
}
