using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingFish : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteRenderer fgSprite;
    [SerializeField]
    private SpriteRenderer formlessSprite;

    [SerializeField]
    private float distanceFromCenter = 10f;

    private float speedInside = 5f;
    private float speedOutside = 2f;
    /*
        [SerializeField]
        private float maxLifeTime = 10f;*/

    [SerializeField]
    private GameObject container;
    private Rigidbody2D rb;
    private bool isSwimming = false;
    private Vector2 target;
    private FishSpawner spawner;
    private Fish fish;
    public void Initialize(Fish spawn)
    {
        this.fish = spawn;
        speedInside = spawn.SpeedInPool;
        speedOutside = spawn.SpeedOutSidePool;
        isSwimming = false;
        container.SetActive(true);
        rb = spriteRenderer.GetComponent<Rigidbody2D>();
        rb.transform.localScale = new Vector2(spawn.Visual.Scale, spawn.Visual.Scale);
        spriteRenderer.sprite = spawn.Visual.sprite;
        formlessSprite.sprite = spawn.Visual.fgSprite;
        fgSprite.sprite = spawn.Visual.fgSprite;
        fgSprite.color = spawn.Visual.fgColor;
        spriteRenderer.gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
    }


    public void SwimThrough(FishPool fishPool, FishSpawner spawner)
    {
        isSwimming = true;
        this.spawner = spawner;
        target = fishPool.transform.position;
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        if (randomDir.magnitude < 0.5f)
        { // extremely unlikely
            randomDir = Random.insideUnitCircle.normalized;
        }
        Vector2 spawnPos = randomDir * distanceFromCenter;
        Vector2 newPos = target + spawnPos;
        Debug.Log($"Poolpos [{target}] spawnPos: [{spawnPos}] newPos:[{newPos}]");
        rb.transform.position = newPos;
        rb.transform.right = target - newPos;
        rb.AddForce(rb.transform.right * speedOutside, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (isSwimming)
        {
            if (Vector2.Distance(rb.transform.position, target) > distanceFromCenter)
            {
                Debug.Log("Kill cos out");
                Kill();
            }
        }
    }

    public void TriggerEnter(Collider2D other, bool enteredPool)
    {
        if (enteredPool)
        {
            rb.velocity *= (speedInside / speedOutside);
            return;
        }
        HookProjectile projectile = other.gameObject.GetComponentInParent<HookProjectile>();
        if (projectile != null)
        {
            projectile.Kill();
            Kill();
        }
    }

    public void ExitPool()
    {
        //ExitPool
        rb.velocity *= (speedOutside / speedInside);
    }

    public void Kill()
    {
        isSwimming = false;
        spawner.FishDied(fish);
        container.SetActive(false);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceFromCenter);
    }
}
