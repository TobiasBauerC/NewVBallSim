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
    [SerializeField] private AudioSource announcerSource = null;

    [Header("SFX Clips")]
    public AudioClip[] volleyballPassSounds;
    public AudioClip[] volleyballSpikeSounds;
    public AudioClip[] volleyballSetSounds;
    public AudioClip[] volleyballBounceSounds;
    public AudioClip[] volleyballToolSounds;
    
    [Header("Music Tracks")]
    [SerializeField] private AudioClip gameMusicTrack = null;
    [SerializeField] private AudioClip mainMenuMusicTrack = null;

    [Header("Announcer Clips")]
    public AudioClip[] announcerGameStart; // interrupt
    public AudioClip[] announcerBlueTeamServe;
    public AudioClip[] announcerRedTeamServe;
    public AudioClip[] announcerPlayerReception;
    public AudioClip[] announcerServeHappens; // interrupt
    public AudioClip[] announcer1Pass;
    public AudioClip[] announcer2Pass;
    public AudioClip[] announcer3Pass;
    public AudioClip[] announcerAce;
    public AudioClip[] announcerMissServe;
    public AudioClip[] announcerServeInNet;
    public AudioClip[] announcerPlayerToOffence;
    public AudioClip[] announcerPlayerSetChoice;
    public AudioClip[] announcerSetHappens; // interrupt
    public AudioClip[] announcerPlayerAttackChoice;
    public AudioClip[] announcerTool;
    public AudioClip[] announcerKill;
    public AudioClip[] announcerHitOut;
    public AudioClip[] announcerBlock;
    public AudioClip[] announcerHitIntoNet;
    public AudioClip[] announcer1Dig; // interrupt
    public AudioClip[] announcer2Dig; // interrupt
    public AudioClip[] announcer3Dig; // interrupt
    public AudioClip[] announcerRedWinsPoint;
    public AudioClip[] announcerBlueWinsPoint;
    public AudioClip[] announcerPlayerDefenseSetUp;
    public AudioClip[] announcerBlockersReact;
    public AudioClip[] announcerPlayerServeChoice;
    public AudioClip[] announcerWelcomeToGame;
    public AudioClip[] announcerDifficultyChoice;

    [Header("Announcer Numbers Clips")]
    public AudioClip[] announcer0;
    public AudioClip[] announcer1;
    public AudioClip[] announcer2;
    public AudioClip[] announcer3;
    public AudioClip[] announcer4;
    public AudioClip[] announcer5;
    public AudioClip[] announcer6;
    public AudioClip[] announcer7;
    public AudioClip[] announcer8;
    public AudioClip[] announcer9;
    public AudioClip[] announcer10;
    public AudioClip[] announcer11;
    public AudioClip[] announcer12;
    public AudioClip[] announcer13;
    public AudioClip[] announcer14;
    public AudioClip[] announcer15;
    public AudioClip[] announcer16;
    public AudioClip[] announcer17;
    public AudioClip[] announcer18;
    public AudioClip[] announcer19;
    public AudioClip[] announcer20;
    public AudioClip[] announcer21;
    public AudioClip[] announcer22;
    public AudioClip[] announcer23;
    public AudioClip[] announcer24;
    public AudioClip[] announcer25;
    public AudioClip[] announcerScoreIsClose;

    private List<AudioClip[]> announcerQueue = new List<AudioClip[]>();
    private bool announcerCoroutineRunning = false;


    //[Header("Announcer Lines")]
    //[SerializeField] private AudioClip[] announcerGameStartLines = null;

    private void Start()
    {
        PlayAnnouncerLineInterrupt(announcerWelcomeToGame);
    }


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

    public void PlayAnnouncerLineInterrupt(AudioClip[] clips)
    {
        announcerSource.Stop();
        announcerQueue.Clear();
        int clipNumber = Random.Range(0, clips.Length);
        announcerSource.clip = clips[clipNumber];
        announcerSource.Play();
    }

    public void PlayAnnouncerLineQueue(AudioClip[] clips)
    {
        // Debug.Log("calling the announcer queue line function");
        if (announcerSource.isPlaying)
        {
            Debug.Log("announcer queue is already playing");
            announcerQueue.Add(clips);

            if (announcerCoroutineRunning)
            {
                // if the coroutine is already running, do nothing
                // Debug.Log("Coroutine is already running, NOT starting it again");
            }
            else
            {
                // if the coroutine is not running, start it
                // Debug.Log("Coroutine is not running, going to start it");
                StartCoroutine(PlayAnnouncerQueue());
            }
        }
        else
        {
            // Debug.Log("Announcer Queue is not playing");
            int clipNumber = Random.Range(0, clips.Length);
            announcerSource.clip = clips[clipNumber];
            announcerSource.Play();
        }
    }

    private IEnumerator PlayAnnouncerQueue()
    {
        announcerCoroutineRunning = true;
        // Debug.Log("Coroutine Starting");
        while (announcerQueue.Count > 0)
        {
            // Debug.Log("Stuff still in the queue list");
            if (!announcerSource.isPlaying)
            {
                // Debug.Log("announcer source no longer playing, so trying to do stuff");
                int clipNumber = Random.Range(0, announcerQueue[0].Length);
                announcerSource.clip = announcerQueue[0][clipNumber];
                announcerQueue.RemoveAt(0);
                announcerSource.Play();
            }
            else yield return null;
        }

        announcerCoroutineRunning = false;
        yield break;

    }

    public void PlayAnnouncerLineBasedOnResult(int resultNumber)
    {
        switch (resultNumber)
        {
            case -1: // tool
                PlayAnnouncerLineQueue(announcerTool);
                break;
            case 0: // kill
                PlayAnnouncerLineQueue(announcerKill);
                break;
            case 1: // dig 1
                PlayAnnouncerLineInterrupt(announcer1Dig);
                break;
            case 2: // dig 2
                PlayAnnouncerLineInterrupt(announcer2Dig);
                break;
            case 3: // dig 3
                PlayAnnouncerLineInterrupt(announcer3Dig);
                break;
            case 100: // block
                PlayAnnouncerLineQueue(announcerBlock);
                break;
            case -2: // hit out of bounds
                PlayAnnouncerLineQueue(announcerHitOut);
                break;
            case -3: // hit in the net
                PlayAnnouncerLineQueue(announcerHitIntoNet);
                break;
        }
    }

    public void PlayAnnouncerLineQueueBaseOnPassNumber(int passNumber)
    {
        switch (passNumber)
        {
            case 1: // dig 1
                PlayAnnouncerLineQueue(announcer1Pass);
                break;
            case 2: // dig 2
                PlayAnnouncerLineQueue(announcer2Pass);
                break;
            case 3: // dig 3
                PlayAnnouncerLineQueue(announcer3Pass);
                break;
        }
    }



}
