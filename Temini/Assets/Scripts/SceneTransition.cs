using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string level;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Huitzilopochtli")
            SceneManager.LoadScene(level);
    }
}
