using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMobFiniteState : IFiniteState
{
    public Mob mob;
    private Enum stateID;

    public AbstractMobFiniteState(Mob mob, Enum stateID)
    {
        this.mob = mob;
        this.stateID = stateID;
    }

    public Enum GetStateID()
    {
        return stateID;
    }

    public abstract void OnEnter();

    public abstract void OnExit();

    public abstract void OnFixedUpdate();

    public abstract void OnUpdate();

    public abstract IFiniteState SwitchStateDecision();
}
