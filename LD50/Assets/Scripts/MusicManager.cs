using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : MonoBehaviour
{
    public static MusicManager main;
    private void Awake()
    {
        main = this;
    }
    private List<AudioFade> fades = new List<AudioFade>();

    [SerializeField]
    private List<MusicTrack> tracks = new List<MusicTrack>();

    [SerializeField]
    private AudioSource musicSourcePrefab;

    [SerializeField]
    private float volume = 0.8f;

    [SerializeField]
    private float fadeOutDuration = 0.2f;
    [SerializeField]
    private float fadeInDuration = 0.5f;

    MusicTrack currentTrack;

    private void Start()
    {
        foreach (MusicTrack track in tracks)
        {
            if (track.Source == null)
            {
                track.Source = Instantiate(musicSourcePrefab);
                track.Source.clip = track.Clip;
                track.Source.volume = 0;
                track.Source.Play();
            }
        }
    }

    public void PlayMusic(MusicTrackType type)
    {
        MusicTrack track = tracks.Find(x => x.Type == type);
        if (track == null)
        {
            Debug.LogError("No music track found for type: " + type);
            return;
        }
        PlayMusic(track);
    }

    private void PlayMusic(MusicTrack track)
    {
        if (currentTrack == null)
        {
            currentTrack = track;
            currentTrack.Source.volume = volume;
            return;
        }
        if (currentTrack.Source == track.Source)
        {
            return;
        }

        /*if (!currentTrack.Source.isPlaying)
        {
            currentTrack.Source.clip = track.Clip;
            musicSource.volume = volume;
            musicSource.Play();
        }*/
        if (currentTrack.Source.isPlaying)
        {
            AudioSource oldTrack = currentTrack.Source;
            fades.Add(new AudioFade(fadeOutDuration, 0f, oldTrack, delegate
            {
                //currentTrack.Source.Pause();
                oldTrack.volume = 0;

                /*musicSource.clip = newTrack;
                musicSource.volume = 0f;
                musicSource.Play();*/
            }));
            currentTrack = track;
            fades.Add(new AudioFade(fadeInDuration, volume, currentTrack.Source, delegate
            {
                currentTrack.Source.volume = volume;
            }));
        }
    }

    public void Update()
    {
        for (int index = 0; index < fades.Count; index += 1)
        {
            AudioFade fade = fades[index];
            if (fade != null && fade.IsFading)
            {
                fade.Update();
            }
            if (!fade.IsFading)
            {
                fades.Remove(fade);
            }
        }
    }
}

[System.Serializable]
public class MusicTrack
{
    public AudioClip Clip;
    public MusicTrackType Type;
    [HideInInspector]
    public AudioSource Source;
}

public enum MusicTrackType
{
    Intro,
    Main,
    Water
}

public class AudioFade
{
    public AudioFade(float duration, float target, AudioSource track, UnityAction callback)
    {
        this.duration = duration;
        IsFading = true;
        timer = 0f;
        originalVolume = track.volume;
        targetVolume = target;
        audioSource = track;
        fadeComplete = callback;
    }
    public bool IsFading { get; private set; }
    private float duration;
    private float timer;
    private float targetVolume;
    private AudioSource audioSource;
    private float originalVolume;

    private UnityAction fadeComplete;

    public void Update()
    {
        timer += Time.unscaledDeltaTime / duration;
        audioSource.volume = Mathf.Lerp(originalVolume, targetVolume, timer);
        if (timer >= 1)
        {
            audioSource.volume = targetVolume;
            IsFading = false;
            fadeComplete.Invoke();
        }
    }
}