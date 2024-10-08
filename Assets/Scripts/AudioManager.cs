using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] SFXSounds, MusicSounds;

    private AudioSource _lastMusicSource;

    [field: SerializeField, Range(0, 1)]
    public float SFXVolumeValue { get; private set; } = 1;

    [field: SerializeField, Range(0, 1)]
    public float MusicVolumeValue { get; private set; } = 1;

    
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
    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(SFXSounds, sound => sound.Name == name);
        if (sound == null)
        {
            Debug.Log("Sound Effect " + name + " not found!");
            return;
        }

        ConfigureAudioSource(sound, isSFX: true);
    }


     public void PlayMusic(string name)
    {
        Sound sound = Array.Find(MusicSounds, sound => sound.Name == name);
        if (sound == null)
        {
            Debug.Log("Music " + name + " not found!");
            return;
        }

        ConfigureAudioSource(sound, isSFX: false);
        sound.source = _lastMusicSource;
    }

    private void ConfigureAudioSource(Sound sound, bool isSFX = true)
    {
        sound.source = gameObject.AddComponent<AudioSource>();
        sound.source.clip = sound.Clip;
        sound.source.volume = isSFX ? SFXVolumeValue : MusicVolumeValue;
        sound.source.pitch = sound.Pitch;
        sound.source.loop = sound.Loop;
        sound.source.Play();
    }

    public void ToggleMusic()
    {
        _lastMusicSource.mute = !_lastMusicSource.mute;
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
        _lastMusicSource.volume = volume;
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
