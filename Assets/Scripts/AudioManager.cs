using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    
    [Header("--------------------------------------------------------------------------------------")]

    public AudioClip BGM;
    public AudioClip correct;
    public AudioClip wrong;
    public AudioClip button;
    

    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }
}
