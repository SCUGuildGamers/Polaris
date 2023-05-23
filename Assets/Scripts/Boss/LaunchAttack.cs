using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LaunchAttack : StateMachineBehaviour
{
    private BossController _plasticBoss;

    public Plastic _plastic;
    public Plastic _lanePlastic;
    public TurtleController Turtle;
    public UrchinController Urchin;
    public platform platform;


    public float MinMovementSpeed;
    public float MaxMovementSpeed;
    public int NumPlasticSpawn;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public async void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _plasticBoss = animator.GetComponent<BossController>();
        int attackNum = Random.Range(0, 4);
        
        int temp = attackNum;
        int i = 1;
        // player = GameObject.FindGameObjectWithTag("PlayerPlaceholder").transform;

        if (animator.GetBool("isEnraged"))
        {
            animator.SetInteger("specialsLeft", 3);
            i = 2;
        }
        else
        {
            animator.SetInteger("specialsLeft", 1);
        }
        if (animator.GetInteger("attacksLeft") > 0)
        {
            do
            {
                _plasticBoss = animator.GetComponent<BossController>();
                
                 if (attackNum == 0)
                 {
                     animator.SetBool("setTraj", true);
                     _plasticBoss = animator.GetComponent<BossController>();
                     _plasticBoss.ShootSetTrajectoryWaves(5, 0.5f, 3, 0.5f, 0.05f);
                     Debug.Log("SET TRAJ ATTACK GO");
                 }
                 else if (attackNum == 1)
                 {
                     animator.SetBool("Turtle", true);
                     _plasticBoss = animator.GetComponent<BossController>();
                     _plasticBoss.TurtleAttack(12, 12);
                     Debug.Log("TURTLE ATTACK GO");
                 }
                 else if (attackNum == 2)
                 {
                     animator.SetBool("Urchin", true);
                     _plasticBoss = animator.GetComponent<BossController>();
                     _plasticBoss.UrchinAttack(NumPlasticSpawn);
                     Debug.Log("URCHIN ATTACK GO");
                 }
                 else if (attackNum == 3)
                 {
                     animator.SetBool("HomingShot", true);
                     _plasticBoss = animator.GetComponent<BossController>();
                     _plasticBoss.ShootHomingShots(5, 1f, 0.03f);
                     Debug.Log("HOMING SHOT GO");
                 }
                 else
                 {
                     animator.SetBool("SlowHoming", true);
                     _plasticBoss = animator.GetComponent<BossController>();
                     _plasticBoss.ShootSlowHomingShots(3, 5f);
                     Debug.Log("SLOW SHOT GO");
                 }
                 i--;
                 temp = attackNum;
                 while(temp == attackNum)
                 {
                     attackNum = Random.Range(0, 4);
                 }
                
            } while (i > 0);
                
            
        }
        else
        {
            animator.SetBool("specialAttack", true);
        }
    }
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
