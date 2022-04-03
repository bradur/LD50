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
    private bool isTransition;
    private bool isNight = false;
    public bool IsNight { get { return isNight; } }
    public bool IsNightFully { get { return !isTransition && isNight; } }

    private bool dayStarted = false;

    void Start()
    {
        dayIntensity = light2D.intensity;
        DayStarted();
    }

    public void DayStarted()
    {
        FishPoolManager.main.StartSpawning();
        dayStarted = true;
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
        isNight = false;
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
        if (!dayStarted && !isTransition)
        {
            DayStarted();
        }
    }
}
