using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCharacterInfo : UICanvas
{
    [SerializeField] RawImage imgCharacter;
    [SerializeField] Camera camRT;
    [SerializeField] ManagerSO managerSO;
    [SerializeField] Text nameCharacter;
    [SerializeField] Text health;
    [SerializeField] Text damage;
    [SerializeField] Text atkSpeed;
    [SerializeField] Text moveSpeed;

    [SerializeField] Text hpPlus;
    [SerializeField] Text dmgPlus;
    [SerializeField] Text atkSpeedPlus;
    [SerializeField] Text moveSpeedPlus;
    public override void Setup()
    {
        base.Setup();
        GameManager.ChangeState(GameState.Upgrade);
    }
    public override void Open()
    {
        base.Open();
        SetImageCharacter(GameObjectType.MeleeHero);
        SetInfo();
    }
    public void PreviousButton()
    {
        GameObjectType gameObjType = EntitiesManager.Ins.CurrentUpgHero.GOType;
        if(EntitiesManager.Ins.CurrentUpgHero.CurrentModel.GOType != managerSO.ListModel[0].GOType)
        {
            --gameObjType;          
        }    
        else
        {
            gameObjType = managerSO.ListModel[managerSO.ListModel.Count - 3].GOType;
        }
        EntitiesManager.Ins.SpawnUpgradeHero(gameObjType, camRT.transform);
        SetInfo();
    }   
    public void NextButton()
    {
        GameObjectType gameObjType = EntitiesManager.Ins.CurrentUpgHero.GOType;
        if (EntitiesManager.Ins.CurrentUpgHero.CurrentModel.GOType != managerSO.ListModel[managerSO.ListModel.Count - 3].GOType)
        {
            ++gameObjType;
        }
        else
        {
            gameObjType = GameObjectType.MeleeHero;
        }
        EntitiesManager.Ins.SpawnUpgradeHero(gameObjType, camRT.transform);
        SetInfo();
    }    
    public void SetImageCharacter(GameObjectType goType)
    {
        EntitiesManager.Ins.SpawnUpgradeHero(goType, camRT.transform);
        imgCharacter.texture = camRT.targetTexture;
    }    
    public void SetInfo()
    {
        GameObjectType goType = EntitiesManager.Ins.CurrentUpgHero.GOType;   
        if (PlayerData.Ins.data.DicDataPlayer.ContainsKey(goType))
        {
            nameCharacter.text = EntitiesManager.Ins.CurrentUpgHero.name;
            health.text = PlayerData.Ins.data.DicDataPlayer[goType].HP.ToString();
            damage.text = PlayerData.Ins.data.DicDataPlayer[goType].Damage.ToString();
            atkSpeed.text = PlayerData.Ins.data.DicDataPlayer[goType].AttackSpeed.ToString();
            moveSpeed.text = PlayerData.Ins.data.DicDataPlayer[goType].MoveSpeed.ToString();

            SetBonusHP(PlayerData.Ins.data.DicDataPlayer[goType].HPBonus);
            SetBonusDamage(PlayerData.Ins.data.DicDataPlayer[goType].DamageBonus);
            SetBonusAtkSpeed(PlayerData.Ins.data.DicDataPlayer[goType].AttackSpeedBonus);
            SetBonusMoveSpeed(PlayerData.Ins.data.DicDataPlayer[goType].MoveSpeedBonus);
        }
    }    
    public void SetBonusHP(float hp)
    {
        hpPlus.text = " +(" + hp.ToString() + ")";
    }
    public void SetBonusDamage(float damage)
    {
        dmgPlus.text = " +(" + damage.ToString() + ")";
    }
    public void SetBonusAtkSpeed(float atkSpeed)
    {
        atkSpeedPlus.text = " +(" + atkSpeed.ToString() + ")";
    }
    public void SetBonusMoveSpeed(float moveSpeed)
    {
        moveSpeedPlus.text = " +(" + moveSpeed.ToString() + ")";
    }
    public void CloseUI()
    {
        Close(0);
        GameManager.ChangeState(GameState.MainMenu);
    }    
}
