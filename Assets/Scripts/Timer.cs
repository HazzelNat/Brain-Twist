using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public TMP_Text timeText;
    public Image slider;
    public float timeLimit = 60f;

    public float time;
    bool startTimer;

    float multiplierFactor;

    public event Action OnTimerEnded;

    void Start() {
        time = timeLimit;
        StartTimer();
    }

    public void StartTimer() {

        multiplierFactor = 1f / timeLimit;

        timeText.text = time.ToString();
        startTimer = true;

        slider.fillAmount = time * multiplierFactor;

    }

    public void ResetTimer()
    {
        time = timeLimit;
        timeText.text = Mathf.CeilToInt(time).ToString(); 
        slider.fillAmount = 1f; 
        startTimer = true; 
    }

    public void StopTimer() {
        startTimer = false;
    }
    
    void Update()
    {
        if (!startTimer) return;

        if (time > 0) {
            time -= Time.deltaTime;
            timeText.text = Mathf.CeilToInt(time).ToString();
            slider.fillAmount = time * multiplierFactor;

        } else {
            time = 0;
            startTimer = false;
            OnTimerEnded?.Invoke();
        }


    }



}
