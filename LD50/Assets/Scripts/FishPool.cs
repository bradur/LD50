using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPool : MonoBehaviour
{

    [SerializeField]
    private List<Sprite> possiblePoolSprite;

    [SerializeField]
    private FishPoolCollider poolCollider;

    private GameObject fishPoolVisual;

    [SerializeField]
    private FishSpawner fishSpawner;


    [SerializeField]
    private Animator animator;

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        Sprite poolSprite = GetRandomPoolSprite();
        poolCollider.Initialize(poolSprite);
    }

    private Sprite GetRandomPoolSprite()
    {
        return possiblePoolSprite[Random.Range(0, possiblePoolSprite.Count)];
    }

    public void StartFishing()
    {
        fishSpawner.Initialize(this);
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
        Destroy(gameObject);
    }
}
