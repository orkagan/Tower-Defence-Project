using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] Transform _gameMap => GameObject.FindGameObjectWithTag("GameMap").transform;

    [SerializeField, Tooltip("The amount of enemies which spawn every turn.")]
    int _enemySpawnCountPerTurn = 15;

    [SerializeField, Tooltip("How much the above number will increase every following turn.")]
    int _countIncreasePerFollowingTurn = 3;

    [SerializeField, Space(20), Tooltip("The areas where the enemies spawn.")]
    Transform[] _spawnPoints;

    public UnityEvent onFinishSpawning;

    private void Start()
    {
        onFinishSpawning.AddListener(() => StartCoroutine(nameof(CountEnemies)));
    }

    public void BeginSpawning() => StartCoroutine(nameof(SpawnEnemies));

    private IEnumerator CountEnemies()
    {
        while (GameStateHandler.Instance.GetCurrentState == GameState.AttackPhase)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length == 0)
            {
                GameStateHandler.Instance.SwitchState();
            }

            yield return null;
        }
    }

    //spawns a set number of enemies
    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < _enemySpawnCountPerTurn; i++)
        {
            int randomSpawn = Random.Range(0, _spawnPoints.Length);
            GameObject newEnemy = Instantiate(_enemyPrefab, GetRandomPointInBoxCollider(_spawnPoints[randomSpawn]), Quaternion.identity, _gameMap);
            newEnemy.name = $"Enemy #{i + 1}";
            newEnemy.GetComponent<NetworkObject>().Spawn();
            Debug.Log($"Spawned {newEnemy.name}");
            
            yield return new WaitForSeconds(0.5f);
        }
        
        onFinishSpawning.Invoke();
    }

    //returns random position within a transform's box collider.
    private Vector3 GetRandomPointInBoxCollider(Transform area)
    {
        BoxCollider collider = area.gameObject.GetComponent<BoxCollider>();

        Vector3 spawn = new Vector3(
            Random.Range(0, collider.size.x / 2),
            0f,
            Random.Range(0, collider.size.z / 2));

        return area.position + spawn;
    }

    //increases the number of enemies that spawn each turn
    public void IncreaseEnemySpawnCount()
    {
        _enemySpawnCountPerTurn += _countIncreasePerFollowingTurn;
    }
}