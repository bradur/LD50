using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishingControl : MonoBehaviour
{
    private void Awake()
    {
        main = this;
    }
    public static PlayerFishingControl main;
    private bool isFishing = false;
    public bool IsFishing { get { return isFishing; } }
    private void CastRod()
    {
        if (CanFish())
        {
            Debug.Log($"mousePos: {mousePos.x}, playerPos: {transform.position.x}");
            if (mousePos.x > transform.position.x)
            {
                PlayerMovement.main.TurnRight();
            }
            else if (mousePos.x < transform.position.x)
            {
                PlayerMovement.main.TurnLeft();
            }
            PlayerAnimator.main.CastRod();
            PlayerMovement.main.StopWalking();
            isFishing = true;
        }
    }

    private Vector2 mousePos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public bool CanFish()
    {
        if (PlayerMovement.main.IsWalking)
        {
            return false;
        }
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        return !hit.collider || hit.collider.tag != "nonFishable";
    }

    public void StopFishing()
    {
        if (isFishing)
        {
            isFishing = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRod();
        }
    }
}
