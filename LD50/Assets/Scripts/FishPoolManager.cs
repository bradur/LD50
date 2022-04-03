using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPoolManager : MonoBehaviour
{
    public static FishPoolManager main;
    private void Awake()
    {
        main = this;
    }
    [SerializeField]
    private FishPool fishPoolPrefab;

    [SerializeField]
    private float distanceFromIslandX = 10;
    [SerializeField]
    private float distanceFromIslandY = 10;

    [SerializeField]
    private Transform target;


    [SerializeField]
    private List<Sprite> possiblePoolSprite;
    private List<Sprite> sprites;

    [SerializeField]
    private List<FishSpawnConfig> possibleConfigs;
    private List<FishSpawnConfig> configs;
    private int poolsSpawned = 0;
    private int poolsAlive = 0;
    [SerializeField]
    private float minDistance = 5f;
    public void ReInitSprites()
    {
        sprites = new List<Sprite>(possiblePoolSprite);
    }

    public void ReInitConfigs()
    {
        configs = new List<FishSpawnConfig>(possibleConfigs);
    }
    private Sprite GetRandomPoolSprite()
    {
        if (sprites == null || sprites.Count == 0)
        {
            ReInitSprites();
        }
        int randomIndex = Random.Range(0, sprites.Count);
        Sprite sprite = sprites[randomIndex];
        sprites.Remove(sprite);
        return sprite;
    }

    private FishSpawnConfig GetRandomConfig()
    {
        if (configs == null || configs.Count == 0)
        {
            ReInitConfigs();
        }
        int randomIndex = Random.Range(0, configs.Count);
        FishSpawnConfig config = configs[randomIndex];
        configs.Remove(config);
        return config;
    }

    private int pooltargetCount = 4;
    private bool isSpawning = false;
    private float timer = 0f;
    [SerializeField]
    private float spawnInterval = 5f;

    private List<Vector2> spawnedPositions;
    public void StartSpawning()
    {
        spawnedPositions = new List<Vector2>();
        poolsAlive = 0;
        poolsSpawned = 0;
        timer = 0f;
        isSpawning = true;
    }

    private void Update()
    {
        if (isSpawning)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                Vector2 newPos = GetSpawnPos();
                bool fail = false;
                Vector2 closePos = Vector2.zero;
                foreach (Vector2 oldPos in spawnedPositions)
                {
                    if (Vector2.Distance(oldPos, newPos) < minDistance)
                    {
                        closePos = oldPos;
                        fail = true;
                        break;
                    }
                }
                if (fail)
                {
                    Debug.Log($"New pos too close: {newPos} {closePos}");
                    return;
                }
                timer = 0;
                spawnedPositions.Add(newPos);
                Spawn(newPos);
                if (poolsSpawned >= pooltargetCount)
                {
                    isSpawning = false;
                }
            }
        }
    }


    private Vector2 GetSpawnPos()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        if (randomDir.magnitude < 0.5f)
        { // extremely unlikely
            randomDir = Random.insideUnitCircle.normalized;
        }
        Vector2 spawnPos = new Vector2(randomDir.x * distanceFromIslandX, randomDir.y * distanceFromIslandY);
        Vector2 newPos = (Vector2)target.position + spawnPos;
        return newPos;
    }
    private void Spawn(Vector2 newPos)
    {
        FishPool fishPool = Instantiate(fishPoolPrefab, transform);

        fishPool.Initialize(GetRandomPoolSprite(), GetRandomConfig(), newPos);
        poolsSpawned += 1;
        poolsAlive += 1;
    }

    public void FishPoolDie()
    {
        poolsAlive -= 1;
        if (poolsAlive <= 0)
        {
            CampFire.main.PoolsDried();
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, distanceFromIslandX);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceFromIslandY);
    }
}
