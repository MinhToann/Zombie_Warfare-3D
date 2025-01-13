using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private bool isDestroyOnClose = false;
    [SerializeField] Animator animator;
    protected GameState gameState;
    private Transform tf;
    private string currentAnim;
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
    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float ratio = (float)Screen.width / (float)Screen.height;
        if (ratio > 2.1f)
        {
            Vector2 leftBottom = rect.offsetMin;
            Vector2 rightTop = rect.offsetMax;

            leftBottom.y = 0f;
            rightTop.y = -100f;

            rect.offsetMin = leftBottom;
            rect.offsetMax = rightTop;
        }
    }

    public virtual void Setup()
    {

    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectly), time);
    }
    //Tat canvas truc tiep
    public virtual void CloseDirectly()
    {
        if (isDestroyOnClose)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
    public void ChangeAnim(string newAnim)
    {
        if (currentAnim != newAnim)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = newAnim;
            animator.SetTrigger(currentAnim);
        }
    }
}
