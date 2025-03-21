using DG.Tweening;
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
    [SerializeField] Character character;
    public void SetModelType(GameObjectType gameObjectType)
    {
        this.gameObjectType = gameObjectType;
    }
    
    public void OnInit(float deg)
    {
        //Quaternion newRotate = TF.rotation;
        TF.rotation = Quaternion.Euler(0, deg, 0);
    }    
    public void RotateModel(float deg, float timer)
    {
        TF.DORotate(new Vector3(0, deg, 0), timer);       
    }
    private void OnGunShooting()
    {
        weaponInHand.Attack();
    }    
    private void OnDelay()
    {
        //character.SetBoolAttack(false); 
    }    
    private void OnHitWeapon() //For animation event
    {
        ParticlePool.Play(CurrentWeapon.EffectWeapon, CurrentWeapon.TF.position, Quaternion.identity);
        //CurrentWeapon.CollideWithCharacter(CurrentWeapon.Owner.Target);
        CurrentWeapon.CollideWithCharacter(CurrentWeapon.Owner.Target);
        CurrentWeapon.CollideWithTower(CurrentWeapon.Owner.TowerTarget);
    }
    private void SetZeroMoveSpeed()
    {
        character.SetMoveSpeed(0);
    }    
    private void SetNormalMoveSpeed()
    {
        character.SetMoveSpeed(character.NormalMS);
    }    
    private void StandUp()//For animation event zombie
    {
        //Vector3 onGround = TF.position;
        //onGround.y = 0.6f;
        //TF.DOMoveY(onGround.y, 1f);
        //TF.position = new Vector3(TF.position.x, 1, TF.position.z);
    }    
    private void RotateAfterStandUp()//For animation event zombie 
    {
        
        character.SetNormalStateForZombie();
        //Vector3 onGround = TF.position;
        //onGround.y = 0f;
        //TF.DOMoveY(onGround.y, 0.2f);
        //TF.position = new Vector3(TF.position.x, -1, TF.position.z);
        //RotateModel(-90, 1.5f);
    }
    private void KneelStandUp()
    {
        character.KneelRotate();
    }    
    private void KneelDownToAttack()
    {
        character.OnAttackEnter();
    }    
    public void SetAnimatorOverride(AnimatorOverrideController animOverride)
    {
        anim.runtimeAnimatorController = animOverride;
    }
    public void SetCharacterForModel(Character character)
    {
        this.character = character;
    }     
}
