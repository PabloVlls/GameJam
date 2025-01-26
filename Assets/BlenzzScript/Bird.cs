using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Configuraci�n de Movimiento")]
    public float normalSpeed = 5f; // Velocidad inicial del p�jaro
    public float scaredSpeedMultiplier = 2f; // Multiplicador de velocidad al ser asustado
    public float targetX = 20f; // Coordenada X de destino

    [Header("Efecto de Vuelo")]
    public float flapAmplitude = 0.5f; // Amplitud del movimiento de "aleteo"
    public float flapFrequency = 2f; // Frecuencia del movimiento de "aleteo"

    private float currentSpeed; // Velocidad actual del p�jaro
    private Vector3 direction; // Direcci�n del movimiento
    private bool isScared = false; // Indica si el p�jaro est� asustado

    void Start()
    {
        // Calcular direcci�n inicial hacia el destino
        direction = (new Vector3(transform.position.x, transform.position.y, targetX ) - transform.position).normalized;
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        // Movimiento hacia el destino con efecto de vuelo
        Fly();

        // Destruir el p�jaro si alcanza el destino en X
        if ((targetX > 0 && transform.position.z >= targetX) || (targetX < 0 && transform.position.z <= targetX))
        {
            Destroy(gameObject);
        }
    }

    private void Fly()
    {
        // Movimiento recto hacia el objetivo
        transform.position += direction * currentSpeed * Time.deltaTime;

        // Aleteo (movimiento sinusoidal en Y para simular vuelo)
        float flapOffset = Mathf.Sin(Time.time * flapFrequency) * flapAmplitude;
        transform.position += new Vector3(0, flapOffset * Time.deltaTime, 0);
    }

    void OnMouseDown()
    {
        if (!isScared)
        {
            // El p�jaro es asustado
            isScared = true;

            // Cambiar direcci�n 45� a la izquierda o derecha
            float angle = Random.value > 0.5f ? 45f : -45f; // Elegir aleatoriamente entre izquierda o derecha
            direction = Quaternion.Euler(0, angle, 0) * direction;

            // Aumentar velocidad
            currentSpeed *= scaredSpeedMultiplier;

            Debug.Log("�P�jaro asustado! Direcci�n cambiada y velocidad aumentada.");
        }
    }
}


