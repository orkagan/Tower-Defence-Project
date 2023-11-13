using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindTarget : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        GameObject target = GameObject.FindGameObjectWithTag("Drill");
        Vector3 position = target.transform.position;
        _agent.SetDestination(position);
    }
}
