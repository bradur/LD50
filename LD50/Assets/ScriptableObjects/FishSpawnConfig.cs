using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "FishSpawnConfig", menuName = "ScriptableObjects/FishSpawnConfig", order = 1)]
public class FishSpawnConfig : ScriptableObject
{
    [SerializeField]
    private FishSpawn spawn;
    public FishSpawn Spawn { get { return spawn; } }
}

[System.Serializable]
public class FishSpawn
{
    public List<Fish> fishes;

    public void Init()
    {
        foreach (Fish fish in fishes)
        {
            fish.Init();
        }
    }
    public Fish GetRandomFish()
    {
        List<Fish> fishies = fishes.Where(f => f.CanSpawn()).ToList();
        Fish fish = null;
        if (fishies.Count > 0)
        {
            fish = fishies[Random.Range(0, fishies.Count)];
            fish.Spawn();
        }
        return fish;
    }

}

[System.Serializable]
public class Fish
{
    public string Name;
    public int Amount;
    public Sprite sprite;
    public Sprite fgSprite;
    public Color fgColor;
    public float Speed = 2f;

    private int spawnCount = 0;
    public void Init()
    {
        spawnCount = 0;
    }
    public bool CanSpawn()
    {
        return spawnCount < Amount;
    }
    public void Spawn()
    {
        spawnCount += 1;
    }
}