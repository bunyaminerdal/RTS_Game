using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToDestinationState : IState
{
    private readonly UnitController unit;
    private readonly NavMeshAgent navAgent;
    private readonly Animator animator;
    private LineRenderer lineRenderer;
    private Transform clickMarkerTransform;
    private GameObject clickMarker;

    private Vector3 currentDestination;
    
    public MoveToDestinationState(UnitController _unit, NavMeshAgent _navAgent, Animator _animator,LineRenderer _lineRenderer,Transform _clickMarkerTransform,GameObject _clickMarker)
    {
        unit = _unit;
        navAgent = _navAgent;
        animator = _animator;
        lineRenderer = _lineRenderer;
        clickMarkerTransform = _clickMarkerTransform;
        clickMarker = _clickMarker;
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
        ClearPath();
    }

    public void Tick()
    {
        if(currentDestination!=unit.unitDestination)
        {
            currentDestination = unit.unitDestination;
            navAgent.SetDestination(unit.unitDestination);            
        }
        DrawPath();
    }

    //clickMarker
    void DrawPath()
    {
        if (unit.isSelected())
        {
            lineRenderer.positionCount = navAgent.path.corners.Length;
            lineRenderer.SetPosition(0, unit.transform.position);
            if (navAgent.path.corners.Length < 2)
            {
                return;
            }
            Vector3 pointPos = Vector3.zero;
            for (int i = 0; i < navAgent.path.corners.Length; i++)
            {
                pointPos = new Vector3(navAgent.path.corners[i].x, navAgent.path.corners[i].y, navAgent.path.corners[i].z);
                lineRenderer.SetPosition(i, pointPos);
            }
            clickMarker.transform.position = pointPos;
            if (!clickMarker.activeSelf)
            {
                //click marker                
                clickMarker.transform.SetParent(clickMarkerTransform);
                clickMarker.SetActive(true);
            }
        }
        else
        {
            if (clickMarker.activeSelf)
            {
                clickMarker.SetActive(false);
                clickMarker.transform.position = unit.transform.position;
                clickMarker.transform.SetParent(unit.transform);
                lineRenderer.positionCount = 0;
            }
        }

    }

    void ClearPath()
    {
        if (clickMarker.activeSelf)
        {
            clickMarker.SetActive(false);
            clickMarker.transform.position = unit.transform.position;
            clickMarker.transform.SetParent(unit.transform);
            lineRenderer.positionCount = 0;
        }
    }

}
