using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [Header("Configuraci�n de Luz y Raycast")]
    public Light spotLight; // La Spot Light del espejo
    public float rayDistance = 10f; // Distancia del Raycast
    public LayerMask detectionLayer; // Capas que el raycast puede detectar
    public Light postLight; // La luz del poste que activa el sistema

    [Header("Configuraci�n de Rotaci�n del Espejo")]
    public float rotationSpeed = 50f; // Velocidad de rotaci�n
    public float minRotationAngle = -90f; // �ngulo m�nimo de rotaci�n
    public float maxRotationAngle = 90f; // �ngulo m�ximo de rotaci�n

    private float currentRotationAngle = 0f; // �ngulo actual del espejo
    private bool rotatingRight = true; // Indica si el espejo est� rotando hacia la derecha

    private void Update()
    {
        // Rotaci�n del Espejo
        HandleRotation();

        // Activar el Raycast solo si la luz del poste est� encendida
        if (postLight != null && postLight.enabled)
        {
            HandleRaycast();
        }
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(0)) // Detectar clic izquierdo del mouse
        {
            // Lanzar un raycast desde la c�mara hacia donde apunta el mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Verificar si el objeto tocado es este espejo
                if (hit.collider.gameObject == gameObject)
                {
                    // Realizar la rotaci�n
                    float rotationDirection = rotatingRight ? 1f : -1f;
                    float rotationAmount = rotationSpeed * Time.deltaTime * rotationDirection;

                    // Calcular el nuevo �ngulo
                    float newRotationAngle = currentRotationAngle + rotationAmount;

                    // Verificar si alcanzamos los l�mites de rotaci�n
                    if (newRotationAngle > maxRotationAngle)
                    {
                        newRotationAngle = maxRotationAngle;
                        rotatingRight = false; // Cambiar direcci�n
                    }
                    else if (newRotationAngle < minRotationAngle)
                    {
                        newRotationAngle = minRotationAngle;
                        rotatingRight = true; // Cambiar direcci�n
                    }

                    // Actualizar el �ngulo actual y rotar el espejo solo en el eje Y
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
            // Direcci�n y origen del raycast
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

