using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobEatState : AbstractMobFiniteState
{
    public MobEatState(Mob mob, Enum stateID) : base(mob, stateID)
    {
    }

    public override void OnEnter()
    {
        mob.agent.SetDestination(mob.foodInRange.transform.position);
    }

    public override void OnExit()
    {
    }

    public override void OnFixedUpdate()
    {
        ProcessRefreshDestination();
    }

    public override void OnUpdate()
    {
    }

    public override IFiniteState SwitchStateDecision()
    {
        if (mob.foodInRange != null && mob.foodInRange.gameObject.activeSelf == false)
            mob.foodInRange = null;

        if (mob.foodInRange == null)
            return mob.stateDictionary[Mob.MobFSMState.Wander];

        if (mob.agent.remainingDistance < 0.4F)
            return mob.stateDictionary[Mob.MobFSMState.Chomp];
        else
            return this;
    }

    private float destinationRefreshElasped = 0;
    private float navMeshRefreshDestinationFrequency = 0.5f;
    private void ProcessRefreshDestination()
    {
        if (destinationRefreshElasped < navMeshRefreshDestinationFrequency)
            destinationRefreshElasped += Time.fixedDeltaTime;

        if (destinationRefreshElasped >= navMeshRefreshDestinationFrequency)
        {
            destinationRefreshElasped = 0;
            mob.agent.SetDestination(mob.foodInRange.transform.position);
        }
    }
}
