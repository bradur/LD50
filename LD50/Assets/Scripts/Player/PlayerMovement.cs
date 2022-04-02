using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Awake()
    {
        main = this;
    }
    public static PlayerMovement main;
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed = 5f;
    private float moveHorizontal;
    private float moveVertical;
    private Vector3 mousePos;
    Vector2 movement = Vector2.zero;

    private bool isWalking = false;
    public bool IsWalking { get { return isWalking; } }

    private bool turnedLeft = false;
    private bool turnedRight = true;

    //private float minMagnitude = 0.4f;

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

    public void TurnRight()
    {
        if (turnedRight)
        {
            return;
        }
        turnedRight = true;
        turnedLeft = false;
        Vector3 rotation = transform.eulerAngles;
        rotation.y = 0;
        transform.eulerAngles = rotation;
    }

    public void TurnLeft()
    {
        if (turnedLeft)
        {
            return;
        }
        turnedLeft = true;
        turnedRight = false;
        Vector3 rotation = transform.eulerAngles;
        rotation.y = 180;
        transform.eulerAngles = rotation;
    }

    private bool movingLeft
    {
        get
        {
            return rb.velocity.x < 0f;
        }
    }

    private bool movingRight
    {
        get
        {
            return rb.velocity.x > 0.001f;
        }
    }


    private void Walk()
    {
        rb.velocity = movement * moveSpeed;
        if (!isWalking)
        {
            isWalking = true;
            PlayerFishingControl.main.StopFishing();
            PlayerAnimator.main.Walk();
        }

        if (movingLeft)
        {
            TurnLeft();
        }
        else if (movingRight)
        {
            TurnRight();
        }

    }

    public void StopWalking()
    {
        if (isWalking)
        {
            isWalking = false;
            rb.velocity = Vector2.zero;
            PlayerAnimator.main.IdleFromWalk();
        }
    }

    private bool MovementKeyIsPressed()
    {
        return (
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow)
        );
    }
    private void FixedUpdate()
    {
        // 2d top down movement
        movement = new Vector2(moveHorizontal, moveVertical);
        if (MovementKeyIsPressed())
        {
            Walk();
        }
        else
        {
            StopWalking();
        }

        // rotate towards mouse
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
