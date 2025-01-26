using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPost : MonoBehaviour
{
    [Header("Configuración de Luz")]
    public Light pointLight; // La luz del poste
    public GameObject mirrorSpotLight; // La Spot Light del espejo
    public float lightDuration = 4f; // Duración de la luz encendida

    private bool isActive = false;

    private void OnMouseDown()
    {
        if (!isActive)
        {
            StartCoroutine(ActivateLight());
        }
    }

    private System.Collections.IEnumerator ActivateLight()
    {
        isActive = true;

        // Encender la luz del poste y la del espejo
        pointLight.enabled = true;
        mirrorSpotLight.SetActive(true);

        yield return new WaitForSeconds(lightDuration);

        // Apagar ambas luces
        pointLight.enabled = false;
        mirrorSpotLight.SetActive(false);

        isActive = false;
    }
}

