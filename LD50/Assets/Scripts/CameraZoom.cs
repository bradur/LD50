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
    void Start()
    {
        targetSize = minSize;
        zoom = minSize;
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
        timer = 0f;
        targetSize = maxSize;
        zooming = true;
        duration = zoomDuration;
    }

    public void ZoomOut()
    {
        originalSize = Camera.main.orthographicSize;
        timer = 0f;
        targetSize = minSize;
        zooming = true;
        duration = zoomDuration;
    }

    public void ZoomOutToSky()
    {
        originalSize = Camera.main.orthographicSize;
        timer = 0f;
        targetSize = skySize;
        zooming = true;
        duration = skyDur;
    }

    // Update is called once per frame
    void Update()
    {
        if (zooming)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                zooming = false;
                zoom = targetSize;
                if (theStart)
                {
                    DayNightManager.main.Init();
                    theStart = false;
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
