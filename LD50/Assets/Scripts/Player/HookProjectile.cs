using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookProjectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float projectilespeed = 5f;
    [SerializeField]
    private float maxLifeTime = 8f;
    private float lifeTimer = 0f;

    [SerializeField]
    private float maxDistance = 10f;

    // Start is called before the first frame update
    private bool isShot = false;
    private bool isPast = true;
    private Vector2 targetPos;
    private Vector2 startingPos;
    public void Shoot(Vector2 target)
    {
        targetPos = target;
        startingPos = rb.transform.position;
        rb.gameObject.SetActive(true);

        rb.transform.right = new Vector3(target.x, target.y, 0f) - rb.transform.position;
        rb.AddForce(rb.transform.right * projectilespeed, ForceMode2D.Impulse);
        isPast = false;
        isShot = true;
    }

    public void CancelShot()
    {
        Kill();
    }

    private void Update()
    {
        if (isShot)
        {
            lifeTimer += Time.deltaTime;
            bool maxDistReached = Vector2.Distance(startingPos, rb.transform.position) > maxDistance;
            if (maxDistReached || lifeTimer >= maxLifeTime)
            {
                Debug.Log($"hook died cause of dist: {maxDistReached}");
                Kill();
                return;
            }
            /*float dist = Vector2.Distance(rb.transform.position, targetPos);
            if (isPast)
            {
                if (dist > 2f)
                {
                    Kill();
                    isPast = false;
                }
            }
            else if (dist < 0.1f)
            {
                isPast = true;
            }*/
        }
    }

    public void Kill()
    {
        lifeTimer = 0f;
        isShot = false;
        transform.rotation = Quaternion.identity;
        rb.transform.localPosition = Vector2.zero;
        rb.velocity = Vector2.zero;
        rb.gameObject.SetActive(false);
        HookShooter.main.HookDie();
    }
}
