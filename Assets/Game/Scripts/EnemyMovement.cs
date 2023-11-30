using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{    
    private GameObject _target, _player, _drill;

    private NavMeshAgent _agent => GetComponentInChildren<NavMeshAgent>();
    private Enemy _enemy => GetComponent<Enemy>();
    private float _distanceToSearch => _enemy.GetRange;

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

        if (distance < _distanceToSearch)
        {
            return true;
        }

        return false;
    }
}