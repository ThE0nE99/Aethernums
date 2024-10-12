using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("AudioSource")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXsource;
    [Header("Audio Clips-----")]
    public AudioClip backgroundMenu;
    public AudioClip backgroundCredit;
    public AudioClip backgroundgame;

    public AudioClip jump;
    public AudioClip run;
    public AudioClip buton;
    private void Start()
    {
        musicSource.clip = backgroundMenu;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXsource.PlayOneShot(clip);
    }

    public void OpcionesMu()
    {
        musicSource.Stop();
        musicSource.clip = backgroundCredit;
        musicSource.Play();

    }

    public void OpcionesMuvolver()
    {
        musicSource.Stop();
        musicSource.clip = backgroundMenu;
        musicSource.Play();

    }
    public void botono()
    {

        SFXsource.PlayOneShot(buton);
        

    }
}
