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

    void Start()
    {

    }

    public void SetTarget(Transform target, float smoothTime = 0.1f)
    {
        this.smoothTime = smoothTime;
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        // follow target
        if (followX)
        {
            newPos.x = target.position.x;
        }
        if (followY)
        {
            newPos.y = target.position.y;
        }
        if (followZ)
        {
            newPos.z = target.position.z;
        }
        //smoothdamp follow
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);

        //transform.position = newPos;
    }
}
