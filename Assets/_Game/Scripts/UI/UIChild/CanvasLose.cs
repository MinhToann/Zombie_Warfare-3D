using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLose : UICanvas
{
    public override void Setup()
    {
        base.Setup();
        GameManager.ChangeState(GameState.Lose);
    }
    public void ExitButton()
    {
        UIManager.Ins.CloseAll();
        LevelManager.Ins.ResetAll();
        UIManager.Ins.OpenUI<CanvasChangeScene>();
    }
    public void RetryButton()
    {
        Close(0);
        LevelManager.Ins.ResetAll();
        UIManager.Ins.GetUI<CanvasGameplay>().Setup();
        UIManager.Ins.GetUI<CanvasGameplay>().Open();
    }    
}
