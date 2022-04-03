using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    // Start is called before the first frame update
    public static CampFire main;
    private void Awake()
    {
        main = this;
    }

    [SerializeField]
    private GameObject fire;
    [SerializeField]
    private GameObject requiredWoodObj;
    [SerializeField]
    private Animator animator;


    [SerializeField]
    private List<RequiredWood> requiredWoods = new List<RequiredWood>();
    void Start()
    {

    }

    private int wood = 0;

    private float extinguishDelay = 0.5f;
    private float timer = 0f;

    public bool CanAddWood { get { return wood + 1 <= requiredWoods.Count; } }
    public void AddWood()
    {
        if (wood < requiredWoods.Count)
        {
            RequiredWood rWood = requiredWoods[wood];
            rWood.Activate();
        }
        wood += 1;
        if (wood >= requiredWoods.Count)
        {
            DayNightManager.main.TransitionToNight();
            fire.SetActive(true);
        }
    }

    private void Update()
    {
        if (fire.activeSelf && DayNightManager.main.IsNightFully)
        {
            timer += Time.deltaTime;
            if (timer >= extinguishDelay)
            {
                Extinguish();
                timer = 0f;
            }
        }
    }

    public void PoolsDried()
    {
        requiredWoodObj.SetActive(true);
        DriftWoodSpawner.main.StartSpawning();
    }

    public void Extinguish()
    {
        foreach (RequiredWood rWood in requiredWoods)
        {
            rWood.Deactivate();
        }
        requiredWoodObj.SetActive(false);
        fire.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && PlayerInventory.main.CanUseWood && CanAddWood)
        {
            PlayerInventory.main.UseWood();
            AddWood();
        }
    }
}
