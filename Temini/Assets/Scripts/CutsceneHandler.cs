using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneHandler : MonoBehaviour
{
    private VideoPlayer player;
    private float time = 3.0f;
    private InputMaster inputMaster;

    [SerializeField] private string sceneToLoad;
    
    private void Awake()
    {
        inputMaster = new InputMaster();

        inputMaster.Player.SkipCutscene.performed += context => skip();
        
        player = GetComponent<VideoPlayer>();
        player.Play();
    }

    private void Update()
    {
        if (time >= 0)
        {
            time -= Time.deltaTime;
            return;
        }
        else
        {
            if (!player.isPlaying)
                finishedPlaying();
        }
    }

    private void finishedPlaying()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void skip()
    {
        Debug.Log("skipping");
    }
}
