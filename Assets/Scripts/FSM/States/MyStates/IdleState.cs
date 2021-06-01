using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : IState
{
    private readonly UnitController unit;
    private readonly NavMeshAgent navAgent;
    private readonly Animator animator;
    public IdleState(UnitController _unit, NavMeshAgent _navAgent,Animator _animator)
    {
        unit = _unit;
        navAgent = _navAgent;
        animator = _animator;
    }
    public void OnEnter()
    {
        Debug.Log("enter idle");
    }

    public void OnExit()
    {
        Debug.Log("exit idle");
    }

    public void Tick()
    {
        //Debug.Log("update idle");
    }
    
}
