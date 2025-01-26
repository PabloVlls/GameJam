using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [Header("Configuración de Luz y Raycast")]
    public Light spotLight; // La Spot Light del espejo
    public float rayDistance = 10f; // Distancia del Raycast
    public LayerMask detectionLayer; // Capas que el raycast puede detectar
    public Light postLight; // La luz del poste que activa el sistema

    [Header("Configuración de Rotación del Espejo")]
    public float rotationSpeed = 50f; // Velocidad de rotación
    public float minRotationAngle = -90f; // Ángulo mínimo de rotación
    public float maxRotationAngle = 90f; // Ángulo máximo de rotación

    private float currentRotationAngle = 0f; // Ángulo actual del espejo
    private bool rotatingRight = true; // Indica si el espejo está rotando hacia la derecha

    private void Update()
    {
        // Rotación del Espejo
        HandleRotation();

        // Activar el Raycast solo si la luz del poste está encendida
        if (postLight != null && postLight.enabled)
        {
            HandleRaycast();
        }
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(0)) // Detectar clic izquierdo del mouse
        {
            // Lanzar un raycast desde la cámara hacia donde apunta el mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Verificar si el objeto tocado es este espejo
                if (hit.collider.gameObject == gameObject)
                {
                    // Realizar la rotación
                    float rotationDirection = rotatingRight ? 1f : -1f;
                    float rotationAmount = rotationSpeed * Time.deltaTime * rotationDirection;

                    // Calcular el nuevo ángulo
                    float newRotationAngle = currentRotationAngle + rotationAmount;

                    // Verificar si alcanzamos los límites de rotación
                    if (newRotationAngle > maxRotationAngle)
                    {
                        newRotationAngle = maxRotationAngle;
                        rotatingRight = false; // Cambiar dirección
                    }
                    else if (newRotationAngle < minRotationAngle)
                    {
                        newRotationAngle = minRotationAngle;
                        rotatingRight = true; // Cambiar dirección
                    }

                    // Actualizar el ángulo actual y rotar el espejo solo en el eje Y
                    currentRotationAngle = newRotationAngle;

                    // Asegurarse de que solo se rota sobre el eje Y
                    Vector3 currentRotation = transform.rotation.eulerAngles;
                    transform.rotation = Quaternion.Euler(currentRotation.x, currentRotationAngle, currentRotation.z);
                }
            }
        }
    }


    private void HandleRaycast()
    {
        if (spotLight != null && spotLight.enabled)
        {
            // Dirección y origen del raycast
            Vector3 rayDirection = spotLight.transform.forward;
            Vector3 rayOrigin = spotLight.transform.position;

            // Dibujar el rayo
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

            // Crear el raycast
            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayDistance, detectionLayer))
            {
                // Si golpea un objeto con el tag "Door", lo abrimos
                if (hit.collider.CompareTag("Door"))
                {
                    hit.collider.GetComponent<Door>()?.OpenDoor();
                }
            }
        }
    }
}

