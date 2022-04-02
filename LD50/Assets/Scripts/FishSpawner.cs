using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private SwimmingFish swimmingFishPrefab;

    [SerializeField]
    private FishSpawnConfig config;
    FishPool pool;
    private bool isSpawning = false;
    private bool initialized = false;
    public void Initialize(FishPool pool)
    {
        if (initialized)
        {
            return;
        }
        this.pool = pool;
        config.Spawn.Init();
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
    private int spawnedFish = 0;
    private bool noMoreFish = false;
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

    public void FishDied(Fish fish)
    {
        spawnedFish--;
        if (noMoreFish && spawnedFish <= 0)
        {
            pool.DryOut();
        }
    }

    private void SpawnAFish()
    {
        Fish fish = config.Spawn.GetRandomFish();
        if (fish == null)
        {
            Debug.Log("out of fish");
            isSpawning = false;
            noMoreFish = true;
            if (spawnedFish == 0)
            {
                pool.DryOut();
            }
            return;
        }
        spawnedFish += 1;
        SwimmingFish swimmingFish = Instantiate(swimmingFishPrefab, pool.transform.position, Quaternion.identity);
        swimmingFish.Initialize(fish);
        swimmingFish.SwimThrough(pool, this);
        Debug.Log($"Spawned a fish: {fish.Name} at {swimmingFish.transform.position}");
    }
}

