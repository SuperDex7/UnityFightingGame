using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        damage = 20;
        attackIndex = 1;
        duration = 0.5f;
        animator.SetTrigger("AirAttack" + attackIndex);
        Debug.Log("Player AirAttack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new AirComboState());
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}
