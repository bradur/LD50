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
    private float distanceFromCenter = 10f;

    private float speed = 5f;
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
        speed = spawn.Speed;
        isSwimming = false;
        container.SetActive(true);
        rb = spriteRenderer.GetComponent<Rigidbody2D>();
        spriteRenderer.sprite = spawn.sprite;
        fgSprite.sprite = spawn.fgSprite;
        fgSprite.color = spawn.fgColor;
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
        rb.AddForce(rb.transform.right * speed, ForceMode2D.Impulse);
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

    public void TriggerEnter(Collider2D other)
    {

        HookProjectile projectile = other.gameObject.GetComponentInParent<HookProjectile>();
        if (projectile != null)
        {
            projectile.Kill();
            Kill();
        }
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
