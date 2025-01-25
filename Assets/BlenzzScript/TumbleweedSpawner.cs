using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleweedSpawner : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    public GameObject tumbleweedPrefab; // Prefab del arbusto rodante
    public Vector2 spawnIntervalRange = new Vector2(2f, 5f); // Rango de tiempo entre apariciones
    public Vector2 spawnXRange = new Vector2(-30f, -20f); // Rango de posición X de inicio
    public Vector2 spawnYRange = new Vector2(0f, 2f); // Rango de posición Y de inicio
    public Vector2 spawnZRange = new Vector2(-10f, 10f); // Rango de posición Z de inicio
    public float tumbleweedTargetX = 20f; // Coordenada X de destino para los arbustos

    private void Start()
    {
        // Inicia la generación de arbustos
        StartCoroutine(SpawnTumbleweeds());
    }

    private System.Collections.IEnumerator SpawnTumbleweeds()
    {
        while (true)
        {
            // Esperar un tiempo aleatorio antes de generar un arbusto
            float spawnInterval = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
            yield return new WaitForSeconds(spawnInterval);

            // Generar un nuevo arbusto
            SpawnTumbleweed();
        }
    }

    private void SpawnTumbleweed()
    {
        // Calcular posición aleatoria dentro de los rangos definidos
        float spawnX = Random.Range(spawnXRange.x, spawnXRange.y);
        float spawnY = Random.Range(spawnYRange.x, spawnYRange.y);
        float spawnZ = Random.Range(spawnZRange.x, spawnZRange.y);

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        // Instanciar el prefab del arbusto rodante en la posición calculada
        GameObject newTumbleweed = Instantiate(tumbleweedPrefab, spawnPosition, Quaternion.identity);

        // Configurar el destino del arbusto
        Tumbleweed tumbleweedScript = newTumbleweed.GetComponent<Tumbleweed>();
        if (tumbleweedScript != null)
        {
            tumbleweedScript.targetX = tumbleweedTargetX;
        }

        Debug.Log("Arbusto rodante generado en posición: " + spawnPosition);
    }
}


