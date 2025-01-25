using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    [Header("Configuración del movimiento")]
    public bool moveOnX = true; // Mover en eje X (true) o Z (false)
    public float movementRange = 5f; // Límite de movimiento en el eje seleccionado

    private Camera mainCamera;
    private Vector3 initialPosition;
    private bool isDragging = false;

    void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position; // Guardar la posición inicial
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        // Obtener la posición del mouse en el espacio de la pantalla
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, initialPosition); // Plano de referencia en el suelo
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);

            // Movimiento restringido según el eje seleccionado
            if (moveOnX)
            {
                float newX = Mathf.Clamp(hitPoint.x, initialPosition.x - movementRange, initialPosition.x + movementRange);
                transform.position = new Vector3(newX, initialPosition.y, initialPosition.z);
            }
            else
            {
                float newZ = Mathf.Clamp(hitPoint.z, initialPosition.z - movementRange, initialPosition.z + movementRange);
                transform.position = new Vector3(initialPosition.x, initialPosition.y, newZ);
            }
        }
    }
}
