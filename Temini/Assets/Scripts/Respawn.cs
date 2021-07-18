using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Huitzilopochtli");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Huitzilopochtli")
        {
            player.GetComponent<PlayerController>().playerAttributes.health -= 10;
            Debug.Log("Health: " + player.GetComponent<PlayerController>().playerAttributes.health);
            player.transform.position = GameObject.Find("Respawn Point").transform.position;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
