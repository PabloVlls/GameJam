using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Funciones para cambiar escenas
    public void LoadInicio()
    {
        CambiarEscena("inicio");
    }

    public void LoadNivel1()
    {
        CambiarEscena("nivel1");
    }

    public void LoadNivel2()
    {
        CambiarEscena("nivel2");
    }

    public void LoadNivel3()
    {
        CambiarEscena("nivel3");
    }

    public void LoadPantNvles()
    {
        CambiarEscena("zonadeniveles");
    }

    // Funci�n para cerrar el juego
    public void QuitGame()
    {
        //Debug.Log("Cerrando el juego..."); // Mensaje para depuraci�n (visible solo en el editor)
        Application.Quit(); // Cierra la aplicaci�n
    }

    public void Pausar()
    {
        Time.timeScale = 0f;
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator CargarEscena(string nombreEscena)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);
        while(!operacion.isDone)
        {
            yield return null;
        }
        Debug.Log("Escena cargada");
    }

    public void CambiarEscena(string nombre)
    {
        StartCoroutine(CargarEscena(nombre));
    }
}

