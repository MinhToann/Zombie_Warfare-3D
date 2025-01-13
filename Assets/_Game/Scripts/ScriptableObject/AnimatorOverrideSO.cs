using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Animator Override Info")]
public class AnimatorOverrideSO : ScriptableObject
{
    [SerializeField] GameObjectType gameObjectType;
    public GameObjectType GOType => gameObjectType;
    [SerializeField] AnimatorOverrideController anim;
    public AnimatorOverrideController Anim => anim;
}
