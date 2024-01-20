using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        
        //Attack
        damage = 5;
        attackIndex = 1;
        duration = 0.5f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
        // Move player forward
        //Vector3 forwardMovement = _stateMachine.transform.forward * movespeed * duration;
       // _stateMachine.transform.position += forwardMovement;

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new GroundComboState());
            }else 
            {
                
                stateMachine.SetNextStateToMain();
                
            }
        }
    }
}

