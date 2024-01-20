using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        
        damage = 0;
        
        duration = 0.1f;
        animator.SetTrigger("Blocking");
        Debug.Log("Blocking");
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
