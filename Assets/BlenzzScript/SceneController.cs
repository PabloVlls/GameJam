using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Funciones para cambiar escenas
    public void LoadInicio()
    {
        SceneManager.LoadScene("inicio");
    }

    public void LoadCreditos()
    {
        SceneManager.LoadScene("creditos");
    }

    public void LoadHistoria()
    {
        SceneManager.LoadScene("historia");
    }

    public void LoadTutorial1()
    {
        SceneManager.LoadScene("tutorial1");
    }

    public void LoadTutorial2()
    {
        SceneManager.LoadScene("tutorial2");
    }

    public void LoadTutorial3()
    {
        SceneManager.LoadScene("tutorial3");
    }

    public void LoadTutorial4()
    {
        SceneManager.LoadScene("tutorial4");
    }

    public void LoadZonaDeNiveles()
    {
        SceneManager.LoadScene("zonadeniveles");
    }

    public void LoadNivel1()
    {
        SceneManager.LoadScene("nivel1");
    }

    public void LoadNivel2()
    {
        SceneManager.LoadScene("nivel2");
    }

    public void LoadNivel3()
    {
        SceneManager.LoadScene("nivel3");
    }

    // Función para cerrar el juego
    public void QuitGame()
    {
        Debug.Log("Cerrando el juego..."); // Mensaje para depuración (visible solo en el editor)
        Application.Quit(); // Cierra la aplicación
    }
}

