using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    private float dirx = 0f;
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    [SerializeField]private PlayerData playerData;
    public Rigidbody2D RB { get; private set; }
    private Vector2 workspace;
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        FacingDirection = 1;
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        StateMachine.Initialize(IdleState);
        RB = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
           dirx = Input.GetAxis("Horizontal");
        RB.velocity = new Vector2(dirx * playerData.movementVelocity, RB.velocity.y);
        //CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
        

    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    public void SetVelocityX(float velocity)
    {
        
        //workspace.Set(velocity, CurrentVelocity.y);
        //RB.velocity = workspace;
        //CurrentVelocity = workspace;
    }
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput!= 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection  *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
