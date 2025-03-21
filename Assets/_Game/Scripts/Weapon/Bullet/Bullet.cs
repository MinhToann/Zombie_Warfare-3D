using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : GameUnit
{
    private Character owner;
    [SerializeField] BulletModel currentModel;
    public BulletModel CurrentModel => currentModel;
    public void ChangeModel(GameObjectType modelType)
    {
        if (currentModel != null)
        {
            Destroy(currentModel.gameObject);
        }
        currentModel = Instantiate(managerSO.ChangeBulletModel(modelType), TF);
        for (int i = 0; i < managerSO.ListBulletSO.Count; i++)
        {
            if (modelType == managerSO.ListBulletSO[i].GOType)
            {
                managerSO.SetValueForBulletModel(currentModel, i);
            }
        }
        currentModel.OnInit();
    }
    public void SetOwner(Character owner)
    {
        this.owner = owner;
    }
    private void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    private void Update()
    {
        TF.position += TF.right * MoveSpeed * Time.deltaTime;
        if ((owner.Target != null && !owner.Target.isDeath))
        {
            TF.position = Vector3.MoveTowards(TF.position, owner.Target.TF.position, MoveSpeed * Time.deltaTime);
        }
        //if ((owner.TowerTarget != null && !owner.TowerTarget.isDestroyedd))
        //{
        //    //TF.position = Vector3.MoveTowards(TF.position, owner.TowerTarget.TF.position, MoveSpeed * Time.deltaTime);
        //    TF.position = TF.right * MoveSpeed * Time.deltaTime;
        //}
    }
    private void CollideWithCharacter(Character character)
    {
        if (character != null)
        {
            if (character != owner && character.PoolTypeObject != owner.PoolTypeObject && !character.isDeath)
            {
                character.OnHit(Damage + owner.Damage);
                owner.SetBoolAttack(false);
                OnDespawn();
            }

        }
    }
    private void CollideWithTower(Tower tower)
    {
        if (tower != null && !tower.isDestroyedd && tower != owner.OwnTown)
        {
            if (!owner.isDeath)
            {
                tower.OnHit(Damage + owner.Damage);
                owner.SetBoolAttack(false);

            }
            OnDespawn();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Cache.CollideWithCharacter(other))
        {
            CollideWithCharacter(Cache.CollideWithCharacter(other));
        }
        if (Cache.CollideWithTower(other))
        {
            CollideWithTower(Cache.CollideWithTower(other));
        }

    }
}
