using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManageTeam : UICanvas
{
    [SerializeField] GameplayIcon icon;
    [SerializeField] ManagerSO managerSO;
    [SerializeField] Transform parentIconCollection;
    [SerializeField] Transform parentIconInDesk;
    private bool isInCollection;
    public override void Setup()
    {
        base.Setup();
        GameManager.ChangeState(GameState.ManageTeam);
        CreateGameIcon();
    }
    private void CreateGameIcon()
    {
        if (LevelManager.Ins.ListIcon.Count <= 0)
        {
            for (int i = 0; i < managerSO.ListCharacterSO.Count - 6; i++)
            {
                GameplayIcon newIcon = Instantiate(icon);
                managerSO.SetValueForIcon(newIcon, i);
                newIcon.OnInit();
                //TeamData.Ins.AddToCollection(newIcon.GOType);
                if (!TeamData.Ins.data.IsCheckInCollection(newIcon.GOType))
                {
                    if(!TeamData.Ins.data.IsCheckInDesk(newIcon.GOType))
                    {
                        newIcon.TF.SetParent(parentIconCollection);                      
                        TeamData.Ins.AddToCollection(newIcon.GOType);
                    }
                    else
                    {
                        newIcon.TF.SetParent(parentIconInDesk);
                        //TeamData.Ins.AddToDesk(newIcon.GOType);
                    }
                }
                else
                {
                    //Destroy(newIcon.gameObject);
                    newIcon.TF.SetParent(parentIconCollection);
                    TeamData.Ins.AddToCollection(newIcon.GOType);
                    TeamData.Ins.RemoveFromCollection(newIcon.GOType);
                    

                }
                //else if (TeamData.Ins.data.ListInDesk.Contains(newIcon))
                //{
                //    newIcon.TF.SetParent(parentIconInDesk);
                //}
            }
        }

    }
    public void ManageCharacter(GameplayIcon icon)
    {
        if (TeamData.Ins.data.IsCheckInCollection(icon.GOType))
        {
            Destroy(icon.gameObject);
            TeamData.Ins.RemoveFromCollection(icon.GOType);
            for (int i = 0; i < managerSO.ListCharacterSO.Count - 6; i++)
            {
                GameplayIcon newIcon = Instantiate(icon, parentIconInDesk);
                managerSO.SetValueForIcon(newIcon, i);
                newIcon.OnInit();
                TeamData.Ins.AddToDesk(newIcon.GOType);
            }
        }
        else if(TeamData.Ins.data.IsCheckInDesk(icon.GOType))
        {
            Destroy(icon.gameObject);
            TeamData.Ins.RemoveFromCollection(icon.GOType);
            for (int i = 0; i < managerSO.ListCharacterSO.Count - 6; i++)
            {
                GameplayIcon newIcon = Instantiate(icon, parentIconCollection);
                managerSO.SetValueForIcon(newIcon, i);
                newIcon.OnInit();
                TeamData.Ins.AddToCollection(newIcon.GOType);
            }
        }    
    }    
    public void CloseUI()
    {
        Close(0);
        GameManager.ChangeState(GameState.MainMenu);
    }    
}
