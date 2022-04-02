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
        fishSpawner.Initialize(this);
    }

    private Sprite GetRandomPoolSprite()
    {
        return possiblePoolSprite[Random.Range(0, possiblePoolSprite.Count)];
    }

    public void StartFishing()
    {
        animator.Play("poolFishing");
        fishSpawner.StartSpawning();
    }

    public void StopFishing()
    {
        animator.Play("poolIdle");
        fishSpawner.StopSpawning();
    }
}
