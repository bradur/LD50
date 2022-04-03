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

    public void AddItem(Color fgColor, Sprite fgSprite, Sprite bgSprite)
    {
        PlayerInventoryItem inventoryItem = Instantiate(prefab, transform);

        inventoryItem.Init(fgColor, fgSprite, bgSprite, 1f, transform, items.Count);
        items.Add(inventoryItem);
    }

    public void AddFish(Fish fish)
    {
        PlayerInventoryItem inventoryItem = Instantiate(prefab, transform);
        FishVisual visual = fish.Visual;

        inventoryItem.Init(visual.fgColor, visual.fgSprite, visual.sprite, visual.Scale, transform, items.Count);
        items.Add(inventoryItem);
    }

    public bool UseFish()
    {
        if (items.Count > 0)
        {
            PlayerInventoryItem item = items[0];
            items.Remove(item);
            item.Kill();
            return true;
        }
        return false;
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
