using UnityEngine;

public class VentDuct : MonoBehaviour
{
    [Header("Configuraci�n de movimiento")]
    public Vector3 suctionDirection = Vector3.forward; // Direcci�n configurable (Z por defecto)
    public float suctionSpeed = 5f; // Velocidad de movimiento

    [Header("Da�o al permanecer en el trigger")]
    public float damageAmount = 100f; // Cantidad de da�o
    public float damageDelay = 2f; // Tiempo necesario en el trigger para aplicar da�o

    [Header("Estado del ducto")]
    public bool isBlocked = false; // Si est� bloqueado por un objeto con tag "Barrier"

    private float timeInTrigger = 0f; // Tiempo que la burbuja lleva dentro del trigger
    private GameObject player; // Referencia al jugador (burbuja)
    private bool isDealingDamage = false; // Controla si ya est� infligiendo da�o

    private void Update()
    {
        // Si no hay jugador en el trigger o el ducto est� bloqueado, no hacer nada
        if (player == null || isBlocked) return;

        // Mover la burbuja en la direcci�n configurada
        player.transform.Translate(suctionDirection.normalized * suctionSpeed * Time.deltaTime, Space.World);

        // Incrementar el tiempo que lleva en el trigger
        timeInTrigger += Time.deltaTime;

        // Si el tiempo supera el l�mite y no se ha aplicado da�o a�n
        if (timeInTrigger >= damageDelay && !isDealingDamage)
        {
            isDealingDamage = true; // Asegurarse de que solo se aplique una vez
            DealDamageToPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Se ha detectado un objeto: " + other.gameObject.name + " con tag: " + other.tag);

        // Si el objeto es la burbuja (puedes verificarlo con un tag o un componente)
        if (other.CompareTag("Player"))
        {
            player = other.gameObject; // Guardar referencia al jugador
            timeInTrigger = 0f; // Reiniciar el contador
            isDealingDamage = false; // Asegurarse de que pueda volver a infligir da�o
        }

        // Si el objeto es un "Barrier", bloquear el ducto
        if (other.CompareTag("Barrier"))
        {
            Debug.Log("Blokeadooo");
            isBlocked = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si la burbuja sale del trigger, detener el movimiento y resetear el da�o
        if (other.CompareTag("Player"))
        {
            player = null;
            timeInTrigger = 0f; // Reiniciar el tiempo en el trigger
        }

        // Si un "Barrier" sale del trigger, desbloquear el ducto
        if (other.CompareTag("Barrier"))
        {
            isBlocked = false;
        }
    }

    private void DealDamageToPlayer()
    {
        // Buscar un componente de salud en el jugador (puedes ajustar esto seg�n tu sistema de vida)
        BubbleHealth playerHealth = player.GetComponent<BubbleHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount); // Infligir da�o
            Debug.Log("Burbuja da�ada por ducto: " + damageAmount);
        }
    }
}

