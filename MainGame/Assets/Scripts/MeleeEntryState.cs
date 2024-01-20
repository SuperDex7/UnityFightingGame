using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntryState : State
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        PlayerMovement playerMovement = _stateMachine.GetComponent<PlayerMovement>();

        if (playerMovement.IsGrounded() && playerMovement.Sprinting())
        {
            _stateMachine.SetNextState(new RunningEntryState());
        }
        else if (playerMovement.IsGrounded())
        {
            _stateMachine.SetNextState(new GroundEntryState());
        }
        else if (!playerMovement.IsGrounded())
        {
            _stateMachine.SetNextState(new AirEntryState());
        }
    }
}
