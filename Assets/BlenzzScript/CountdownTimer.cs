using UnityEngine;
using UnityEngine.UI; // Para manejar elementos del Canvas como Text

public class CountdownTimer : MonoBehaviour
{
    [Header("Tiempo del contador")]
    public float maxTime = 60f; // Tiempo m�ximo en segundos (modificable en el inspector)
    private float currentTime; // Tiempo actual

    [Header("Referencias UI")]
    public Text timerText; // Texto en el Canvas para mostrar el tiempo

    [Header("Acci�n al finalizar")]
    public GameObject objectToActivate; // Objeto a activar cuando el tiempo llegue a 0

    [Header("Colores del texto")]
    public Color startColor = Color.white; // Color inicial (blanco)
    public Color endColor = Color.red;    // Color final (rojo)

    [Header("Estado del jugador")]
    public bool isPlayerDead = false; // Controla si el jugador ha muerto

    private bool isTimeOver = false; // Para asegurarnos de que la acci�n ocurra solo una vez

    void Start()
    {
        // Inicializar el tiempo actual al tiempo m�ximo
        currentTime = maxTime;

        //Debug.Log("Conteo");

        // Asegurarse de que el objeto a activar est� desactivado al inicio
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    void Update()
    {
        // Si el tiempo se acab� o el jugador est� muerto, no hacer nada m�s
        if (isTimeOver || isPlayerDead) return;

        // Reducir el tiempo actual
        currentTime -= Time.deltaTime;

        // Asegurarse de que no sea menor que 0
        if (currentTime <= 0)
        {
            currentTime = 0;
            EndTimer(); // Llamar a la acci�n cuando el tiempo llegue a 0
        }

        // Actualizar el texto del contador en el Canvas
        UpdateTimerText();

        // Actualizar el color del texto
        UpdateTimerColor();

        //Debug.Log("Estoy contando");
    }

    void UpdateTimerText()
    {
        // Convertir el tiempo actual a un formato de minutos y segundos
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // Mostrar el tiempo en el texto
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndTimer()
    {
        isTimeOver = true; // Asegurar que no se active varias veces

        // Activar el objeto designado
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        // Puedes agregar m�s l�gica aqu�, como mostrar un mensaje o finalizar el juego
        //Debug.Log("�El tiempo se ha acabado!");
    }

    void UpdateTimerColor()
    {
        // Interpolar el color entre blanco y rojo en funci�n del porcentaje de tiempo restante
        float t = 1f - (currentTime / maxTime); // Progresi�n inversa (de 1 a 0)
        timerText.color = Color.Lerp(startColor, endColor, t);
    }
}


