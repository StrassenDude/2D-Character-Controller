using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamagable, ICharacterMovable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }

    private void Start()
    {
        CurrentHealth  = MaxHealth;

        StateMachine.Initialize(FlyingState);

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        StateMachine.CurrentCharacterState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentCharacterState.PhysicsUpdate();
    }

    #region State Machine Variables


    private void Awake()
    {
        StateMachine = new CharacterStateMachine();

        ShootingState = new CharacterShootingState(this, StateMachine);
        DashingState = new CharacterDashingState(this, StateMachine);
        FlyingState = new CharacterFlyingState(this, StateMachine);
        OnWaterState = new CharacterOnWaterState(this, StateMachine);
    }


    public CharacterStateMachine StateMachine { get; set; }

    public CharacterFlyingState FlyingState { get; set; }
    public CharacterShootingState ShootingState { get; set; }
    public CharacterDashingState DashingState { get; set; }
    public CharacterOnWaterState OnWaterState { get; set; }
   



    #endregion

    

    #region Health/Die Functions
    public void Damage(float damgeAmaount)
    {
        CurrentHealth -= damgeAmaount;

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

    #region Check Movement

    public Rigidbody2D rb { get; set; }


    public void MoveCharacter()
    {

    }


    #endregion


    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        //TODO: fill on state
    }
    public enum AnimationTriggerType
    {
        CharacterDamaged,
        CharacterShooting
    }

    #endregion
}
