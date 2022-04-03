using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingFishCollider : MonoBehaviour
{

    private SwimmingFish swimmingFish;

    private bool isFishable = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        bool enteredPool = other.gameObject.layer == LayerMask.NameToLayer("CanFish");
        if (enteredPool)
        {
            Debug.Log("Fish entered pool");
            isFishable = true;
        }
        if (!isFishable)
        {
            return;
        }
        if (swimmingFish == null)
        {
            swimmingFish = GetComponentInParent<SwimmingFish>();
        }
        swimmingFish.TriggerEnter(other, enteredPool);
        Debug.Log(other.name + " trigger");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CanFish"))
        {
            Debug.Log("Fish exited pool");
            swimmingFish.ExitPool();
            isFishable = false;
        }
    }
}
