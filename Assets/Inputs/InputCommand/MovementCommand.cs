using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCommand : Command
{
    // animasyonu da burada yapabilirim. sadece yürüme
    [SerializeField]
    private float movementSpeed ;

    private IMovementInput _move;
        
    //private Vector3 moveDirection = Vector3.zero;
    private Coroutine movementCoroutune;
    private Vector3 newPosition;

    private void Awake()
    {
        _move = GetComponent<IMovementInput>();
    }
    public override void ExecuteWithVector3(Vector3 vector3)
    {
        newPosition = Vector3.zero;
        if(movementCoroutune == null) movementCoroutune = StartCoroutine(Move());
        
    }

    private IEnumerator Move()
    {
        
        while (_move.moveDirection != Vector3.zero)
        {
            newPosition = Vector3.zero;
            if (_move.moveDirection.z > 0)
            {
                newPosition += (transform.forward / movementSpeed);
            }
            if (_move.moveDirection.z < 0)
            {
                newPosition -= (transform.forward / movementSpeed);
            }
            if (_move.moveDirection.x > 0)
            {
                newPosition += (transform.right / movementSpeed);
            }
            if (_move.moveDirection.x < 0)
            {
                newPosition -= (transform.right / movementSpeed);
            }
           transform.position += newPosition;
            yield return null;
        }
        movementCoroutune = null;
    }

}
