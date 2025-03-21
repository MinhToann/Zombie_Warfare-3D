using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHealthBar : MonoBehaviour
{
    [SerializeField] Image health;
    public Image HealthImg => health;
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
}
