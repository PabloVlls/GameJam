using System.Collections;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Vector3 startPoint; // Punto inicial
    public Vector3 endPoint;   // Punto final
    public float speed = 2f;   // Velocidad de movimiento
    public ParticleSystem diggingEffect; // Partículas para el efecto de salir/hundirse

    private bool isClicked = false; // Indica si la serpiente ha sido clicada

    private void Start()
    {
        transform.position = startPoint; // Comienza en el punto inicial
        PlayDiggingEffect(); // Efecto al salir de la tierra
    }

    private void Update()
    {
        if (!isClicked) // Si no se ha clicado, continúa moviéndose
        {
            MoveSnake();
        }
    }

    private void MoveSnake()
    {
        // Mover hacia el punto final
        transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);

        // Verificar si llegó al destino
        if (Vector3.Distance(transform.position, endPoint) < 0.1f)
        {
            StartCoroutine(SinkAndDestroy());
        }
    }

    private void OnMouseDown()
    {
        if (!isClicked) // Detectar clic
        {
            isClicked = true;
            StartCoroutine(SinkAndDestroy()); // Hacer que la serpiente se hunda
        }
    }

    private IEnumerator SinkAndDestroy()
    {
        PlayDiggingEffect(); // Efecto de hundirse
        yield return new WaitForSeconds(0.5f); // Esperar un poco para que el efecto sea visible
        Destroy(gameObject); // Destruir la serpiente
    }

    private void PlayDiggingEffect()
    {
        if (diggingEffect != null)
        {
            ParticleSystem effect = Instantiate(diggingEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration); // Destruir las partículas después de que terminen
        }
    }
}

