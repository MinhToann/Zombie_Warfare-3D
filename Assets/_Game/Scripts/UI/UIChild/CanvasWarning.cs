using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasWarning : UICanvas
{
    public override void Setup()
    {
        base.Setup();
        ChangeAnim(Constant.ANIM_APPEAR);
    }
    public override void Update()
    {
        base.Update();
        Invoke(nameof(Deactive), 3f);
    }
    private void Deactive()
    {
        ChangeAnim(Constant.ANIM_DISSAPPEAR);
        Invoke(nameof(CloseUI), 0.8f);
    }    
    private void CloseUI()
    {
        Close(0);
    }    
}
