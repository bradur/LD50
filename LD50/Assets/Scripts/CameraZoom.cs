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
    [SerializeField]
    private float maxZoom = 0.8f;
    private float minZoom = 1;
    private float originalSize;
    [SerializeField]
    private float zoomDuration = 1f;
    private float timer = 0f;
    private bool zoomIn = false;
    private bool zoomOut = false;
    void Start()
    {
        zoom = minZoom;
        originalSize = Camera.main.orthographicSize;
    }

    public void ZoomIn()
    {
        zoomIn = true;
        zoomOut = false;
    }

    public void ZoomOut()
    {
        zoomOut = true;
        zoomIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomIn)
        {
            timer += Time.deltaTime;
            if (timer > zoomDuration)
            {
                zoomIn = false;
                timer = 0f;
                zoom = maxZoom;
            }
            else
            {
                zoom = Mathf.Lerp(minZoom, maxZoom, timer / zoomDuration);
                Camera.main.orthographicSize = originalSize * zoom;
            }
        }
        else if (zoomOut)
        {
            timer += Time.deltaTime;
            if (timer > zoomDuration)
            {
                zoomIn = true;
                timer = 0f;
                zoom = minZoom;
            }
            else
            {
                zoom = Mathf.Lerp(maxZoom, minZoom, timer / zoomDuration);
                Camera.main.orthographicSize = originalSize * zoom;
            }
        }

    }
}
