using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UpgradeButton : MonoBehaviour
{

    private float hpValue = 5f;
    private float damageValue = 2f;
    private float atkSpeedValue = 0.05f;
    private float moveSpeedValue = 0.1f;

    private float hpBonus = 0;
    private float damageBonus = 0;
    private float atkSpeedBonus = 0;
    private float moveSpeedBonus = 0;
    public void UpgradeHP()
    {
        GameObjectType objType = EntitiesManager.Ins.CurrentUpgHero.GOType;
        //Hero hero = EntitiesManager.Ins.CurrentUpgHero;
        float hp = PlayerData.Ins.data.DicDataPlayer[objType].HP;
        hp += hpValue;
        hpBonus += hpValue;
        PlayerData.Ins.SetHP(objType, hp);
        PlayerData.Ins.SetHPBonus(hpBonus);
        PlayerData.Ins.SetHPBonus(objType, hpBonus);
        //hero.SetHP(hero.HP + PlayerData.Ins.data.DicDataPlayer[objType].HPBonus);  
        UIManager.Ins.GetUI<CanvasCharacterInfo>().SetBonusHP(hpBonus);
    }   
    public void UpgradeDamage()
    {
        GameObjectType objType = EntitiesManager.Ins.CurrentUpgHero.GOType;
        //Hero hero = EntitiesManager.Ins.CurrentUpgHero;
        float damage = PlayerData.Ins.data.DicDataPlayer[objType].Damage;
        damage += damageValue;
        damageBonus += damageValue;
        PlayerData.Ins.SetDamage(objType, damage);
        PlayerData.Ins.SetDamageBonus(damageBonus);
        PlayerData.Ins.SetDamageBonus(objType, damageBonus);
        //hero.SetDamage(hero.Damage + PlayerData.Ins.data.DicDataPlayer[objType].DamageBonus);
        UIManager.Ins.GetUI<CanvasCharacterInfo>().SetBonusDamage(damageBonus);
    }   
    public void UpgradeAtkSpeed()
    {
        GameObjectType objType = EntitiesManager.Ins.CurrentUpgHero.GOType;
        //Hero hero = EntitiesManager.Ins.CurrentUpgHero;
        float atkSpeed = PlayerData.Ins.data.DicDataPlayer[objType].AttackSpeed;
        atkSpeed += atkSpeedValue;
        atkSpeedBonus += atkSpeedValue;
        PlayerData.Ins.SetAtkSpeed(objType, atkSpeed);
        PlayerData.Ins.SetAtkSpeedBonus(atkSpeedBonus);
        PlayerData.Ins.SetAtkSpeedBonus(objType, atkSpeedBonus);
        //hero.SetAtkSpeed(hero.AtkSpeed + PlayerData.Ins.data.DicDataPlayer[objType].AttackSpeedBonus);
        UIManager.Ins.GetUI<CanvasCharacterInfo>().SetBonusAtkSpeed(atkSpeedBonus);
    }   
    public void UpgradeMoveSpeed()
    {
        GameObjectType objType = EntitiesManager.Ins.CurrentUpgHero.GOType;
        //Hero hero = EntitiesManager.Ins.CurrentUpgHero;
        float moveSpeed = PlayerData.Ins.data.DicDataPlayer[objType].MoveSpeed;
        moveSpeed += moveSpeedValue;
        moveSpeedBonus += moveSpeedValue;
        PlayerData.Ins.SetMoveSpeed(objType, moveSpeed);
        PlayerData.Ins.SetMoveSpeedBonus(moveSpeedBonus);
        PlayerData.Ins.SetMoveSpeedBonus(objType, moveSpeedBonus);
        //hero.SetMoveSpeed(hero.MoveSpeed + PlayerData.Ins.data.DicDataPlayer[objType].MoveSpeedBonus);
        UIManager.Ins.GetUI<CanvasCharacterInfo>().SetBonusMoveSpeed(moveSpeedBonus);
    }    
}
