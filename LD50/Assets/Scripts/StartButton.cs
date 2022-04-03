using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public static StartButton main;

    [SerializeField]
    public Image imgFireDisplay;
    [SerializeField]
    public GameObject fireContainer;
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
    public void Enable(int fireCount)
    {
        if (fireCount == 0)
        {
            fireContainer.SetActive(false);
        }
        else
        {
            fireContainer.SetActive(true);
        }
        imgFireDisplay.rectTransform.sizeDelta = new Vector2(20f * fireCount, 50f);
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
