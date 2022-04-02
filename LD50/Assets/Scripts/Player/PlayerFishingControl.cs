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
    private LayerMask CanFishLayer;
    private Vector2 prevMousePos;
    private RaycastHit2D cachedHit;
    private FishPoolCollider currentFishPool;

    private FollowTarget cameraFollow;

    [SerializeField]
    private float maxDistanceFromPool = 5f;
    void Start()
    {
        CanFishLayer = LayerMask.NameToLayer("CanFish");
        cameraFollow = Camera.main.GetComponent<FollowTarget>();
        MousePositionIsFishable();
    }
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

            currentFishPool = cachedHit.collider.GetComponent<FishPoolCollider>();
            if (currentFishPool != null)
            {
                currentFishPool.StartFishing();
                cameraFollow.SetTarget(currentFishPool.transform, 0.2f);
                CameraZoom.main.ZoomIn();
            }
            else
            {
                Debug.LogWarning("Poolcollider null!");
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
        return !PlayerMovement.main.IsWalking && MousePositionIsFishable() && PoolIsCloseEnough(); ;
    }

    private bool PoolIsCloseEnough()
    {
        Debug.Log($"Distance: {Vector2.Distance(cachedHit.transform.position, transform.position)}");
        return Vector2.Distance(cachedHit.transform.position, transform.position) < maxDistanceFromPool;
    }

    public bool MousePositionIsFishable()
    {
        RaycastHit2D hit;
        if (prevMousePos == mousePos)
        {
            hit = cachedHit;
        }
        else
        {
            hit = Physics2D.Raycast(mousePos, Vector2.zero, 10f);
            cachedHit = hit;
        }
        prevMousePos = mousePos;
        return cachedHit.collider && cachedHit.collider.gameObject.layer == CanFishLayer;
    }

    public void StopFishing()
    {
        if (isFishing)
        {
            if (currentFishPool != null)
            {
                currentFishPool.StopFishing();
                cameraFollow.SetTarget(transform);
            }
            CameraZoom.main.ZoomOut();
            isFishing = false;
        }
    }

    void Update()
    {
        if (isFishing)
        {
            /*if (!MousePositionIsFishable())
            {
                Debug.Log("Stop because mouse not over pool");
                StopFishing();
                PlayerAnimator.main.IdleFromFish();
            }*/
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (isFishing)
            {
                HookShooter.main.Shoot(mousePos);
            }
            else
            {
                CastRod();
            }
        }
    }
}
