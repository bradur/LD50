using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    private void Awake()
    {
        main = this;
    }
    public static PlayerInventory main;
    [SerializeField]
    private PlayerInventoryItem prefab;

    private List<PlayerInventoryItem> items = new List<PlayerInventoryItem>();
    public void AddFish(Fish fish)
    {
        PlayerInventoryItem inventoryItem = Instantiate(prefab, transform);
        FishVisual visual = fish.Visual;
        
        inventoryItem.Init(visual.fgColor, visual.fgSprite, visual.sprite, visual.Scale, transform, items.Count);
        items.Add(inventoryItem);
    }
}
