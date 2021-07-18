using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;
    private bool isGrounded;
    private Rigidbody2D rb;
    private InputMaster inputMaster;
    private Animator animator;
    private BoxCollider2D collide;
    
    [HideInInspector] public PlayerAttributes playerAttributes;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpingPower = 10f;
    [SerializeField] private PauseMenuHandler pauseMenuHandler;

    private bool isMoving = false;
    public bool isAttacking = false;
    private float distToGround;

    public Transform attackPoint;
    public float attackRange = 3f;
    public LayerMask enemyLayers;
    public Slider healthBar;

    // Set up input and instances
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAttributes = new PlayerAttributes();
        inputMaster = new InputMaster();
        animator = GetComponent<Animator>();
        collide = GetComponent<BoxCollider2D>();
        distToGround = collide.bounds.extents.y;
        if (SceneManager.GetActiveScene().name == "Room1")
        {
            playerAttributes.health = 100.0f;
            Carryover.playerHealth = 100.0f;
        }
        else
        {
            playerAttributes.health = Carryover.playerHealth;
        }
        isGrounded = false;

        inputMaster.Player.Pause.performed += context => pauseKeyPressed(); 
        inputMaster.Player.Jump.performed += context => jump();
        inputMaster.Player.Attack.performed += context => attack();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    private void Update()
    {
        Carryover.playerHealth = playerAttributes.health;
        healthBar.value = playerAttributes.health;
        
        if (playerAttributes.health <= 0.0f)
            die();
    }

    private void FixedUpdate()
    {
        // Take input from the controls and apply it to the rigidbody
        Vector2 moveInput = inputMaster.Player.Movement.ReadValue<Vector2>();
        if (isAttacking == false)
        {

            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
            isMoving = rb.velocity != Vector2.zero;
            animator.SetBool("isMoving", isMoving);
            animator.SetFloat("Direction", moveInput.x * moveSpeed);
        }
        else if(isAttacking == true && isGrounded == true)
        {
            rb.velocity = Vector2.zero;
            
        }
        if (moveInput.x * moveSpeed < -0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            
        }
        else if (moveInput.x * moveSpeed > 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void attack()
    {
        animator.SetTrigger("Attack");
        animator.SetBool("isAttacking", true);
        isAttacking = true;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.gameObject.GetComponent<Enemy>().health -= 40.0f;
        }
    }

    private void pauseKeyPressed()
    {
        switch (Time.timeScale)
        {
            // If game is paused
            case 0.0f:
                pauseMenuHandler.unpause();
                break;
            // If game is unpaused
            case 1f:
                pauseMenuHandler.pause();
                break;
        }
    }
    
    private void jump()
    {
        // If the player is grounded then jump by the specified jumpforce
        if (isGrounded == true)
        {

            rb.AddForce(new Vector2(rb.velocity.x, jumpingPower), ForceMode2D.Impulse);
            isGrounded = false;

        }
        animator.SetBool("isJumping", true);
        animator.SetTrigger("Takeoff");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tilemap" && isGrounded == false)
        {
            isGrounded = true;
        }
        animator.SetBool("isJumping", false);
        animator.ResetTrigger("Takeoff");
    }

    private void die()
    {
        Destroy(gameObject);
        // Load the game over scene
        SceneManager.LoadScene("GameOver");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
