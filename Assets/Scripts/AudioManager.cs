using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] SFXSounds, MusicSounds;
    public AudioSource MusicSource;
    
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

    [field: SerializeField, Range(0,1)]
    public float SFXVolumeValue { get; private set; }
    
    [field: SerializeField, Range(0,1)]
    public float MusicVolumeValue { get; private set; }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(SFXSounds, sound => sound.Name == name);
        if (sound == null)
        {
            Debug.Log("Sound Effect " + name + " not found!");
            return;
        }

        ConfigureSFXSource(sound);
    }


     public void PlayMusic(string name)
    {
        Sound sound = Array.Find(MusicSounds, sound => sound.Name == name);
        if (sound == null)
        {
            Debug.Log("Music " + name + " not found!");
            return;
        }

        if (MusicSource.isPlaying)
        {
            MusicSource.Stop();
        }

        ConfigureMusicSource(MusicSource, sound);
    }

    
    private void ConfigureMusicSource(AudioSource audioSource, Sound sound)
    {
        sound.source = audioSource;
        sound.source.clip = sound.Clip;
        sound.source.volume = MusicVolumeValue;
        sound.source.pitch = sound.Pitch;
        sound.source.loop = sound.Loop;
        sound.source.Play();
    }

    private void ConfigureSFXSource(Sound sound)
    {
        sound.source = gameObject.AddComponent<AudioSource>();
        sound.source.clip = sound.Clip;
        sound.source.volume = SFXVolumeValue;
        sound.source.pitch = sound.Pitch;
        sound.source.loop = sound.Loop;
        sound.source.Play();
    }

    public void ToggleMusic()
    {
        MusicSource.mute = !MusicSource.mute;
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
        MusicSource.volume = volume;
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
