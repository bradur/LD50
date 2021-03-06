using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private SwimmingFish swimmingFishPrefab;

    FishSpawnConfig config;
    FishPool pool;
    private bool isSpawning = false;
    private bool initialized = false;
    public void Initialize(FishPool pool, FishSpawnConfig config)
    {
        if (initialized)
        {
            return;
        }
        this.config = config;
        this.pool = pool;
        config.Init();
    }

    public void StartSpawning()
    {
        spawnInterval = config.GetRandomIntervalInRange();
        Debug.Log($"Spawn interval now: {spawnInterval}");
        spawnTimer = 0;
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
        else
        {
            StartSpawning();
        }
    }

    private void SpawnAFish()
    {
        Fish fish = config.Spawn.GetRandomFish();
        if (fish == null)
        {
            Debug.Log("out of fish");
            StopSpawning();
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
        StopSpawning();
        Debug.Log($"Spawned a fish: {fish.Name} at {swimmingFish.transform.position}");
    }
}

