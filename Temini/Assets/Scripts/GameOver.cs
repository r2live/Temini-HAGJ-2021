using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kills;
    [SerializeField] private TextMeshProUGUI percentage;

    private float enemyCount = 9f;
    private float coyolWorth = 5f;

    private void Awake()
    {
        kills.text = "Enemies killed: " + Carryover.enemiesKilled;
        percentage.text = "Percent done: " + ((float)Carryover.enemiesKilled / (enemyCount + coyolWorth)) * 100 + "%";
    }
}
