using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldStar : MonoBehaviour
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
    [SerializeField] bool isShining;
    public bool IsShining => isShining;
    public void SetBoolShining(bool isShining)
    {
        this.isShining = isShining;
    }    
}
