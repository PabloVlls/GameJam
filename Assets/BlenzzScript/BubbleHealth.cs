using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Para usar el Slider

public class BubbleHealth : MonoBehaviour
{
    [Header("Vida de la burbuja")]
    public float maxHealth = 100f; // Vida m�xima de la burbuja
    public float health;          // Vida actual
    public float lifeDecayRate = 1f; // Cu�nta vida pierde por segundo autom�ticamente

    [Header("Referencias UI")]
    public Slider healthSlider; // Slider para mostrar la vida

    [Header("Da�o por colisi�n")]
    public float tier1Damage = 5f;
    public float tier2Damage = 10f;
    public float tier3Damage = 20f;

    [Header("Acci�n al finalizar")]
    public GameObject objectToActivate; // Objeto a activar cuando el tiempo llegue a 0

    private bool canTakeDamage = true; // Controla si puede recibir da�o

    void Start()
    {
        // Inicializar la vida al m�ximo y configurar el slider
        health = maxHealth;

        //Debug.Log("Vida"); 

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }
    }

    void Update()
    {
        // Reducir vida autom�ticamente con el tiempo
        health -= lifeDecayRate * Time.deltaTime;
        health = Mathf.Clamp(health, 0, maxHealth); // Asegurarse de que no pase de los l�mites

        // Actualizar el slider
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }

        // Verificar si la burbuja muere
        if (health <= 0)
        {
            Die();
        }

        //Debug.Log("Descuento Vida");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canTakeDamage) return; // Si no puede recibir da�o, salir

        // Detectar el tag del objeto con el que colision�
        switch (collision.gameObject.tag)
        {
            case "tier1":
                TakeDamage(tier1Damage);
                break;
            case "tier2":
                TakeDamage(tier2Damage);
                break;
            case "tier3":
                TakeDamage(tier3Damage);
                break;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth); // Mantener la vida en rango
        if (healthSlider != null)
        {
            healthSlider.value = health;
        }

        // Iniciar el cooldown de da�o
        StartCoroutine(DamageCooldown());
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1f); // Esperar 1 segundo
        canTakeDamage = true;
    }

    void Die()
    {
        // L�gica para cuando la burbuja muere (desactivar objeto, reiniciar nivel, etc.)
        //Debug.Log("La burbuja ha muerto.");
        Destroy(gameObject); // Destruye el objeto de la burbuja
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        FindObjectOfType<CountdownTimer>().isPlayerDead = true;
        //Debug.Log("El jugador ha muerto. Contador detenido.");
    }
}
