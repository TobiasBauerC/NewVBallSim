using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }


    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource = null;
    [SerializeField] private AudioSource sfxSource = null;
    [SerializeField] private AudioSource sfx2Source = null;
    [SerializeField] private AudioSource sfx3Source = null;
    // [SerializeField] private AudioSource announcerSource = null;

    [Header("SFX Clips")]
    public AudioClip[] volleyballPassSounds;
    public AudioClip[] volleyballSpikeSounds;
    public AudioClip[] volleyballSetSounds;
    public AudioClip[] volleyballBounceSounds;
    public AudioClip[] volleyballToolSounds;
    
    [Header("Music Tracks")]
    [SerializeField] private AudioClip gameMusicTrack = null;
    [SerializeField] private AudioClip mainMenuMusicTrack = null;

    //[Header("Announcer Lines")]
    //[SerializeField] private AudioClip[] announcerGameStartLines = null;


    public void PlayGameMusic()
    {
        musicSource.Stop();
        musicSource.clip = gameMusicTrack;
        musicSource.Play();
    }

    public void PlayMainMenuMusic()
    {
        musicSource.Stop();
        musicSource.clip = mainMenuMusicTrack;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip[] clips)
    {
        AudioSource source = GetAvailableSFXSource();
        if (source == null)
        {
            Debug.LogError("Tried to play a sound when all sfx channels were full");
            return;
        }
        else
        {
            // random clip from the array
            int clipNumber = Random.Range(0, clips.Length);
            source.clip = clips[clipNumber];
            source.Play();
        }
    }

    private AudioSource GetAvailableSFXSource()
    {
        if (!sfxSource.isPlaying)
            return sfxSource;
        else if (!sfx2Source.isPlaying)
            return sfx2Source;
        else if (!sfx3Source.isPlaying)
            return sfx3Source;
        else return null;
    }



}
