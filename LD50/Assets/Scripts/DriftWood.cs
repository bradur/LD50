using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftWood : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer fgSprite;

    public Sprite FG { get { return fgSprite.sprite; } }
    public Sprite BG { get { return fgSprite.sprite; } }
    public Color Color { get { return fgSprite.color; } }

    public void Initialize(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
