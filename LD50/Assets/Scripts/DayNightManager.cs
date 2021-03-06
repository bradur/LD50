using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager main;
    private void Awake()
    {
        main = this;
    }
    [SerializeField]
    private Light2D light2D;

    [SerializeField]
    private float intensityStep = -0.1f;
    [SerializeField]
    private float intensityStepDuration = 1f;

    [SerializeField]
    private float nightTransitionDuration = 1f;
    [SerializeField]
    private float nightIntensity = 0;
    [SerializeField]
    private float dayTransitionDuration = 1f;

    private float dayIntensity = 0f;

    [SerializeField]
    private float nightDuration = 5f;
    private float timer = 0f;

    private float duration = 0f;
    private float originalIntensity = 0f;
    private float targetIntensity = 0f;
    private bool isTransition = false;
    private bool isNight = true;
    public bool IsNight { get { return isNight; } }
    public bool IsNightFully { get { return !isTransition && isNight; } }

    private bool dayStarted = false;

    [SerializeField]
    private float morningRitualDuration = 0f;
    private int eatFishCount = 0;
    private int eatFishTarget = 3;
    public int EatFishTarget { get { return eatFishTarget; } }
    [SerializeField]
    private int originalFishTarget = 3;
    private bool isMorningRitual = false;
    private bool isOver = false;
    private bool zoomStarted = false;
    [SerializeField]
    private float deathDuration = 5f;

    [SerializeField]
    private Sprite foodSprite;
    [SerializeField]
    private Color foodColor = Color.red;
    private bool initialized = false;

    private int campfiresMade = 0;

    public void AddStartingFood()
    {
        for (int index = 0; index < originalFishTarget; index += 1)
        {
            PlayerInventory.main.AddItem(foodColor, foodSprite, foodSprite);
        }
        //PlayerInventory.main.AddItem(foodColor, foodSprite, foodSprite);
        //PlayerInventory.main.AddItem(foodColor, foodSprite, foodSprite);
    }

    public void Init()
    {
        eatFishTarget = originalFishTarget;
        isNight = true;
        isTransition = false;
        dayStarted = false;
        dayIntensity = light2D.intensity;
        eatFishCount = 0;
        initialized = true;
        zoomStarted = false;
        campfiresMade = 0;
        isOver = false;
        PlayerAnimator.main.Idle();
        MusicManager.main.PlayMusic(MusicTrackType.Main);
    }

    public void StartMorningRitual()
    {
        Debug.Log("morning ritual started");
        eatFishCount = 0;
        isMorningRitual = true;
        Hunger.main.ShowHunger(eatFishTarget);
        Hunger.main.Show();
    }

    public void DayStarted()
    {
        isNight = false;
        FishPoolManager.main.StartSpawning();
        dayStarted = true;
        eatFishTarget += 1;
        campfiresMade += 1;
    }
    public void AddTimeStep()
    {
        originalIntensity = light2D.intensity;
        Transition(intensityStepDuration, originalIntensity + intensityStep);
    }

    public void TransitionToNight()
    {
        isNight = true;
        Transition(nightTransitionDuration, nightIntensity);
    }

    public void TransitionToDay()
    {
        dayStarted = false;
        Transition(dayTransitionDuration, dayIntensity);
    }

    private void Transition(float tDuration, float target)
    {
        isTransition = true;
        timer = 0f;
        duration = tDuration;
        originalIntensity = light2D.intensity;
        targetIntensity = target;
    }

    void Update()
    {
        if (!initialized)
        {
            return;
        }
        if (zoomStarted)
        {
            return;
        }
        if (isOver)
        {
            timer += Time.deltaTime;
            if (timer >= deathDuration)
            {
                zoomStarted = true;
                CameraZoom.main.ZoomOutToSky(campfiresMade);
                Hunger.main.Hide();
                initialized = false;
                timer = 0f;
            }
            return;
        }
        if (isMorningRitual)
        {
            timer += Time.deltaTime;
            if (timer >= morningRitualDuration)
            {
                bool fishWasEaten = PlayerInventory.main.UseFish();
                if (!fishWasEaten)
                {
                    timer = 0f;
                    Debug.Log("you died of hunger!");
                    PlayerAnimator.main.PlayDeath();
                    isMorningRitual = false;
                    isOver = true;
                    return;
                }
                SoundManager.main.PlaySound(GameSoundType.Munch);
                eatFishCount += 1;
                Hunger.main.ShowHunger(eatFishTarget - eatFishCount);
                Debug.Log($"Fishes eaten: {eatFishCount}");
                if (eatFishCount >= eatFishTarget)
                {
                    isMorningRitual = false;
                    Hunger.main.Hide();
                    DayStarted();
                }
                timer = 0f;
            }
            return;
        }
        if (!dayStarted && !isTransition)
        {
            StartMorningRitual();
            return;
        }
        if (isTransition)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                isTransition = false;
                light2D.intensity = targetIntensity;
                timer = 0f;
            }
            else
            {
                light2D.intensity = Mathf.Lerp(originalIntensity, targetIntensity, timer / duration);
            }
        }
        if (IsNightFully)
        {
            timer += Time.deltaTime;
            if (timer >= nightDuration)
            {
                TransitionToDay();
            }
        }


    }
}
