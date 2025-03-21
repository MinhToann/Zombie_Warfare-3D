using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarProgress : MonoBehaviour
{
    private Transform tf;
    public Transform TF
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
    [SerializeField] GoldStar currentGoldStar;
    public GoldStar CurrentGoldStar => currentGoldStar;

    [SerializeField] Image goldStarImg;
    public Image GoldStarImage => goldStarImg;
    [SerializeField] Transform tfSpawnGoldStar;

    private bool isShining;
    public bool IsShining => isShining;
    private void Start()
    {
        
    }
    public void OnInit()
    {
        float alpha = 0;
        Color currentColor = goldStarImg.color;
        currentColor.a = alpha;
        goldStarImg.color = currentColor;
        goldStarImg.transform.position = tfSpawnGoldStar.position;
        //currentGoldStar.TF.position = tfSpawnGoldStar.position;
    }
    public void SetShining(bool shining)
    {
        isShining = shining;
    }    
}
