using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    private void Awake() {

        if(instance != null){
            Destroy(gameObject);
        }
        instance = this;

        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }
    public void Play(string name){
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if(sound == null)
            return;
        sound.source.Play();
    }

}
