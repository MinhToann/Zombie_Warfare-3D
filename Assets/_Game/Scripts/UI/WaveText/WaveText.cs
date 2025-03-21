using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveText : MonoBehaviour
{
    private Transform tf;
    public Transform TF
    {
        get
        {
            if(tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
    [SerializeField] Animator anim;
    private string currentAnim;
    [SerializeField] Text waveText;

    private void Start()
    {
        SetWaveText(LevelManager.Ins.CurrentNumberWave + 1);
    }
    public void OnInit()
    {
        //TF.localScale = Vector3.zero;
        ChangeAnim(Constant.ANIM_APPEAR);
        //Invoke(nameof(ChangeNormalState), 3f);
    }
    private void ChangeNormalState()
    {
        //ChangeAnim(Constant.ANIM_NORMAL);
        //TF.localScale = Vector3.zero;
        Destroy(this.gameObject);
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
