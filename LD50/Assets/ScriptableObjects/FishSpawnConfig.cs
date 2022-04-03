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

    [SerializeField]
    [Range(0.5f, 5f)]
    private float intervalMin = 1f;
    [SerializeField]
    [Range(2f, 10f)]
    private float intervalMax = 3f;

    public float GetRandomIntervalInRange()
    {
        return Random.Range(intervalMin, intervalMax);
    }

    public void Init()
    {
        spawn.Init(this);
        foreach (Fish fish in spawn.fishes)
        {
            PlayerInventory.main.AddFish(fish);
        }
    }

    [SerializeField]
    private List<FishVisual> fishVisuals;

    public FishVisual GetVisual(FishType type)
    {
        return fishVisuals.FirstOrDefault(x => x.Type == type);
    }
}

[System.Serializable]
public class FishSpawn
{
    public List<Fish> fishes;

    public void Init(FishSpawnConfig config)
    {
        foreach (Fish fish in fishes)
        {
            fish.Init(config.GetVisual(fish.Type));
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
    public float SpeedInPool = 2f;
    public float SpeedOutSidePool = 1f;
    public FishType Type;
    private FishVisual visual;
    public FishVisual Visual { get { return visual; } }
    private int spawnCount = 0;
    public void Init(FishVisual visual)
    {
        this.visual = visual;
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

[System.Serializable]
public class FishVisual
{

    public FishType Type;
    public Sprite sprite;
    public Sprite fgSprite;
    public Color fgColor;
    [Range(0.2f, 2f)]
    public float Scale = 1f;
}

public enum FishType
{
    Small,
    Big
}