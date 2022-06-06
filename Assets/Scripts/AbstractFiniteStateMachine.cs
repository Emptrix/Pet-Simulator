using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFiniteStateMachine : MonoBehaviour
{
    public IFiniteState currentState = null;
    public string currentStatesDisplay = "";
    public Dictionary<Enum, IFiniteState> stateDictionary = new Dictionary<Enum, IFiniteState>();
    public IFiniteState defaultState = null;

    private void OnEnable()
    {
        ReturnToDefaultState();
    }

    public virtual void Update()
    {
        currentState.OnUpdate();

        if (currentState.SwitchStateDecision() != currentState)
        {
            SwitchState(currentState.SwitchStateDecision().GetStateID());
        }
    }

    public virtual void FixedUpdate()
    {
        currentState.OnFixedUpdate();        
    }

    protected void ReturnToDefaultState()
    {
        SwitchState(defaultState.GetStateID());
    }

    public void SwitchState(Enum stateId)
    {
        if (null != currentState)
            currentState.OnExit();

        currentState = stateDictionary[stateId];
        currentStatesDisplay = stateDictionary[stateId].GetStateID().ToString();
        currentState.OnEnter();
    }
}

public interface IFiniteState
{
    public void OnEnter();
    public void OnExit();
    public void OnUpdate();
    public void OnFixedUpdate();
    public IFiniteState SwitchStateDecision();
    public Enum GetStateID();
}