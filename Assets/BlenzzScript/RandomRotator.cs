using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    [Header("Configuración de Rotación (X/Z)")]
    public bool rotateOnX = true; // Rotar en el eje X
    public bool rotateOnZ = false; // Rotar en el eje Z
    public float rotationSpeed = 50f; // Velocidad de rotación constante en X o Z

    [Header("Rotación en el Eje Y")]
    public bool rotateOnYAxis = true; // Activar/desactivar rotación en el eje Y
    public float yRotationSpeed = 30f; // Velocidad de rotación en el eje Y
    public float maxYRotationAngle = 45f; // Ángulo máximo de rotación en el eje Y

    [Header("Tiempos Aleatorios")]
    public float minActiveTime = 2f; // Tiempo mínimo de rotación
    public float maxActiveTime = 5f; // Tiempo máximo de rotación
    public float minIdleTime = 1f; // Tiempo mínimo de pausa
    public float maxIdleTime = 3f; // Tiempo máximo de pausa

    private float rotationTimer = 0f; // Temporizador para controlar la rotación
    private float currentMaxTime = 0f; // Tiempo actual antes de cambiar estado
    private bool isRotating = false; // Indica si está girando o en pausa

    private float currentYAngle = 0f; // Ángulo actual de rotación en Y
    private bool isYReversing = false; // Indica si la rotación en Y está invirtiendo

    void Start()
    {
        // Inicializar un tiempo aleatorio para el primer estado
        SetRandomTime();
    }

    void Update()
    {
        // Actualizar el temporizador
        rotationTimer += Time.deltaTime;

        // Controlar si debe cambiar entre girar o estar en pausa
        if (rotationTimer >= currentMaxTime)
        {
            isRotating = !isRotating; // Cambiar el estado (rotar/pausa)
            SetRandomTime(); // Establecer un nuevo tiempo aleatorio
            rotationTimer = 0f; // Reiniciar temporizador
        }

        // Si está en estado de rotación
        if (isRotating)
        {
            // Rotación constante en X o Z
            RotateOnXOrZ();

            // Rotación en Y (si está activada)
            if (rotateOnYAxis)
            {
                RotateOnYAxis();
            }
        }
    }

    private void RotateOnXOrZ()
    {
        if (rotateOnX)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
        }
        else if (rotateOnZ)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    private void RotateOnYAxis()
    {
        // Determinar la dirección del movimiento alternante en Y
        float direction = isYReversing ? -1f : 1f;

        // Calcular el nuevo ángulo en Y basado en la velocidad
        currentYAngle += yRotationSpeed * direction * Time.deltaTime;

        // Limitar el ángulo dentro del rango permitido
        if (currentYAngle >= maxYRotationAngle)
        {
            isYReversing = true; // Cambiar dirección
        }
        else if (currentYAngle <= -maxYRotationAngle)
        {
            isYReversing = false; // Cambiar dirección
        }

        // Aplicar la rotación alternante en Y
        transform.localRotation = Quaternion.Euler(
            transform.localRotation.eulerAngles.x,
            currentYAngle,
            transform.localRotation.eulerAngles.z
        );
    }

    private void SetRandomTime()
    {
        // Establecer un tiempo aleatorio dependiendo del estado actual
        if (isRotating)
        {
            currentMaxTime = Random.Range(minActiveTime, maxActiveTime); // Tiempo de rotación
        }
        else
        {
            currentMaxTime = Random.Range(minIdleTime, maxIdleTime); // Tiempo de pausa
        }
    }
}


