using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIntegerBehaviour : StateMachineBehaviour
{
    public string mName;
    public bool updateOnState;
    public bool updateOnStateMachine;
    public int valueOnEnter;
    public int valueOnExit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetInteger(mName, valueOnEnter);
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (updateOnState)
        {
            animator.SetInteger(mName, valueOnExit);
        }
    }
    
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetInteger(mName,valueOnEnter);
        }
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if (updateOnStateMachine)
        {
            animator.SetInteger(mName, valueOnExit);
        } 
    }
    
}
