using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeEntry : MonoBehaviour
{
    [Header("Configuración")]
    public Transform exitPoint; // Punto de salida (el objeto asociado)
    public float teleportDelay = 0.3f; // Tiempo de retraso antes de teletransportar

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Iniciar el teletransporte con un delay
            StartCoroutine(TeleportPlayer(other));
        }
    }

    private IEnumerator TeleportPlayer(Collider player)
    {
        // Esperar el tiempo especificado antes de teletransportar
        yield return new WaitForSeconds(teleportDelay);

        // Verificar que exista un punto de salida configurado
        if (exitPoint != null)
        {
            // Teletransportar al jugador al punto de salida
            player.transform.position = exitPoint.position;

            // Opcional: reiniciar la rotación del jugador si lo deseas
            player.transform.rotation = exitPoint.rotation;

            Debug.Log("Jugador teletransportado al punto de salida.");
        }
        else
        {
            Debug.LogWarning("No se ha asignado un punto de salida para esta tubería.");
        }
    }
}

