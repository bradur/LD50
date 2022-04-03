using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private bool followX = false;
    [SerializeField]
    private bool followY = false;
    [SerializeField]
    private bool followZ = false;

    private float smoothTime = 0.1f;

    Vector3 newPos;
    [SerializeField]
    private UpdateType adjust;
    [SerializeField]
    private UpdateType apply;
    void Start()
    {

    }

    public void SetTarget(Transform target, float smoothTime = 0.1f, UpdateType apply = UpdateType.Fixed)
    {
        this.apply = apply;
        this.smoothTime = smoothTime;
        this.target = target;
    }

    // Update is called once per frame
    private void Update()
    {
        if (adjust == UpdateType.Update)
        {
            AdjustCamera();
        }
        if (apply == UpdateType.Update)
        {
            SetCamera();
        }

    }

    void LateUpdate()
    {

        if (adjust == UpdateType.Late)
        {
            AdjustCamera();
        }
        if (apply == UpdateType.Late)
        {
            SetCamera();
        }
        //transform.position = newPos;
    }

    void FixedUpdate()
    {
        if (adjust == UpdateType.Fixed)
        {
            AdjustCamera();
        }
        if (apply == UpdateType.Fixed)
        {
            SetCamera();
        }
    }

    private void SetCamera()
    {
        //transform.position = Vector3.Lerp(transform.position, newPos, smoothTime);
        Vector3 velocity = Vector3.zero;
        //Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
    }
    private void AdjustCamera()
    {
        Vector3 targetPos = transform.position;
        // follow target
        if (followX)
        {
            targetPos.x = target.position.x;
        }
        if (followY)
        {
            targetPos.y = target.position.y;
        }
        if (followZ)
        {
            targetPos.z = target.position.z;
        }
        newPos = targetPos;
    }

}

public enum UpdateType
{
    Update,
    Fixed,
    Late
}