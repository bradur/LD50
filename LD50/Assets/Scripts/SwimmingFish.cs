using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingFish : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float distanceFromCenter = 10f;

    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D rb;
    public void Initialize(FishSpawn spawn)
    {
        rb = spriteRenderer.GetComponent<Rigidbody2D>();
        spriteRenderer.sprite = spawn.sprite;
        spriteRenderer.gameObject.AddComponent<PolygonCollider2D>();
    }

    public void SwimThrough(FishPool fishPool)
    {
        Vector2 target = fishPool.transform.position;
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

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceFromCenter);
    }
}
