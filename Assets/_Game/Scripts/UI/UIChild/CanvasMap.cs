using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMap : UICanvas
{
    public override void Setup()
    {
        base.Setup();
        GameManager.ChangeState(GameState.Map);
    }
}
