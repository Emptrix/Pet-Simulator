using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobWanderState : AbstractMobFiniteState
{
    private Vector3 destination;
    public MobWanderState(Mob mob, Enum stateID) : base(mob, stateID)
    {
    }

    public override void OnEnter()
    {
        SetLocation();
    }

    public override void OnExit()
    {
        mob.agent.ResetPath();
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnUpdate()
    {
        if (mob.agent.remainingDistance < 0.5f)
            SetLocation();
    }

    public override IFiniteState SwitchStateDecision()
    {
        if (mob.foodInRange)
            return mob.stateDictionary[Mob.MobFSMState.Eat];
        else
            return this;
    }

    private void SetLocation()
    {
        mob.GetRandomPointOnNavMeshSurface(mob.transform.position, 15, out destination);
        mob.agent.SetDestination(destination);
        mob.targetDestination = destination;
    }
}
