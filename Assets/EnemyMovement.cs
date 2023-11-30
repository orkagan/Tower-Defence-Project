using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject _currentTarget;
    [SerializeField] private GameObject _drill;
    [SerializeField] private List<GameObject> _potentialTargets;

    private NavMeshAgent _agent => GetComponentInChildren<NavMeshAgent>();
    private Enemy _enemy => GetComponent<Enemy>();
    private float DistanceToSearch => _enemy.GetRange;

	private void OnValidate()
	{
        populateTargets();
	}
	private void Start()
    {
        
    }

    private void Update()
    {
        FindTarget();
    }
    private void populateTargets()
	{
        _drill = GameObject.FindGameObjectWithTag("Drill");

        _potentialTargets.Clear();
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
		{
            _potentialTargets.Add(player);
        }
    }

    private void FindTarget()
    {
        //gets closest player in range or null
        GameObject playerTarget = IsPlayerInRange();
		if (playerTarget != null)
		{
            _currentTarget = playerTarget;
		}
		else if(_drill != null)
		{
            //otherwise targets the drill
            _currentTarget = _drill;
		}
		else
		{
            //if no drill do nothing (mostly for testing)
            return;
		}
        _agent.SetDestination(_currentTarget.transform.position);
    }

    private GameObject IsPlayerInRange()
    {
        float closestPlayerDist = float.PositiveInfinity;
        GameObject closestPlayer = null;

        //go through list of targets(players)
        foreach (GameObject target in _potentialTargets)
		{
            float dist = Vector3.Distance(target.transform.position, transform.position);
            if (dist < closestPlayerDist & dist < DistanceToSearch)
			{
                closestPlayerDist = dist;
                closestPlayer = target;
			}
        }
        //returns the closest player within search range or null
        return closestPlayer;
    }
}