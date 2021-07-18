using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : StateMachineBehaviour
{
    Transform player;
    CoyolController cc;
    int firedShots;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Huitzilopochtli").transform;
        cc = animator.GetComponent<CoyolController>();
        firedShots = 0;

        Vector3 diff = player.position - cc.transform.position;
        float rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        if (animator.GetFloat("Direction") > 0.01f)
        {
            rotationZ -= 90.0f;
        }
        else if (animator.GetFloat("Direction") < -0.01f)
        {
            rotationZ += 90.0f;
        }


        Instantiate(cc.shot, cc.firePoint.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
        animator.SetBool("isShooting", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*
        if(firedShots < cc.volleyNum)
        {
            
            firedShots++;
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
        Debug.Log(firedShots);
        */

        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Finished Shooting");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
