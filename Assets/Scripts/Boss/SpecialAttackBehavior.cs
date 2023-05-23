using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    override public async void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        numAttacks = 2;
        attackNum = Random.Range(0, 2);
        _plasticBoss = animator.GetComponent<BossController>();
        animator.SetInteger("attacksLeft", 2);
        // player = GameObject.FindGameObjectWithTag("PlayerPlaceholder").transform;
        if (animator.GetInteger("specialsLeft") > 0)
        {
            if (attackNum == 0)
            {
                var tasks = new Task[1];
                tasks[0] = _plasticBoss.LaneShots2(3, 1000f, 0.06f, false);
               
               
                Debug.Log("TORNADO ATTACK GO");
                Task.WhenAll(tasks);
                animator.SetBool("Tornado", true);
            }
            else if (attackNum == 1 && animator.GetBool("isEnraged"))
            {
                Debug.Log("da sucky?");
                _plasticBoss.startSuck();
                animator.SetBool("Suck", true);
            }
            else
            {

                var tasks = new Task[1];
                tasks[0] = _plasticBoss.LaneShots2(3, 1000f, 0.06f, false);

                animator.SetBool("Tornado", true);
                Debug.Log("TORNADO ATTACK GO");
                Task.WhenAll(tasks);
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
