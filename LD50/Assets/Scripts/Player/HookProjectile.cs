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

    // Start is called before the first frame update
    private bool isShot = false;
    private bool isPast = true;
    private Vector2 targetPos;
    public void Shoot(Vector2 target)
    {
        targetPos = target;
        // rotate towards target
        rb.gameObject.SetActive(true);
        //Vector3 rotation = rb.transform.eulerAngles;
        //rotation.z = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
        //rb.transform.eulerAngles = rotation;
        rb.transform.right = new Vector3(target.x, target.y, 0f) - rb.transform.position;
        //Vector2 dir = (target - (Vector2)transform.position).normalized;
        rb.AddForce(rb.transform.right * projectilespeed, ForceMode2D.Impulse);
        isPast = false;
        isShot = true;
    }

    private void Update()
    {
        if (isShot)
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer >= maxLifeTime)
            {
                Kill();
                return;
            }
            float dist = Vector2.Distance(rb.transform.position, targetPos);
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
            }
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
