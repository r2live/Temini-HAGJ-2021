using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float health = 10f;

    private PlayerController player;
    
    private void Awake()
    {
        player = GameObject.Find("Huitzilopochtli").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player") return;
        player.playerAttributes.health += health;
        Destroy(gameObject);
    }
}
