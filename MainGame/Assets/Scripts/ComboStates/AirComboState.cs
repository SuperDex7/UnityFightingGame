using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirComboState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        damage = 25;
        attackIndex = 2;
        duration = 0.5f;
        animator.SetTrigger("AirAttack" + attackIndex);
        Debug.Log("Player AirAttack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            stateMachine.SetNextStateToMain();
        }
    }
}

