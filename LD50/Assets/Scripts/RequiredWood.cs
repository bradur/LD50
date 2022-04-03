using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredWood : MonoBehaviour
{
    [SerializeField]

    SpriteRenderer activesprite;
    [SerializeField]
    SpriteRenderer inActiveSprite;
    public void Activate()
    {
        activesprite.enabled = true;
        inActiveSprite.enabled = false;
    }

    public void Deactivate()
    {
        activesprite.enabled = false;
        inActiveSprite.enabled = true;
    }
}
