using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class HighScoreManager : MonoBehaviour
{
    public int score;
    public int highScore;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    
}
