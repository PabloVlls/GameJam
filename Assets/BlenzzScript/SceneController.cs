using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Funciones para cambiar escenas
    public void LoadInicio()
    {
        SceneManager.LoadScene("inicio");
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

    public void LoadPantNvles()
    {
        SceneManager.LoadScene("zonadeniveles");
    }

    // Funci�n para cerrar el juego
    public void QuitGame()
    {
        Debug.Log("Cerrando el juego..."); // Mensaje para depuraci�n (visible solo en el editor)
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
}

