using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasChangeScene : UICanvas
{
    private float timer;
    public override void Setup()
    {
        base.Setup();
        GameManager.ChangeState(GameState.ChangeScene);
        timer = 3.9f;
        LevelManager.Ins.SetBoolIsMenu(true);
    }
    public override void Update()
    {
        base.Update();
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 1f)
            {
                UIManager.Ins.OpenUI<CanvasMainMenu>();
            }    
        }    
        else
        {
            CloseUI();
        }    
    }
    private void CloseUI()
    {
        Close(0);

    }    
}
