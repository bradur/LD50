using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public static StartButton main;
    private void Awake()
    {
        main = this;
    }
    [SerializeField]
    GameObject ui;

    private void Start()
    {
        MusicManager.main.PlayMusic(MusicTrackType.Intro);
    }

    private bool isStarted = false;
    public void Disable()
    {
        ui.SetActive(false);
    }
    public void Enable()
    {
        isStarted = false;
        ui.SetActive(true);
    }
    public void StartGame()
    {
        if (isStarted)
        {
            return;
        }
        Disable();
        CameraZoom.main.StartGame();
        isStarted = true;
    }

    private void Update()
    {
        if (isStarted)
        {
            return;
        }
        else
        {
            if (Input.anyKeyDown)
            {
                StartGame();
            }
        }
    }
}
