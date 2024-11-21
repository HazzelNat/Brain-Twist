using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChangeScene : MonoBehaviour
{

    public void ChangeScene(String sceneName){
        SceneManager.LoadScene(sceneName);
    }



}
