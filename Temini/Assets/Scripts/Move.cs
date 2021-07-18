using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : StateMachineBehaviour
{

    Transform player;
    CoyolController cc;
    Transform point1;
    Transform point2;
    Transform chosenPoint;
    Rigidbody2D rb;
    GameObject coyol;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.Find("Huitzilopochtli").transform;
        cc = animator.GetComponent<CoyolController>();
        point1 = GameObject.Find("Point 1").transform;
        point2 = GameObject.Find("Point 2").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        if(cc.currentPoint == 1)
        {
            cc.currentPoint = 2;
            chosenPoint = point2;
        }
        else
        {
            cc.currentPoint = 1;
            chosenPoint = point1;
        }

        
        coyol = GameObject.Find("Coyolxauhqui");
        Debug.Log(chosenPoint.position);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (coyol.transform.position != chosenPoint.position)
        {
            coyol.transform.position = Vector3.MoveTowards(coyol.transform.position, chosenPoint.position, cc.speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cc.isMoving = false;
        Debug.Log("Exit");
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
