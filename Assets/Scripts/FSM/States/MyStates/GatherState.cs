using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GatherState : IState
{
    private readonly UnitController unit;
    private readonly NavMeshAgent navAgent;
    private readonly Animator animator;
    private Interactable interactable;
    private float gatherTimer;
    public GatherState(UnitController _unit,NavMeshAgent _navAgent, Animator _animator)
    {
        unit = _unit;
        animator = _animator;
        navAgent = _navAgent;
    }
    public void OnEnter()
    {
        if (Vector3.Distance(unit.currentGatherResource.position, unit.transform.position) > navAgent.stoppingDistance)
        {
            animator.SetBool("isRunning", true);
            navAgent.SetDestination(unit.currentGatherResource.position);
        }
        interactable = unit.currentGatherResource.GetComponent<Interactable>();
        gatherTimer = unit.GatherTimer;
        interactable.takeInteractSlot();
    }

    public void OnExit()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isGathering", false);
        unit.currentGatherResource = null;
        gatherTimer = 0;
        interactable.giveInteractSlot();
        if (unit.NewMeshWeapon != null)
            if (!unit.NewMeshWeapon.gameObject.activeSelf)
                unit.NewMeshWeapon.gameObject.SetActive(true);
    }

    public void Tick()
    {
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            animator.SetBool("isRunning", false);
            if(gatherTimer == unit.GatherTimer)
                animator.SetBool("isGathering", true);

            if (unit.NewMeshWeapon != null)
                if (unit.NewMeshWeapon.gameObject.activeSelf)
                    unit.NewMeshWeapon.gameObject.SetActive(false);
            Gather();
        }              
    }

    void Gather()
    {
        if (interactable.getCurrentAmount() <= 0 || unit.getUnitInventory().calculateFull(interactable.item))
        {
            unit.currentGatherResource = null;
        }
        else if (interactable.getCurrentAmount() > 0)
        {
            gatherTimer -= Time.deltaTime;
            if (gatherTimer <= 0)
            {
                interactable.setCurrentAmount();
                unit.addItemToInventory(interactable.item);
                gatherTimer = unit.GatherTimer;
            }
            
        }
    }
}
