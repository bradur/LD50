using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPoolCollider : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private FishPool pool;
    [SerializeField]
    private SpriteMask mask;
    public void Initialize(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        mask.sprite = sprite;
        gameObject.AddComponent<PolygonCollider2D>();
        pool = GetComponentInParent<FishPool>();
    }
    // Start is called before the first frame update
    public void StartFishing()
    {
        if (pool != null)
        {
            pool.StartFishing();
        }
    }

    public void StopFishing()
    {
        if (pool != null)
        {
            pool.StopFishing();
        }
    }
}
