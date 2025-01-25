using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Configuración de Apertura")]
    public float openRotationAngle = 90f; // Ángulo al que se abre la puerta
    public float openSpeed = 2f; // Velocidad de apertura

    private bool isOpen = false;

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            StartCoroutine(OpenAnimation());
        }
    }

    private System.Collections.IEnumerator OpenAnimation()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, openRotationAngle, 0f);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, openSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}

