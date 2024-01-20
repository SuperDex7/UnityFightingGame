using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    private StateMachine meleeStateMachine;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            
            meleeStateMachine.SetNextState(new MeleeEntryState());

        }
        if (Input.GetKey(KeyCode.L) && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            
            meleeStateMachine.SetNextState(new Block());
        }
    }
}