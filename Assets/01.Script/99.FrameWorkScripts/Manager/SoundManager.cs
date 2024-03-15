using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource voiceSource;
    public float BGMVolme { get { return bgmSource.volume; } set { bgmSource.volume = value; } }
    public float SFXVolme { get { return sfxSource.volume; } set { sfxSource.volume = value; } }
    public float VoiceVolme { get { return voiceSource.volume; } set {  voiceSource.volume = value; } }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying == false)
            return;

        bgmSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlayVoice(AudioClip clip)
    {
        voiceSource.Stop();
        voiceSource.clip = clip;
        voiceSource.Play();
    }
    public void StopSFX()
    {
        if (sfxSource.isPlaying == false)
            return;

        sfxSource.Stop();
    }
}
