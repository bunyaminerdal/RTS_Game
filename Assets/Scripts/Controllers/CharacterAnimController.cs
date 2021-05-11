using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterAnimController : MonoBehaviour
{
    
    NavMeshAgent agent;
    Animator animator;
    bool isRunning;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(agent.remainingDistance <= agent.stoppingDistance){
            isRunning = false;
        }else{
            isRunning = true;
        }
        animator.SetBool("isRunning",isRunning);
    }
}
