using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    public GameObject birdPrefab; // Prefab del pájaro
    public Vector2 spawnIntervalRange = new Vector2(2f, 5f); // Rango de tiempo entre apariciones
    public Vector2 spawnXRange = new Vector2(-30f, -20f); // Rango de posición X de inicio
    public Vector2 spawnYRange = new Vector2(5f, 15f); // Rango de posición Y de inicio
    public Vector2 spawnZRange = new Vector2(-10f, 10f); // Rango de posición Z de inicio
    public float birdTargetX = 20f; // Coordenada X de destino para los pájaros

    private void Start()
    {
        // Inicia la generación de pájaros
        StartCoroutine(SpawnBirds());
    }

    private System.Collections.IEnumerator SpawnBirds()
    {
        while (true)
        {
            // Esperar un tiempo aleatorio antes de generar un pájaro
            float spawnInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
            yield return new WaitForSeconds(spawnInterval);

            // Generar un nuevo pájaro
            SpawnBird();
        }
    }

    private void SpawnBird()
    {
        // Calcular posición aleatoria dentro de los rangos definidos
        float spawnX = Random.Range(spawnXRange.x, spawnXRange.y);
        float spawnY = Random.Range(spawnYRange.x, spawnYRange.y);
        float spawnZ = Random.Range(spawnZRange.x, spawnZRange.y);

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        // Crear una rotación de 180 grados en el eje Z
        Quaternion spawnRotation = Quaternion.Euler(0, 0, 180);

        // Instanciar el prefab del pájaro con la rotación calculada
        GameObject newBird = Instantiate(birdPrefab, spawnPosition, spawnRotation);

        // Configurar el destino del pájaro
        Bird birdScript = newBird.GetComponent<Bird>();
        if (birdScript != null)
        {
            birdScript.targetX = birdTargetX;
        }

        Debug.Log("Pájaro generado en posición: " + spawnPosition + " con rotación: " + spawnRotation.eulerAngles);
    }

}


