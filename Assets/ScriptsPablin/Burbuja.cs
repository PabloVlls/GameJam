using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burbuja : MonoBehaviour
{
    public float fuerzaFlotante = 0.5f;
    public float alturaMaxima = 0.5f;
    public float velocidadMovimiento = 0.5f;
    public float velocidadMaxima = 5f;

    public Rigidbody rbBurbuja;

    private CorrienteAire corrienteAire = null;
    private Vector3 direccionMovimiento;

    void Start()
    {
        rbBurbuja = GetComponent<Rigidbody>();
        rbBurbuja.useGravity = false;
    }

    void Update()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoZ = Input.GetAxis("Vertical");

        direccionMovimiento = new Vector3(movimientoX, 0, movimientoZ).normalized;
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

        if(direccionMovimiento.magnitude > 0)
        {
            rbBurbuja.AddForce(direccionMovimiento * velocidadMovimiento, ForceMode.Acceleration);
        }

        // Aplicar la corriente de aire si hay una corriente activa
        if (corrienteAire != null)
        {
            AplicarCorrienteAire();
        }

        Vector3 velocidadActual = rbBurbuja.velocity;
        velocidadActual.x = Mathf.Clamp(velocidadActual.x, -velocidadMaxima, velocidadMaxima);
        velocidadActual.z = Mathf.Clamp(velocidadActual.z, -velocidadMaxima, velocidadMaxima);
        rbBurbuja.velocity = new Vector3(velocidadActual.x, rbBurbuja.velocity.y, velocidadActual.z);
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
            Debug.Log("Burbuja entro en la zona de viento");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Viento"))
        {
            // Aqui no detenemos el movimiento, solo quitamos la corriente de aire actual
            corrienteAire = null;
            Debug.Log("Burbuja salio de la zona de viento");
        }
    }
}
