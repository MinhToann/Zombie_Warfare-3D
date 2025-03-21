using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 difference;
    private Vector3 resetCamera;
    private bool isDrag = false;
    [SerializeField] CameraController mainCam;
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
    private void Start()
    {
        resetCamera = Camera.main.transform.position;
        //Camera.main.orthographicSize = 4.5f;
        //Camera.main.DO
        //if(GameManager.IsState(GameState.MainMenu))
        //{
            
        //}
        
    }
    private void LateUpdate()
    {
        if(GameManager.IsState(GameState.MainMenu))
        {
            Camera.main.DOOrthoSize(4.5f, 2f);
            if (Input.GetMouseButton(0))
            {
                difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
                if (!isDrag)
                {
                    isDrag = true;
                    origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                isDrag = false;
            }
            if (isDrag)
            {
                Camera.main.transform.position = origin - difference;
            }
            if (Input.GetMouseButton(1))
            {
                Camera.main.transform.position = resetCamera;
            }
            if(LevelManager.Ins.IsMainMenu)
            {
                TF.position = new Vector3(10, 5.5f, -5);
                LevelManager.Ins.SetBoolIsMenu(false);
            }
        }    
        
    }
}
