using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> possiblePool;
    [SerializeField]
    private List<Sprite> possiblePoolSprite;

    [SerializeField]
    private FishPoolCollider poolCollider;

    private GameObject fishPoolVisual;

    [SerializeField]
    private Animator animator;
    private void Start()
    {
        /*foreach (GameObject fish in possiblePool)
        {
            fish.SetActive(false);
        }*/
        Initialize();
    }
    public void Initialize()
    {
        /*fishPoolVisual = GetRandomPool();
        fishPoolVisual.SetActive(true);*/
        Sprite poolSprite = GetRandomPoolSprite();
        poolCollider.Initialize(poolSprite);
    }
    /*private GameObject GetRandomPool()
    {
        return possiblePool[Random.Range(0, possiblePool.Count)];
    }*/

    private Sprite GetRandomPoolSprite()
    {
        return possiblePoolSprite[Random.Range(0, possiblePoolSprite.Count)];
    }

    public void StartFishing()
    {
        animator.Play("poolFishing");
    }

    public void StopFishing()
    {
        animator.Play("poolIdle");
    }
}
