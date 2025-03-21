using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBuilding : MonoBehaviour
{
    private bool isShining;
    public bool IsShining => isShining;
    public void SetShining(bool shining)
    {
        isShining = shining;
    }      
}
