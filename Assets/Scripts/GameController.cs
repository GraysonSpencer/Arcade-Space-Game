using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the game.
/// Generates the enemies.
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// Number of enemies to be spawned.
    /// </summary>
    public int EnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < EnemyCount; i++)
        {
            GameObject enemy;  
            if (ObjectPooler.sharedInstance.GetPooledObject("EnemyShip", out enemy))
            {
                enemy.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Spawns an enemy ship.
    /// </summary>
    public void SpawnEnemy()
    {
        GameObject enemy; 
        if (ObjectPooler.sharedInstance.GetPooledObject("EnemyShip", out enemy))
        {
            enemy.SetActive(true);
        }
    }
}
