using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    [Header("Configuraci�n de Rotaci�n (X/Z)")]
    public bool rotateOnX = true; // Rotar en el eje X
    public bool rotateOnZ = false; // Rotar en el eje Z
    public float rotationSpeed = 50f; // Velocidad de rotaci�n constante en X o Z

    [Header("Rotaci�n en el Eje Y")]
    public bool rotateOnYAxis = true; // Activar/desactivar rotaci�n en el eje Y
    public float yRotationSpeed = 30f; // Velocidad de rotaci�n en el eje Y
    public float maxYRotationAngle = 45f; // �ngulo m�ximo de rotaci�n en el eje Y

    [Header("Tiempos Aleatorios")]
    public float minActiveTime = 2f; // Tiempo m�nimo de rotaci�n
    public float maxActiveTime = 5f; // Tiempo m�ximo de rotaci�n
    public float minIdleTime = 1f; // Tiempo m�nimo de pausa
    public float maxIdleTime = 3f; // Tiempo m�ximo de pausa

    private float rotationTimer = 0f; // Temporizador para controlar la rotaci�n
    private float currentMaxTime = 0f; // Tiempo actual antes de cambiar estado
    private bool isRotating = false; // Indica si est� girando o en pausa

    private float currentYAngle = 0f; // �ngulo actual de rotaci�n en Y
    private bool isYReversing = false; // Indica si la rotaci�n en Y est� invirtiendo

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

        // Si est� en estado de rotaci�n
        if (isRotating)
        {
            // Rotaci�n constante en X o Z
            RotateOnXOrZ();

            // Rotaci�n en Y (si est� activada)
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
        // Determinar la direcci�n del movimiento alternante en Y
        float direction = isYReversing ? -1f : 1f;

        // Calcular el nuevo �ngulo en Y basado en la velocidad
        currentYAngle += yRotationSpeed * direction * Time.deltaTime;

        // Limitar el �ngulo dentro del rango permitido
        if (currentYAngle >= maxYRotationAngle)
        {
            isYReversing = true; // Cambiar direcci�n
        }
        else if (currentYAngle <= -maxYRotationAngle)
        {
            isYReversing = false; // Cambiar direcci�n
        }

        // Aplicar la rotaci�n alternante en Y
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
            currentMaxTime = Random.Range(minActiveTime, maxActiveTime); // Tiempo de rotaci�n
        }
        else
        {
            currentMaxTime = Random.Range(minIdleTime, maxIdleTime); // Tiempo de pausa
        }
    }
}


