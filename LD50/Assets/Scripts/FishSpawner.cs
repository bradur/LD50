using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private SwimmingFish swimmingFishPrefab;

    [SerializeField]
    private List<FishSpawn> fishes;
    FishPool pool;
    private bool isSpawning = false;
    public void Initialize(FishPool pool)
    {
        this.pool = pool;
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private float spawnTimer = 0;
    [SerializeField]
    private float spawnInterval = 1;
    private void Update()
    {
        if (isSpawning)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0;
                SpawnAFish();
            }
        }
    }

    private void SpawnAFish()
    {
        FishSpawn spawn = fishes[Random.Range(0, fishes.Count)];
        SwimmingFish fish = Instantiate(swimmingFishPrefab, pool.transform.position, Quaternion.identity);
        fish.Initialize(spawn);
        fish.SwimThrough(pool);
        Debug.Log($"Spawned a fish: {spawn.Name} at {fish.transform.position}");
    }
}

[System.Serializable]
public class FishSpawn
{
    public Sprite sprite;
    public string Name;
}
