using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
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
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = TF.position;
        float elapse = 0f;
        if(elapse < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            TF.position = new Vector3(x, y, originalPos.z);
            elapse += Time.deltaTime;
            yield return null;
        }    
        TF.position = originalPos;
    }    
}
