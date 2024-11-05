using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMovable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    public Rigidbody2D _rb { get; set; }
    public bool isFacingRight { get; set; } = true;

    public bool IsAggroed { get; set; }
    public bool IsWhithinStrikingDistance { get; set; }

    #region State Machine Variables

    public EnemyStateMachine StateMachine {  get; set; }
    
    public EnemyIdleState IdleState { get; set; }

    public EnemyAttackState AttackState { get; set; }

    public EnemyChaseState ChaseState { get; set; }


    #endregion

    #region Idle Variables

    public Rigidbody2D BulletPrefab;
    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 1f;

    #endregion

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;

        _rb = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }




    #region Health Die functions
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        Destroy(gameObject);
    }

    #endregion


    #region Movement funktions
    public void MoveEnemy(Vector2 velocity)
    {
        _rb.velocity = velocity;

    }

    public void CheckforLeftOrRightFacing(Vector2 velocity)
    {

    }

    #endregion

    #region Distance Checks

    public void SetAggroState(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetWhithinStrikingDistance(bool isWhithinStrikingDistance)
    {
        IsWhithinStrikingDistance = isWhithinStrikingDistance;
    }

    #endregion


    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }


    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayDeath
    }

    #endregion
}
