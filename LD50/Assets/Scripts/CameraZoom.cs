using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public static CameraZoom main;
    private void Awake()
    {
        main = this;
    }
    private float zoom;

    private float maxSize = 6.5f;
    private float minSize = 8.125f;
    private float skySize = 500f;
    [SerializeField]
    private float skyDur = 5f;
    private float targetSize;
    private float originalSize;
    [SerializeField]
    private float zoomDuration = 1f;
    private float duration = 0f;
    private float timer = 0f;
    private bool zooming = false;
    private bool zoomApplied = true;
    private bool theStart = false;

    private float bufferDuration = 2f;
    private bool isBuffer = false;
    void Start()
    {
        targetSize = minSize;
        zoom = minSize;
    }

    public void StartGame()
    {
        ZoomInStart();

    }

    private void ZoomInStart()
    {
        theStart = true;
        ZoomIn();
    }

    public void ZoomIn()
    {
        originalSize = Camera.main.orthographicSize;
        zoom = originalSize;
        timer = 0f;
        targetSize = maxSize;
        zooming = true;
        duration = zoomDuration;
    }

    public void ZoomOut()
    {
        originalSize = Camera.main.orthographicSize;
        zoom = originalSize;
        timer = 0f;
        targetSize = minSize;
        zooming = true;
        duration = zoomDuration;
    }

    public void ZoomOutToSky(int campFires)
    {
        originalSize = Camera.main.orthographicSize;
        timer = 0f;
        targetSize = skySize;
        zooming = true;
        duration = skyDur;
        StartButton.main.Enable(campFires);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuffer)
        {
            timer += Time.deltaTime;
            if (timer >= bufferDuration)
            {
                isBuffer = false;
                timer = 0f;
                DayNightManager.main.Init();
            }
            return;
        }
        if (zooming)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                zooming = false;
                zoom = targetSize;
                if (theStart)
                {
                    timer = 0f;
                    isBuffer = true;
                    theStart = false;
                    DayNightManager.main.AddStartingFood();
                }
            }
            else
            {
                zoom = Mathf.Lerp(originalSize, targetSize, timer / duration);
            }
            zoomApplied = false;
        }
    }

    void LateUpdate()
    {
        AdjustZoom();
    }

    public void AdjustZoom()
    {
        if (zooming || !zoomApplied)
        {
            Camera.main.orthographicSize = zoom;
            zoomApplied = true;
        }
    }
}
