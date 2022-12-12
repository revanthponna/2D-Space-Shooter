using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Setting min and max Y-axis values for spawning
    public float min_Y = -4.3f, max_Y = 4.3f;

    public GameObject[] asteroidPrefabs;
    public GameObject enemyPrefab;

    public float timer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemies", timer);
    }

    void SpawnEnemies()
    {
        // Getting random position to spawn enemies
        float posY = Random.Range(min_Y, max_Y);
        Vector3 temp = transform.position;
        temp.y = posY;
        // Choosing number between 0 and 1 randomly
        if(Random.Range(0, 2) > 0)
        {
            Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], temp, Quaternion.identity); // If 1, we spawn an asteroid obstacle
        }
        else
        {
            Instantiate(enemyPrefab, temp, Quaternion.Euler(0f, 0f, 90f)); // If 0, we spawn an enemy spaceship
        }

        Invoke("SpawnEnemies", timer);
    }
    
}
