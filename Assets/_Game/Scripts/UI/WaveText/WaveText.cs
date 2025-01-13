using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveText : MonoBehaviour
{
    [SerializeField] Animator anim;
    private string currentAnim;
    [SerializeField] Text waveText;

    private void Start()
    {
        SetWaveText(LevelManager.Ins.CurLevel.CurrentNumberWave + 1);
    }
    public void OnInit()
    {        
        ChangeAnim(Constant.ANIM_WAVE_TEXT);
        
    }
    public void SetWaveText(int number)
    {
        waveText.text = "Wave " + number.ToString();
    }
    public void ChangeAnim(string newAnim)
    {
        if(currentAnim != newAnim)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = newAnim;
            anim.SetTrigger(currentAnim);
        }
    }
}
