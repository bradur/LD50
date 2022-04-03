using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPool : MonoBehaviour
{


    [SerializeField]
    private FishPoolCollider poolCollider;

    private GameObject fishPoolVisual;

    [SerializeField]
    private FishSpawner fishSpawner;


    [SerializeField]
    private Animator animator;
    FishSpawnConfig config;


    public void Initialize(Sprite poolSprite, FishSpawnConfig config, Vector2 pos)
    {
        this.config = config;
        //Sprite poolSprite = GetRandomPoolSprite();
        poolCollider.Initialize(poolSprite);
        transform.position = pos;
    }


    public void StartFishing()
    {
        fishSpawner.Initialize(this, config);
        animator.SetTrigger("fishing");
        fishSpawner.StartSpawning();
    }

    public void StopFishing()
    {
        animator.SetTrigger("stopFishing");
        fishSpawner.StopSpawning();
    }

    public void DryOut()
    {
        animator.SetTrigger("dryOut");
        fishSpawner.StopSpawning();
    }

    public void FinishDryingOut()
    {
        Debug.Log("Fish have gone!");
        Camera.main.GetComponent<FollowTarget>().SetTarget(PlayerMovement.main.transform);
        //StopFishing();
        PlayerFishingControl.main.StopFishing();
        DayNightManager.main.AddTimeStep();
        FishPoolManager.main.FishPoolDie();
        Destroy(gameObject);
    }
}
