using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundCheckDistance = 2f;
    [SerializeField] private Transform groundDetection;
    [SerializeField] private Transform attackHitbox;

    private bool movingRight = true;
    private bool attackStarted = false;
    private bool isMoving;
    private AIState state = AIState.Patrolling;
    private GameObject player;
    private Animator animator;
    private Rigidbody2D rb;

    public float health = 100.0f;

    private void Awake()
    {
        // Get a reference to the player
        player = GameObject.Find("Huitzilopochtli");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        StartCoroutine(checkIfMoving());
        animator.SetBool("isMoving", isMoving);
        if (health <= 0.0f)
            die();
        
        // Check if the player is in range
        state = Physics2D.OverlapCircle(transform.position, 8f).gameObject.tag == "Player" ? AIState.Attacking : AIState.Patrolling;
        
        switch (state)
        {
            // If the enemy is in patrol mode
            case AIState.Patrolling:
            {
                if (attackStarted)
                {
                    attackStarted = false;
                    CancelInvoke("attack");
                }
                
                // Move the enemy to the right
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            
                // Fire a ray from the ground check directly down by the chosen distance
                RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundCheckDistance);
                Debug.DrawRay(groundDetection.position, Vector3.down);
                if (groundInfo.collider == false) // If the ground check ray did not detect any more ground
                {
                    if (movingRight) // If the enemy was moving right then flip to move left
                    {
                        transform.eulerAngles = new Vector3(0, -180, 0);
                        movingRight = false;
                    }
                    else // If the enemy was moving left then flip to move right
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        movingRight = true;
                    }
                }

                break;
            }
            // If the enemy is in attack mode
            // The player is to the right of the enemy
            case AIState.Attacking when player.transform.position.x > transform.position.x:
            {
                // If the enemy is far enough away from the player, then chase the player
                if (Vector2.Distance(player.transform.position, transform.position) >= 4.0f)
                {
                    movingRight = true;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                }
                else // If the enemy is in attack range, attack right
                {
                    if (Physics2D.OverlapCircle(attackHitbox.position, 3f).gameObject.tag == "Player")
                    {
                        // Deal damage every 1 seconds that the player is in damage range
                        if (!attackStarted)
                        {
                            attackStarted = true;
                            InvokeRepeating("attack", 0.1f, 1f);
                        }
                    }
                    else
                    {
                        if (attackStarted)
                        {
                            attackStarted = false;
                            CancelInvoke("attack");
                        }
                    }
                }

                break;
            }
            // The player is to the left of the enemy
            // If the enemy is far enough away from the player, then chase the player
            case AIState.Attacking when Vector2.Distance(player.transform.position, transform.position) >= 4.0f:
            {
                movingRight = false;
                transform.eulerAngles = new Vector3(0, -180, 0);
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                break;
            }
            // If the enemy is in attack range, attack left
            case AIState.Attacking:
            {
                if (Physics2D.OverlapCircle(attackHitbox.position, 3f).gameObject.tag == "Player")
                {
                    // Deal damage every 1 seconds that the player is in damage range
                    if (!attackStarted)
                    {
                        attackStarted = true;
                        InvokeRepeating("attack", 0.1f, 1f);
                    }
                }
                else
                {
                    if (attackStarted)
                    {
                        attackStarted = false;
                        CancelInvoke("attack");
                    }
                }
                
                break;
            }
        }
    }

    private IEnumerator checkIfMoving()
    {
        Vector2 lastPosition = transform.position;
        yield return new WaitForSecondsRealtime(0.1f);
        Vector2 currentPosition = transform.position;
        isMoving = lastPosition != currentPosition;
    }
    
    private void attack()
    {
        animator.SetTrigger("Attack");
        animator.SetBool("isAttacking", true);
        player.GetComponent<PlayerController>().playerAttributes.health -= 10.0f;
    }

    private void die()
    {
        Carryover.enemiesKilled++;
        Destroy(gameObject);
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Star_Brother/Enemy_Die", gameObject);
    }
}
