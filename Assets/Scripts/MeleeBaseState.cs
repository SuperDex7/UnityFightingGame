using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class MeleeBaseState : State
    {
    private Health healthComponent;
    // How long this state should be active for before moving on
    public float duration;
        // Cached animator component
        protected Animator animator;
        // bool to check whether or not the next attack in the sequence should be played or not
        protected bool shouldCombo;
        // The attack index in the sequence of attacks
        protected int attackIndex;
    public PlayerMovement playerMovement;
    public float movespeed;
    public LayerMask enemyLayers;
    // The cached hit collider component of this attack
    protected Collider2D hitCollider;
        // Cached already struck objects of said attack to avoid overlapping attacks on same target
        private List<Collider2D> collidersDamaged;
        // The Hit Effect to Spawn on the afflicted Enemy
        private GameObject HitEffectPrefab;

        // Input buffer Timer
        private float AttackPressedTimer = 0;
    public float damage;

    public override void OnEnter(StateMachine _stateMachine)
        {
            base.OnEnter(_stateMachine);
            animator = GetComponent<Animator>();
            collidersDamaged = new List<Collider2D>();
            hitCollider = GetComponent<ComboCharacter>().hitbox;
            HitEffectPrefab = GetComponent<ComboCharacter>().Hiteffect;
        // Get the Health component from the same game object
        healthComponent = GetComponent<Health>();
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        float moveSpeed = playerMovement.GetMoveSpeed();
        movespeed = moveSpeed;
        ///playerMovement.isAttacking = true;
    }

        public override void OnUpdate()
        {
            base.OnUpdate();
            AttackPressedTimer -= Time.deltaTime;

            if (animator.GetFloat("Weapon.Active") > 0f)
            {
                Attack();
            }
            if (Input.GetKey(KeyCode.L))
        {
            
            Block();
        }

            if (Input.GetKeyDown(KeyCode.J))
            {
                AttackPressedTimer = 0.1f;

            }

            if (animator.GetFloat("AttackWindow.Open") > 0f && AttackPressedTimer > 0)
            {
                shouldCombo = true;
            }
        }

        public override void OnExit()
        {
//playerMovement.isAttacking = false;
            base.OnExit();
        
    }

        protected void Attack()
        {
            Collider2D[] collidersToDamage = new Collider2D[10];
            ContactFilter2D filter = new ContactFilter2D();
        //filter.useTriggers = true;
        
            int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
            for (int i = 0; i < colliderCount; i++)
            {

                if (!collidersDamaged.Contains(collidersToDamage[i]))
                {
                    TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

                    // Only check colliders with a valid Team Componnent attached
                    if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
                    {
                        GameObject.Instantiate(HitEffectPrefab, collidersToDamage[i].transform);
                        Debug.Log("" + collidersToDamage[i] + attackIndex +":" + damage);
                        collidersDamaged.Add(collidersToDamage[i]);
                    // Subtract 10 from the enemy's health
                    Health enemyHealth = collidersToDamage[i].GetComponent<Health>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.health -= damage;
                    }
                }
                }
            }
        }
    protected void Block()
    {
        
    }

    }

