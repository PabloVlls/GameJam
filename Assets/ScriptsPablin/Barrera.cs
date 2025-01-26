using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrera : MonoBehaviour
{
    public Collider colBarrera;
    public Vector3 direccion = Vector3.right; 
    public float fuerzaViento = 10f;
    private CorrienteAire corrienteAire = null;
    public Rigidbody rbBubly;

    private void FixedUpdate()
    {
        if (corrienteAire != null)
        {
            AplicarAire();
        }
    }

    void AplicarAire()
    {
        rbBubly.AddForce(corrienteAire.direccion.normalized * corrienteAire.fuerzaViento, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Burbuja"))
        {
            corrienteAire = other.GetComponent<CorrienteAire>();
            Debug.Log("buenas");
        }
    }
}
