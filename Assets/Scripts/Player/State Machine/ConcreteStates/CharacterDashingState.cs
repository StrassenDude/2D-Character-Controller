using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDashingState : CharacterState
{
    public CharacterDashingState(Character character, CharacterStateMachine characterStateMachine) : base(character, characterStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Character.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
