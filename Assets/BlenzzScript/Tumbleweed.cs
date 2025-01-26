using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbleweed : MonoBehaviour
{
    [Header("Configuraci�n de Movimiento")]
    public float normalSpeed = 3f; // Velocidad inicial del arbusto
    public float scaredSpeedMultiplier = 1.5f; // Multiplicador de velocidad al ser clicado
    public float targetX = 20f; // Coordenada X de destino

    [Header("Rotaci�n del Arbusto")]
    public float tumbleSpeed = 200f; // Velocidad de rotaci�n en direcci�n de movimiento

    [Header("Ajuste de Direcci�n al Hacer Clic")]
    public float clickAngleChange = 20f; // �ngulo de cambio al hacer clic (hacia izquierda o derecha)

    private float currentSpeed; // Velocidad actual del arbusto
    private Vector3 direction; // Direcci�n del movimiento
    private bool isScared = false; // Indica si el arbusto ha sido clicado

    void Start()
    {
        // Calcular direcci�n inicial hacia el destino
        direction = (new Vector3(targetX, transform.position.y, transform.position.z) - transform.position).normalized;
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        // Movimiento hacia el destino con rotaci�n
        Roll();

        // Destruir el arbusto si alcanza el destino en X
        if ((targetX > 0 && transform.position.x >= targetX) || (targetX < 0 && transform.position.x <= targetX))
        {
            Destroy(gameObject);
        }
    }

    private void Roll()
    {
        // Movimiento recto hacia el objetivo
        transform.position += direction * currentSpeed * Time.deltaTime;

        // Rotaci�n en direcci�n del movimiento
        transform.Rotate(Vector3.forward, tumbleSpeed * Time.deltaTime);
    }

    void OnMouseDown()
    {
        if (!isScared)
        {
            // El arbusto cambia de direcci�n al hacer clic
            isScared = true;

            // Cambiar direcci�n aleatoriamente hacia izquierda o derecha (eje Y)
            float angle = Random.value > 0.5f ? clickAngleChange : -clickAngleChange;
            direction = Quaternion.Euler(0, angle, 0) * direction; // Rotaci�n en el plano X-Z

            // Aumentar velocidad
            currentSpeed *= scaredSpeedMultiplier;

            Debug.Log("�Arbusto clicado! Direcci�n cambiada y velocidad aumentada.");
        }
    }
}


