using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingFishCollider : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name + " collision");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name + " trigger");
    }
}
