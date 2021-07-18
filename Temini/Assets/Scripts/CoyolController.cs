using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoyolController : MonoBehaviour
{
    public Transform player;

    public float speed;


    public Animator animator;

    float retreatTimer;
    float cooldown;

    Transform point1;
    Transform point2;

    public bool isMoving;
    public bool isAttacking;
    public bool isLowHealth;

    public Transform firePoint;

    string[] pattern;
    string[] lowHealthPattern;
    int patternCount;
    public int currentPoint;

    public GameObject shot;
    public GameObject meteor;

    public int health = 800;

    public int volleyNum = 4;
    int firedShots = 0;

    public float attackRange = 2.0f;

    public Transform attackPoint;

    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Huitzilopochtli").transform;
        point1 = GameObject.Find("Point 1").transform;
        point2 = GameObject.Find("Point 2").transform;

        firePoint = GameObject.Find("Shoot Spawn Point").transform;
        attackPoint = GameObject.Find("Sword Point").transform;

        speed = 10f;
        isLowHealth = false;
        patternCount = 0;
        pattern = new string[] { "Meteor", "Move", "Meteor", "Shoot", "Shoot", "Shoot", "Shoot", "Shoot", "Shoot", "Shoot", "Shoot", "Move", "Meteor", "Shoot", "Shoot", "Shoot", "Shoot", "Move", "Meteor", "Shoot", "Shoot", "Shoot", "Shoot", "Meteor", "Move" };
        lowHealthPattern = new string[] { "Sword", "Sword", "Move", "Meteor", "Shoot", "Move", "Sword", "Meteor", "Shoot", "Move" };
        currentPoint = 2;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Mathf.Abs(transform.position.x - player.position.x) < retreatDistance)
        {
            //DelayBeforeRetreating();
        }
        */

        

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 400 && isLowHealth == false)
        {
            isLowHealth = true;
        }

        if(health <= 0)
        {
            Die();
            SceneManager.LoadScene("PostBossFight");
        }

    }

    void Die()
    {
        animator.SetTrigger("Dead");
        
    }

    public IEnumerator ChooseAction()
    {
        string actionName;

        if (isLowHealth == false)
        {
            actionName = pattern[patternCount];
        }
        else
        {
            actionName = lowHealthPattern[patternCount];
            patternCount = 0;
        }

        Debug.Log(actionName);

        patternCount++;
        if (patternCount > 21 && isLowHealth == false)
            patternCount = 0;
        else if (patternCount > 15 && isLowHealth == true)
            patternCount = 0;

        if (actionName != "Shoot" || firedShots == 4)
        {
            yield return new WaitForSecondsRealtime(2f);
            firedShots = 0;
        }
        else
        {
            yield return new WaitForSecondsRealtime(0.25f);
            firedShots++;
        }

        ExecuteAction(actionName);

    }

    private void ExecuteAction(string actionName)
    {
        if(actionName == "Shoot")
        {
            Shoot();
        }
        else if(actionName == "Meteor")
        {
            Meteor();
        }
        else if(actionName == "Sword")
        {
            Sword();
        }
        else
        {
            MoveToPoint();
        }

    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
        animator.SetBool("isShooting", true);
        //Instantiate(shot, firePoint.position, firePoint.rotation);

        Vector3 playerDirection = transform.InverseTransformPoint(player.transform.position);
        if(playerDirection.x < 0)
        {
            animator.SetFloat("Direction", -0.02f);
        }
        else
        {
            animator.SetFloat("Direction", 0.02f);
        }
    }

    private void Meteor()
    {
        animator.SetTrigger("Meteor");
        animator.SetBool("isMeteoring", true);
        //Instantiate(shot, firePoint.position, firePoint.rotation);

        Vector3 playerDirection = transform.InverseTransformPoint(player.transform.position);
        if (playerDirection.x < 0)
        {
            animator.SetFloat("Direction", -0.02f);
        }
        else
        {
            animator.SetFloat("Direction", 0.02f);
        }
    }

    private void Sword()
    {
        animator.SetTrigger("Sword");
        animator.SetBool("isSwording", true);

        Vector3 playerDirection = transform.InverseTransformPoint(player.transform.position);
        if (playerDirection.x < 0)
        {
            animator.SetFloat("Direction", -0.02f);
        }
        else
        {
            animator.SetFloat("Direction", 0.02f);
        }
    }

    private void MoveToPoint()
    {
        animator.SetBool("isMoving", true);
        if (currentPoint == 1)
        {
            animator.SetFloat("Direction", -0.02f);
        }
        else
        {
            animator.SetFloat("Direction", 0.02f);
        }
        animator.GetFloat("Direction");
    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
    