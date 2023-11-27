using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyMovement : Enemy
{    
    private GameObject _target, _player, _drill;

    private NavMeshAgent _agent => GetComponent<NavMeshAgent>();
    private float DistanceToSearch => _attackRange;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _drill = GameObject.FindGameObjectWithTag("Drill");
    }

    private void Update()
    {
        //change target to player
        //if player becomes out of range, change back to drill
        _target = IsPlayerInRange() ? _player : _drill;

        FindTarget();
    }

    private void FindTarget()
    {
        Vector3 position = _target.transform.position;
        _agent.SetDestination(position);
    }

    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance < DistanceToSearch)
        {
            return true;
        }

        return false;
    }
}