using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.main.IsWalking)
        {
            PlayClip("pointerWalk");
        }
        else if (PlayerFishingControl.main.IsFishing)
        {
            PlayClip("pointerIdleFishing");
        }
        else
        {
            PlayClip("pointerIdle");
        }
    }

    private void PlayClip(string clipName)
    {
        if (!ClipIsPlaying(clipName))
        {
            animator.Play(clipName);
        }
    }

    private bool ClipIsPlaying(string clipName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(clipName);
    }
}
