using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start() {
        audioManager.PlaySFX(audioManager.BGM);
    }
}
