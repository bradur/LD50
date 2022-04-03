using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            bool canCarry = PlayerInventory.main.GetWood(transform.position);
            if (canCarry)
            {
                GetComponentInParent<DriftWood>().Kill();
            }
        }
    }
}
