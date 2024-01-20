using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCombo2State : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        PlayerMovement playerMovement = _stateMachine.GetComponent<PlayerMovement>();

        //Attack
        damage = 30;
        attackIndex = 3;
        duration = 0.5f;
        animator.SetTrigger("Attack" + attackIndex);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new GroundFinisherState());
            } else if (!playerMovement.IsGrounded())
            {
                stateMachine.SetNextState(new AirEntryState());
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}