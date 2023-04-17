using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormAttackBehavior : StateMachineBehaviour
{
    public int numAttacks; 
    public bool isEnraged;
    public float attackNum;
    public int attacksLeft;
    public bool setTrajectory;
    public bool homingShot;
    public bool specialTransition;
    private BossHealth h;
    private BossController _plasticBoss;
    public int NumPlasticSpawn;
    //private BossPlaceholder healthy;

    Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        numAttacks = 3;
        attackNum = Random.Range(0, 4);
       // player = GameObject.FindGameObjectWithTag("PlayerPlaceholder").transform;
        setTrajectory = false;
        animator.SetInteger("specialsLeft", 1);
        homingShot = false;

        if (animator.GetInteger("attacksLeft") > 0)
        {
            if (attackNum == 0)
            {
                animator.SetBool("setTraj", true);
            }
            else if (attackNum == 1)
            {
                animator.SetBool("Turtle", true);
            }
            else if (attackNum == 2)
            {
                animator.SetBool("Urchin", true);
            }
            else if (attackNum == 3)
            {
                animator.SetBool("HomingShot", true);
            }
            else
            {
                animator.SetBool("SlowHoming", true);
            }
        }
        else
        {
            animator.SetBool("specialAttack", true);
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* 
            here the boss will always be checking if it's health is half health, if it falls below half health it will set enrage to be true 
        
       if(h.GetHealth() <= 50)
        {
            animator.SetBool("isEnraged", true);
        }
        */
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("attacksLeft", animator.GetInteger("attacksLeft") - 1);
        Debug.Log("exiting ");
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
