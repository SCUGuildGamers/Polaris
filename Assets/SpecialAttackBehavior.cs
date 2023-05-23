using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackBehavior : StateMachineBehaviour
{
    public int numAttacks;

    public Plastic _plastic;
    public Plastic _lanePlastic;
    public TurtleController Turtle;
    public UrchinController Urchin;
    public platform platform;

    public float attackNum;
    public int attacksLeft;
    private BossController _plasticBoss;
    public bool specialTransition;
    Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        numAttacks = 2;
        attackNum = Random.Range(0, 2);
        // player = GameObject.FindGameObjectWithTag("PlayerPlaceholder").transform;
        if (animator.GetInteger("specialsLeft") > 0)
        {
            if (attackNum == 0)
            {
                animator.SetBool("Tornado", true);
                
                Debug.Log("TORNADO ATTACK GO");
            }
            else if (attackNum == 1 && animator.GetBool("isEnraged"))
            {
                Debug.Log("da sucky?");
                animator.SetBool("Suck", true);
            }
            else
            {
                animator.SetBool("Tornado", true);
                Debug.Log("TORNADO ATTACK GO");
            }
        }
        else
        {
            animator.SetBool("specialAttack", false);
        }


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("specialsLeft", animator.GetInteger("specialsLeft") - 1);
        Debug.Log("exiting special");
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
