using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobChompState : AbstractMobFiniteState
{
    public MobChompState(Mob mob, Enum stateID) : base(mob, stateID)
    {
    }

    public override void OnEnter()
    {
        mob.chompAnimationPlaying = true;
        mob.animator.SetTrigger("chomp");
    }

    public override void OnExit()
    {
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
    }

    public override IFiniteState SwitchStateDecision()
    {
        if (!mob.chompAnimationPlaying)
            return mob.stateDictionary[Mob.MobFSMState.Wander];
        else
            return this;
    }
}
