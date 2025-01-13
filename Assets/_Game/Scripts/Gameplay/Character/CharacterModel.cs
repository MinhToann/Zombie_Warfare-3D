using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CharacterModel : MonoBehaviour
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
    [SerializeField] Animator anim;
    public Animator Anim => anim;
    

    [SerializeField] GameObjectType gameObjectType;
    public GameObjectType GOType => gameObjectType;
    [SerializeField] Transform spawnPosBullet;
    public Transform SpawnPosBullet => spawnPosBullet;
    [SerializeField] WeaponBase weaponInHand;
    public WeaponBase CurrentWeapon => weaponInHand;
    public void SetModelType(GameObjectType gameObjectType)
    {
        this.gameObjectType = gameObjectType;
    }
    
    public void OnInit(float deg)
    {
        //Quaternion newRotate = TF.rotation;
        TF.rotation = Quaternion.Euler(0, deg, 0);
    }    
    private void OnHitWeapon()
    {
        CurrentWeapon.CollideWithCharacter(CurrentWeapon.Owner.Target);
        CurrentWeapon.CollideWithTower(CurrentWeapon.Owner.TowerTarget);
    }
    public void SetAnimatorOverride(AnimatorOverrideController animOverride)
    {
        anim.runtimeAnimatorController = animOverride;
    }
}
