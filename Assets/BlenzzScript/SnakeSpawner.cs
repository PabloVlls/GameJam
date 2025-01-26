using System.Collections;
using UnityEngine;

public class SnakeSpawner : MonoBehaviour
{
    public GameObject snakePrefab; // Prefab de la serpiente
    public Transform[] spawnPoints; // Puntos de spawn (pueden ser varios)
    public Transform[] endPoints;   // Puntos finales (relacionados con los puntos de spawn)
    public float minSpawnTime = 1f; // Tiempo mínimo entre spawns
    public float maxSpawnTime = 3f; // Tiempo máximo entre spawns

    private void Start()
    {
        StartCoroutine(SpawnSnakes());
    }

    private IEnumerator SpawnSnakes()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnSnake();
        }
    }

    private void SpawnSnake()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];
        Transform endPoint = endPoints[spawnIndex];

        GameObject newSnake = Instantiate(snakePrefab, spawnPoint.position, Quaternion.identity);

        // Configurar el comportamiento de la serpiente
        Snake snakeBehavior = newSnake.GetComponent<Snake>();
        snakeBehavior.startPoint = spawnPoint.position;
        snakeBehavior.endPoint = endPoint.position;
    }
}

