using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    #region Fields
    public Enemy enemyType;
    public int enemyAmount;
    private float spawnDelay;
    private Transform spawnPoint;
    #endregion

    #region Methods
    public void Spawn(Enemy enemy, int amount, float delay)
    { 
    
    }
    #endregion
}
