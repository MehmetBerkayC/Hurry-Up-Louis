using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;

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

        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.Clip;
            sound.source.volume = sound.Volume;
            sound.source.pitch = sound.Pitch;
            sound.source.loop = sound.Loop;
        }
    }

    public static AudioClip PickUp;
    
    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.Name == name);
        if (sound == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        sound.source.Play();
    } 

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.Name == name);
        if (sound == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        sound.source.Stop();
    }

}
