using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSweepAttack : StateMachineBehaviour
{
    private BossController _plasticBoss;

    public int NumProjectiles;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _plasticBoss = animator.GetComponent<BossController>();
        _plasticBoss.SweepRightLayered(NumProjectiles);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
