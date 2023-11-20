using System.Collections;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] Transform _gameMap;

    [SerializeField, Tooltip("The amount of enemies which spawn every turn.")]
    int _enemySpawnCountPerTurn = 15;

    [SerializeField, Tooltip("How much the above number will increase every following turn.")]
    int _countIncreasePerFollowingTurn = 3;

    [SerializeField, Space(20), Tooltip("The areas where the enemies spawn.")]
    Transform[] _spawnPoints;

    public void BeginSpawning() => StartCoroutine(nameof(SpawnEnemies));
    
    //spawns a set number of enemies
    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < _enemySpawnCountPerTurn; i++)
        {
            int randomSpawn = Random.Range(0, _spawnPoints.Length);
            GameObject newEnemy = Instantiate(_enemyPrefab, _gameMap);
            newEnemy.transform.position = GetRandomPointInBoxCollider(_spawnPoints[randomSpawn]);
            newEnemy.name = $"Enemy #{i + 1}";
            Debug.Log($"Spawned enemy #{i + 1}");
            
            yield return new WaitForSeconds(0.5f);
        }
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