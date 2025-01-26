using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [Header("Configuración del globo")]
    public float floatSpeed = 2f; // Velocidad a la que flota hacia arriba
    public float destroyHeight = 10f; // Altura máxima antes de que el globo desaparezca
    public float destroyDelay = 5f; // Tiempo de retraso antes de destruir si el globo está flotando

    private bool isFloating = false;
    private bool isPressed = false;

    void OnMouseDown()
    {
        // Marca que el globo fue presionado
        isPressed = true;
        isFloating = true;

        // Destruir después de un tiempo si está flotando
        Invoke(nameof(DestroyBalloon), destroyDelay);
    }

    void Update()
    {
        if (isFloating)
        {
            // Movimiento hacia arriba
            transform.position += Vector3.up * floatSpeed * Time.deltaTime;

            // Destruir si alcanza la altura máxima
            if (transform.position.y >= destroyHeight)
            {
                DestroyBalloon();
            }
        }

        float swayAmount = 0.1f * Mathf.Sin(Time.time * 2f); // Oscilación suave
        transform.position += new Vector3(swayAmount, 0, 0) * Time.deltaTime;

    }

    private void DestroyBalloon()
    {
        if (isPressed) // Solo destruir si fue presionado
        {
            Destroy(gameObject);
        }
    }
}



