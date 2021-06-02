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
    private Transform interactPoint;
    private float gatherTimer;
    public GatherState(UnitController _unit,NavMeshAgent _navAgent, Animator _animator)
    {
        unit = _unit;
        animator = _animator;
        navAgent = _navAgent;
    }
    public void OnEnter()
    {
        interactable = unit.currentGatherResource.GetComponent<Interactable>();
        interactPoint = unit.currentGatherResource.Find("InteractionPoint").gameObject.transform;
        if (Vector3.Distance(interactPoint.position, unit.transform.position) > navAgent.stoppingDistance)
        {
            animator.SetBool("isRunning", true);
            navAgent.SetDestination(interactPoint.position);
        }
        
        gatherTimer = unit.GatherTimer;
        interactable.takeInteractSlot();
    }

    public void OnExit()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isGathering", false);
        interactable.giveInteractSlot();
        unit.currentGatherResource = null;
        interactable = null;
        gatherTimer = 0;
        unit.ClearPath();
        if (unit.NewMeshWeapon != null)
            if (!unit.NewMeshWeapon.gameObject.activeSelf)
                unit.NewMeshWeapon.gameObject.SetActive(true);
    }

    public void Tick()
    {
        
        if (Vector3.Distance(interactPoint.position, unit.transform.position) <= navAgent.stoppingDistance )
        {
            FaceTarget(unit.currentGatherResource);
            if (gatherTimer == unit.GatherTimer)
            {
                unit.ClearPath();
                animator.SetBool("isGathering", true);
                
                animator.SetBool("isRunning", false);
                if (unit.NewMeshWeapon != null)
                    if (unit.NewMeshWeapon.gameObject.activeSelf)
                        unit.NewMeshWeapon.gameObject.SetActive(false);
            }  
            Gather();
        }
        else
        {
            unit.DrawPath();
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
    void FaceTarget(Transform target)
    {
        Vector3 direction = (target.position - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
