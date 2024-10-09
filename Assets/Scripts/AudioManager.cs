using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [field: SerializeField, Range(0, 1), Header("Audio Settings")]
    public float SFXVolumeValue { get; private set; } = 1;

    [field: SerializeField, Range(0, 1)]
    public float MusicVolumeValue { get; private set; } = 1;
    public bool IsSoundMuted { get; private set; } = false;

    [SerializeField]
    private AudioSource MusicSource, SFXSource;

    [SerializeField, Header("Audio Clips")]
    private Sound[] SFXSounds, MusicSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        AudioSource[] sources = GetComponentsInChildren<AudioSource>();
        if(sources.Length > 0) { 
            MusicSource = sources[0];
            SFXSource = sources[1];
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(SFXSounds, sound => sound.Name == name);
        if (sound == null)
        {
            Debug.Log("Sound Effect " + name + " not found!");
            return;
        }

        ConfigureAudioSource(SFXSource, sound, isSFX: true);
    }


     public void PlayMusic(string name)
    {
        Sound sound = Array.Find(MusicSounds, sound => sound.Name == name);
        if (sound == null)
        {
            Debug.Log("Music " + name + " not found!");
            return;
        }

        ConfigureAudioSource(MusicSource, sound, isSFX: false);
       
    }

    private void ConfigureAudioSource(AudioSource audioSource, Sound sound, bool isSFX = true)
    {
        sound.source = audioSource;
        sound.source.clip = sound.Clip;
        sound.source.volume = isSFX ? SFXVolumeValue : MusicVolumeValue;
        sound.source.pitch = sound.Pitch;
        sound.source.loop = sound.Loop;
        sound.source.mute = IsSoundMuted;
        audioSource.Play();
    }

    public void ToggleMusic()
    {
        IsSoundMuted = !IsSoundMuted;
    }

    // Multiple Sources
    public void ToggleSFX()
    {
        foreach (Sound sound in SFXSounds)
        {
            sound.source.mute = !sound.source.mute;
        }
    }

    public void MusicVolume(float volume)
    {
        MusicVolumeValue = volume;
    }
    
    // Multiple Sources
    public void SFXVolume(float volume)
    {
        SFXVolumeValue = volume;
        foreach(Sound sound in SFXSounds)
        {
            sound.source.volume = volume;
        }
    }

    public void StopSFX(string name)
    {
        Sound sound = Array.Find(SFXSounds, sound => sound.Name == name);
        if (sound == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        sound.source.Stop();
    }

    public void StopMusic(string name)
    {
        Sound sound = Array.Find(MusicSounds, sound => sound.Name == name);
        if (sound == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        sound.source.Stop();
    }

}
