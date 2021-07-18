using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyShot : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D rb;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Huitzilopochtli").transform;
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = (player.position - transform.position).normalized * speed;
    }

}
