using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunComboState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        damage = 20;
        attackIndex = 2;
        duration = 0.5f;
        animator.SetTrigger("RunAttack" + attackIndex);
        Debug.Log("Player RunAttack " + attackIndex + " Fired!");
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
