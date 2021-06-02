using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToDestinationState : IState
{
    private readonly UnitController unit;
    private readonly NavMeshAgent navAgent;
    private readonly Animator animator;

    private Vector3 currentDestination;
    
    public MoveToDestinationState(UnitController _unit, NavMeshAgent _navAgent, Animator _animator,Transform _clickMarkerTransform,GameObject _clickMarker)
    {
        unit = _unit;
        navAgent = _navAgent;
        animator = _animator;
    }
    public void OnEnter()
    {
        currentDestination = unit.unitDestination;
        navAgent.SetDestination(unit.unitDestination);
        animator.SetBool("isRunning", true);
    }

    public void OnExit()
    {
        unit.unitDestination = unit.transform.position;
        animator.SetBool("isRunning", false);
        unit.ClearPath();
    }

    public void Tick()
    {
        if(currentDestination!=unit.unitDestination)
        {
            currentDestination = unit.unitDestination;
            navAgent.SetDestination(unit.unitDestination);            
        }
        unit.DrawPath();
    }

    

}
