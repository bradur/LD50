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

    private bool hasWood = false;
    private PlayerInventoryItem woodItem;

    private List<PlayerInventoryItem> items = new List<PlayerInventoryItem>();
    public void AddFish(Fish fish)
    {
        PlayerInventoryItem inventoryItem = Instantiate(prefab, transform);
        FishVisual visual = fish.Visual;

        inventoryItem.Init(visual.fgColor, visual.fgSprite, visual.sprite, visual.Scale, transform, items.Count);
        items.Add(inventoryItem);
    }

    public bool GetWood(Vector3 pos)
    {
        if (!hasWood)
        {
            hasWood = true;
            woodItem = Instantiate(prefab, transform, true);
            woodItem.InitWood(transform, pos);
            Debug.Log("Got wood!");
            return true;
        }
        Debug.Log("Already has wood.");
        return false;
    }

    public bool CanUseWood { get { return hasWood; } }
    public bool UseWood()
    {
        if (!hasWood)
        {
            return false;
        }
        woodItem.Kill();
        woodItem = null;
        hasWood = false;
        return true;
    }
}
