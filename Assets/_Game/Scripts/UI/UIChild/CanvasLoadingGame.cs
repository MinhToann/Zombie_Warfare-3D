using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasLoadingGame : UICanvas
{
    [SerializeField] Image loadingBar;
    public bool EndLoading => loadingBar.fillAmount == 1;
    public override void Setup()
    {
        base.Setup();
        GameManager.ChangeState(GameState.LoadingGame);
        ChangeAnim(Constant.ANIM_APPEAR);
        loadingBar.fillAmount = 0;
    }
    public override void Update()
    {
        base.Update();
        if (!EndLoading)
        {
            loadingBar.fillAmount += Time.deltaTime * 0.3f;
        }    
        else
        {
            ChangeAnim(Constant.ANIM_DISSAPPEAR);
            UIManager.Ins.OpenUI<CanvasMainMenu>();
            Invoke(nameof(CloseUI), 0.8f);
        }    
    }
    private void CloseUI()
    {
        Close(0);
    }    
}
