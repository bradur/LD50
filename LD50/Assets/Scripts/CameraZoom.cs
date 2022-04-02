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
    private float minSize;
    private float targetSize;
    private float originalSize;
    [SerializeField]
    private float zoomDuration = 1f;
    private float timer = 0f;
    private bool zooming = false;
    private bool zoomApplied = true;
    void Start()
    {
        minSize = Camera.main.orthographicSize;
        targetSize = minSize;
        zoom = minSize;
    }

    public void ZoomIn()
    {
        originalSize = Camera.main.orthographicSize;
        timer = 0f;
        targetSize = maxSize;
        zooming = true;
    }

    public void ZoomOut()
    {
        originalSize = Camera.main.orthographicSize;
        timer = 0f;
        targetSize = minSize;
        zooming = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (zooming)
        {
            timer += Time.deltaTime;
            if (timer > zoomDuration)
            {
                zooming = false;
                zoom = targetSize;
            }
            else
            {
                zoom = Mathf.Lerp(originalSize, targetSize, timer / zoomDuration);
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
