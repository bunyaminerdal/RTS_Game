using UnityEngine;
using UnityEngine.AI;

public class TakeGroundItemState : IState
{
    private readonly UnitController unit;
    private readonly NavMeshAgent navAgent;
    private readonly Animator animator;
    private GroundItem groundItem;
    public TakeGroundItemState(UnitController _unit,NavMeshAgent _navAgent, Animator _animator)
    {
        unit = _unit;
        navAgent = _navAgent;
        animator = _animator;
    }
    public void OnEnter()
    {
        if(Vector3.Distance(unit.currentTargetItem.position,unit.transform.position) > navAgent.stoppingDistance)
        {
            animator.SetBool("isRunning", true);
            navAgent.SetDestination(unit.currentTargetItem.position);
        }        
        groundItem = unit.currentTargetItem.GetComponent<GroundItem>();
        
    }

    public void OnExit()
    {
        animator.SetBool("isRunning", false);
        unit.currentTargetItem = null;
    }

    public void Tick()
    {
        if(navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            unit.currentTargetItem = null;
            unit.addItemToInventory(groundItem.item);
            unit.DestroyGameObject(groundItem.gameObject);
        }       
        
    }
}
