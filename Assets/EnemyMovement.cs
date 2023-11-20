using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private GameObject _target, _player, _drill;
    [SerializeField] private float _distanceToSearch = 5f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _drill = GameObject.FindGameObjectWithTag("Drill");
        _agent = GetComponent<NavMeshAgent>();

        FindTarget();
    }

    private void Update()
    {
        //change target to player
        //if player becomes out of range, change back to drill
        _target = IsPlayerInRange() ? _player : _drill;
    }

    private void FindTarget()
    {
        _target = GameObject.FindGameObjectWithTag("Drill");
        Vector3 position = _target.transform.position;
        _agent.SetDestination(position);
    }

    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance < _distanceToSearch)
        {
            return true;
        }

        return false;
    }
}