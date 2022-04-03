using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftWoodSpawner : MonoBehaviour
{
    public static DriftWoodSpawner main;
    private void Awake()
    {
        main = this;
    }
    [SerializeField]
    private DriftWood driftWoodPrefab;

    [SerializeField]
    private float distanceFromIsland = 10;

    private List<Vector2> spawnedPositions;

    [SerializeField]
    private Transform target;
    int woodSpawned = 0;
    int targetCount = 3;
    public int WoodSpawned { get { return woodSpawned; } }

    [SerializeField]
    private float spawnInterval = 5f;

    [SerializeField]
    private float minDistance = 5f;
    private float timer = 0f;

    bool isSpawning = false;
    public void StartSpawning()
    {
        spawnedPositions = new List<Vector2>();
        woodSpawned = 0;
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
                if (woodSpawned >= targetCount)
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
        Vector2 spawnPos = randomDir * distanceFromIsland;
        Vector2 newPos = (Vector2)target.position + spawnPos;
        return newPos;
    }
    private void Spawn(Vector2 newPos)
    {
        DriftWood driftWood = Instantiate(driftWoodPrefab, transform);
        driftWood.Initialize(newPos);
        woodSpawned += 1;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceFromIsland);
    }
}
