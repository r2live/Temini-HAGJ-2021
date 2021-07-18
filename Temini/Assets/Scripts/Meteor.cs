using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 7f;
    public Rigidbody2D rb;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Huitzilopochtli").transform;
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down.normalized * speed;
    }

 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        /*
        if (collision.gameObject.name == "Coyolxauhqui")
        {
            Physics2D.IgnoreCollision(GameObject.Find("Coyolxauhqui").GetComponent<BoxCollider2D>(), gameObject.GetComponent<PolygonCollider2D>());
        }
        */
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Huitzilopochtli")
        {
            GameObject.Find("Huitzilopochtli").GetComponent<PlayerController>().playerAttributes.health -= 5.0f;
        }
    }
}
