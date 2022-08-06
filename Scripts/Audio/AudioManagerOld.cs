using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManagerOld : MonoBehaviour
{
    //[HideInInspector] 
 
    public SoundOld[] sounds;

    void Awake()
    {
        foreach (SoundOld s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        SoundOld s = Array.Find(sounds, sound => sound.name == name);

        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
    }

    public void Pause(string name)
    {
        SoundOld s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }
}
//FindObjectOfType<AudioManager>().Play("nome do son"); quando morrer
//void Start (){Play("Theme")}