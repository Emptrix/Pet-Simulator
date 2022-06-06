using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob : AbstractFiniteStateMachine
{
    public enum MobFSMState { Wander, Eat, Chomp, Attack };

    [SerializeField]
    private int happiness;
    [SerializeField]
    private int hunger;
    [SerializeField]
    private float movementSpeed;
    public NavMeshAgent agent = null;
    private NavMeshPath path;
    public Animator animator;
    public bool chompAnimationPlaying = false;
    public Vector3 targetDestination;

    private void OnValidate()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        path = new NavMeshPath();
        stateDictionary.Add(MobFSMState.Wander, new MobWanderState(this, MobFSMState.Wander));
        stateDictionary.Add(MobFSMState.Eat, new MobEatState(this, MobFSMState.Eat));
        stateDictionary.Add(MobFSMState.Chomp, new MobChompState(this, MobFSMState.Chomp));

        defaultState = stateDictionary[MobFSMState.Wander];
    }

    private bool canReachPoint = false;
    public bool GetRandomPointOnNavMeshSurface(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            canReachPoint = false;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                agent.CalculatePath(hit.position, path);
                canReachPoint = path.status == NavMeshPathStatus.PathComplete;
                if (canReachPoint)
                {
                    result = hit.position;
                    return true;
                }
            }
        }
        result = agent.transform.position;
        return false;
    }

    public Food foodInRange = null;
    private Food incomingFood = null;
    private void OnTriggerEnter(Collider other)
    {
        incomingFood = other.GetComponent<Food>();
        if (incomingFood != null)
            foodInRange = incomingFood;       
    }

    private void OnTriggerExit(Collider other)
    {
        if (foodInRange == other.GetComponent<Food>())
            foodInRange = null;
    }

    public void Chomp()
    {
        foodInRange.Consume();
    }

    public void ChompAnimationComplete()
    {
        chompAnimationPlaying = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetDestination, 1);
    }
}
