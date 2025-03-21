using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICombatText : MonoBehaviour
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
    [SerializeField] TextMeshProUGUI combatText;
    [SerializeField] Vector3 offset;
    private float endY = 2;
    private float timeDuration = 2;
    public void OnInit(Vector3 tf, string text, Color color)
    {
        combatText.transform.position = tf;// + offset;
        combatText.text = text;
        combatText.color = color;
        combatText.transform.DOMoveY(combatText.transform.position.y + endY, timeDuration);
        combatText.DOFade(0, timeDuration).OnComplete(DestroyCombatText);
    }    
    private void DestroyCombatText()
    {
        Destroy(gameObject);
    }    
}
