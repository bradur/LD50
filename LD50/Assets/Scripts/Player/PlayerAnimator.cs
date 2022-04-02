using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private void Awake()
    {
        main = this;
    }
    public static PlayerAnimator main;
    [SerializeField]
    private Animator animator;

    public void TurnNorth()
    {

    }

    public void TurnEast()
    {

    }

    public void TurnWest()
    {

    }

    public void TurnSouth() { }

    public void Walk()
    {
        animator.Play("walk");
        Debug.Log("walk");
    }

    public void IdleFromWalk()
    {
        if (ClipIsPlaying("walk"))
        {
            Idle();
        }
    }

    public void IdleFromFish()
    {
        if (ClipIsPlaying("fishingIdle") || ClipIsPlaying("swingRod"))
        {
            Idle();
        }
    }

    public void CastRod()
    {
        animator.Play("swingRod");
        Debug.Log("swingRod");
    }


    private bool ClipIsPlaying(string clipName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(clipName);
    }

    public void Idle()
    {
        animator.SetTrigger("idle");
        Debug.Log("idle");
    }
}
