using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControl : MonoBehaviour
{
    [SerializeField] Animator animator;

    public static FadeControl Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        animator.SetBool("FadeOut", false);
    }
    
    public void FadeOut()
    {
        animator.SetBool("FadeOut", true);
    }

}
