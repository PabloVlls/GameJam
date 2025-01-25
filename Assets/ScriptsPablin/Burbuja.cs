using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burbuja : MonoBehaviour
{
    public float fuerzaFlotante = 0.5f;
    public float alturaMaxima = 0.5f;
    public float velMov = 1f;

    private bool llego = false;

    public Rigidbody rbBurbuja;

    public Transform puntoFinal;

    private Vector3 posInicialY;

    void Start()
    {
        rbBurbuja = GetComponent<Rigidbody>();

        posInicialY = transform.position;
    }

    void FixedUpdate()
    {
        if (transform.position.y < alturaMaxima)
        {
            rbBurbuja.AddForce(Vector3.up * fuerzaFlotante, ForceMode.Acceleration);
        }
        else
        {
            rbBurbuja.velocity = new Vector3(rbBurbuja.velocity.x, 0, rbBurbuja.velocity.z);
        }

        if (!llego && puntoFinal != null)
        {
            Mover();
        }
    }

    void Mover()
    {
        Vector3 nuevaPosicion = Vector3.MoveTowards(transform.position, new Vector3(puntoFinal.position.x, posInicialY.y, puntoFinal.position.z), velMov * Time.deltaTime);

        transform.position = new Vector3(nuevaPosicion.x, transform.position.y, nuevaPosicion.z);

        if (Vector3.Distance(transform.position, puntoFinal.position) < 0.1f)
        {
            llego = true;
        }
    }
}
