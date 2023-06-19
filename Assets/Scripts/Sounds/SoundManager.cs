using System;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    ButtonClick,
    BackgroundMusic,
    PlayerMove,
    PlayerDeath,
    PlayerJump,
    PlayerHurt,
    EnemyDeath, 
    EnemyMove,
    EnemyAttack,
    LevelDoor,
    KeyPickup
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    public AudioSource soundEffect, soundMusic;

    public SoundType[] sounds;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGMusic(Sounds.BackgroundMusic);
        InitializeSoundDictionary();
    }

    private Dictionary<Sounds, float> soundTimerDictionary;

    private void InitializeSoundDictionary()
    {
        soundTimerDictionary = new Dictionary<Sounds, float>();
        soundTimerDictionary[Sounds.PlayerMove] = 0;
        soundTimerDictionary[Sounds.PlayerJump] = 0;
        soundTimerDictionary[Sounds.EnemyMove] = 0;
    }

    public void PlayBGMusic(Sounds sound)
    {
        AudioClip clip = GetAudioClip(sound);
        if (clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Play();
        }
        else
        {
            Debug.LogError("Clip not found for sound type: " + sound);
        }
    }

    public void Play(Sounds sound)
    {
        AudioClip clip = GetAudioClip(sound);
        if (CanPlaySound(sound))
        {
            if (clip != null)
            {
                soundEffect.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("Clip not found for sound type: " + sound);
            }
        }
    }

    private bool CanPlaySound(Sounds sound)
    {
        switch (sound)
        {
            default:
                return true;

            case Sounds.PlayerMove:
            case Sounds.PlayerJump:
            case Sounds.EnemyMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float soundPlayTimerMax = .03f;
                    if (lastTimePlayed + soundPlayTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else { return false; }
                }
                else { return true; }
        }
    }

    private AudioClip GetAudioClip(Sounds sound)
    {
        SoundType item = Array.Find(sounds, i => i.soundType == sound);
        if (item != null)
        {
            return item.soundClip;
        }
        return null;
    }
}
