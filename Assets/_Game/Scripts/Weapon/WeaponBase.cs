using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class WeaponBase : GameUnit
{
    private Character owner;
    public Character Owner => owner;
    private bool isAttack;
    public bool IsAttack => isAttack;
    int countCollide = 0;
    public void OnInit(Character owner)
    {
        for(int i = 0; i < managerSO.ListWeaponInfo.Count; i++)
        {
            if(GOType == managerSO.ListWeaponInfo[i].GOType)
            {
                managerSO.SetValueForWeapon(this, i);
                //SetOwner(owner);
            }    
        }    
        
    }    
    public virtual void Attack()
    {
        
    }    
    public void SpawnBullet()
    {
        
        Bullet bullet = SimplePool.Spawn<Bullet>(PoolType.Bullet, owner.CurrentModel.SpawnPosBullet.position, owner.TF.rotation);
        bullet.ChangeModel(this.GOType);
        for (int i = 0; i < managerSO.ListBulletSO.Count; i++)
        {
            if (bullet.CurrentModel.GOType == managerSO.ListBulletSO[i].GOType)
            {
                managerSO.SetValueForBullet(bullet, i);
            }
        }
        bullet.SetOwner(owner);
        //bullet.OnInit();
    }    
    public void SetOwner(Character owner)
    {
        this.owner = owner;
    }

    private void SetTrueAttack(Character c)
    {
        c.SetBoolAttack(true);
    }    
    private void CharacterTakeDame(Character c)
    {
        c.OnHit(Damage + owner.Damage);
    }    
    IEnumerator DelayTakeDame(Character character)
    {
        yield return new WaitForSeconds(owner.CoolDownAttack / 5.225f);
        CollideWithCharacter(character);
    }    
    public void CollideWithCharacter(Character character)
    {
        if (character != null)
        {
            if (character != owner && character.PoolTypeObject != owner.PoolTypeObject && !character.isDeath)
            {
                //DelayTakeDame(character);
                if (!owner.isDeath && owner.IsAttack)
                {
                    //StartCoroutine(DelayTakeDame(character));
                    character.OnHit(Damage + owner.Damage);
                    owner.SetBoolAttack(false);
                }
            }

        }
        //if (!owner.isDeath && owner.IsAttack)
        //{
        //    //StartCoroutine(DelayTakeDame(character));
        //    character.OnHit(Damage + owner.Damage);
        //    owner.SetBoolAttack(false);
        //}
    }
    public void CollideWithTower(Tower tower)
    {
        if (tower != null && owner.IsAttack && !tower.isDestroyed)
        {
            tower.OnHit(Damage + owner.Damage);
            owner.SetBoolAttack(false);
        }
    }
    private void CheckCollide(Character character)
    {
        if (character != null)
        {
            if(character != owner && character.PoolTypeObject != owner.PoolTypeObject)
            {
                SetTrueAttack(character);
            }    
        }    
    }
    //public void OnTriggerEnter(Collider other)
    //{
    //    //isAttack = true; 
    //    if (Cache.CollideWithCharacter(other))
    //    {
    //        CollideWithCharacter(Cache.CollideWithCharacter(other));
    //        //StartCoroutine(DelayTakeDame(Cache.CollideWithCharacter(other)));
    //    }
    //    if (Cache.CollideWithTower(other))
    //    {
    //        CollideWithTower(Cache.CollideWithTower(other));
    //    }

    //}
}
