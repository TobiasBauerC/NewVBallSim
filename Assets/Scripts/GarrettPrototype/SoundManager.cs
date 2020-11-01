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
        DontDestroyOnLoad(gameObject);
    }


    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource sfx2Source;
    [SerializeField] private AudioSource sfx3Source;
    [SerializeField] private AudioSource announcerSource;

    [Header("SFX Clips")]
    public AudioClip[] volleyballContactSounds;
    public AudioClip[] spikeContactSounds;
    public AudioClip[] ballBounceSounds;
    
    [Header("Music Tracks")]
    [SerializeField] private AudioClip gameMusicTrack;

    [Header("Announcer Lines")]
    [SerializeField] private AudioClip[] announcerGameStartLines;


    public void PlayGameMusic()
    {
        musicSource.Stop();
        musicSource.clip = gameMusicTrack;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip[] clips)
    {
        AudioSource source = GetAvailableSFXSource();
        if (source == null)
            return;
        else
        {
            // random clip from the array
            int clipNumber = Random.Range(0, clips.Length + 1);
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
