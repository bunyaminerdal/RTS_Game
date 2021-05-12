using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCommand : Command
{
    // animasyonu da burada yapabilirim. sadece yürüme
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private AnimationCurve curvedSpeed;
    private IMovementInput _move;
        
    //private Vector3 moveDirection = Vector3.zero;
    private Coroutine movementCoroutune;
    private void Awake()
    {
        _move = GetComponent<IMovementInput>();
    }
    public override void ExecuteWithVector3(Vector3 vector3)
    {
        if(movementCoroutune == null) movementCoroutune = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (_move.moveDirection != Vector3.zero)
        {
            transform.position += curvedSpeed.Evaluate(_move.moveDirection.magnitude) * _move.moveDirection;
            yield return null;
        }
        movementCoroutune = null;
    }

}
