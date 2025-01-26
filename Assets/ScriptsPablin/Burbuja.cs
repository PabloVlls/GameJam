using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burbuja : MonoBehaviour
{
    public float fuerzaFlotante = 0.5f;
    public float alturaMaxima = 0.5f;

    public Rigidbody rbBurbuja;

    [SerializeField] private bool llego = false;

    private CorrienteAire corrienteAire = null;

    void Start()
    {
        rbBurbuja = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Aplicar la fuerza flotante
        if (transform.position.y < alturaMaxima)
        {
            rbBurbuja.AddForce(Vector3.up * fuerzaFlotante, ForceMode.Acceleration);
        }
        else
        {
            rbBurbuja.velocity = new Vector3(rbBurbuja.velocity.x, 0, rbBurbuja.velocity.z);
        }

        // Aplicar la corriente de aire si hay una corriente activa
        if (corrienteAire != null)
        {
            AplicarCorrienteAire();
        }
    }

    void AplicarCorrienteAire()
    {
        // Aplicar la fuerza de la corriente de aire
        rbBurbuja.AddForce(corrienteAire.direccion.normalized * corrienteAire.fuerzaViento, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Viento"))
        {
            corrienteAire = other.GetComponent<CorrienteAire>();
            Debug.Log("Burbuja entró en la zona de viento");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Viento"))
        {
            // Aquí no detenemos el movimiento, solo quitamos la corriente de aire actual
            corrienteAire = null;
            Debug.Log("Burbuja salió de la zona de viento");
        }
    }
}
