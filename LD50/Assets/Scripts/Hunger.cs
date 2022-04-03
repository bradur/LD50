using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    public static Hunger main;
    void Awake()
    {
        main = this;
    }

    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    GameObject container;
    [SerializeField]
    Animator animator;

    int rowSize = 5;

    public void Show()
    {
        animator.Play("hungerShow");
    }

    public void ShowHunger(int fishReq)
    {
        spriteRenderer.size = new Vector2(fishReq, 1);
    }

    public void Hide()
    {
        animator.Play("hungerHide");
    }
}
