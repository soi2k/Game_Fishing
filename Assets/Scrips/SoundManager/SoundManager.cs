using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource _audiosource;
    protected void Awake()
    {
        _audiosource = GetComponent<AudioSource>();
    }

    public void PlayAudio(string soundName)
    {
        AudioClip audioClip = (AudioClip)Resources.Load("Sounds/" + soundName, typeof(AudioClip));

        _audiosource.clip = audioClip;
        _audiosource.Play();
    }

    public void StopSound()
    {
        _audiosource.Stop();
    }
}
