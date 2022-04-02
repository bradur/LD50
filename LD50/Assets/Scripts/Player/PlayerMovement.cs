using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed = 5f;
    private float moveHorizontal;
    private float moveVertical;
    private Vector3 mousePos;
    Vector2 movement = Vector2.zero;

    private bool isMoving = false;

    private float minMagnitude = 0.05f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        // 2d top down movement
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        // rotate towards mouse
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void Move()
    {
        rb.velocity = movement * moveSpeed;
        if (!isMoving)
        {
            isMoving = true;
            PlayerAnimator.main.Walk();
        }
    }
    private void FixedUpdate()
    {
        // 2d top down movement
        movement = new Vector2(moveHorizontal, moveVertical);
        if (movement.magnitude > minMagnitude)
        {
            Move();
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                rb.velocity = Vector2.zero;
                PlayerAnimator.main.IdleFromWalk();
            }
        }

        // rotate towards mouse
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
