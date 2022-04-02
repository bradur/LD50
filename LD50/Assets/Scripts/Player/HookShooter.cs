using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShooter : MonoBehaviour
{
    public static HookShooter main;
    private void Awake()
    {
        main = this;
    }
    [SerializeField]
    private HookProjectile hookPrefab;

    private HookProjectile hook;

    private bool isShooting = false;

    public bool CanShoot { get { return !isShooting; } }

    public void CancelShot()
    {
        if (isShooting)
        {
            if (hook)
            {
                hook.CancelShot();
            }
        }
    }

    public void Shoot(Vector2 pos)
    {
        if (!CanShoot)
        {
            return;
        }
        if (hook == null)
        {
            hook = Instantiate(hookPrefab, transform.position, Quaternion.identity);
        }
        hook.transform.position = transform.position;
        hook.Shoot(pos);
        isShooting = true;
    }

    public void HookDie()
    {
        isShooting = false;
    }
}
