using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class opciones_menu_principal : MonoBehaviour
{
    public efectos_botones efectosBotones;
    public configuracion_global configuracion_Global;

    public void Play()
    {
        Debug.Log("Entrando a la escena principal");
        //efectosBotones.BotonClickeado();
        int EscenaActual = SceneManager.GetActiveScene().buildIndex;
        int SiguienteEscena = EscenaActual + 1;
        Debug.Log($"Escena actual: {EscenaActual}, Siguiente escena: {SiguienteEscena}");
        if (SiguienteEscena < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("estoy por hacer el cambio de escenas");
            SceneManager.LoadScene(SiguienteEscena);
        }
        else
        {
            Debug.LogWarning("No hay más escenas en la construcción del juego.");
        }
        Debug.Log("No paso nada, entonces hay algo que falta revisar");
    }

    public void EntrarAjustes()
    {
        Debug.Log("Entrando a ajustes");
        //efectosBotones.BotonClickeado();
        configuracion_Global.Ajustes();
    }

    public void CreditosJuego()
    {
        Debug.Log("Entrando a creditos");
        //efectosBotones.BotonClickeado();
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego");
        //efectosBotones.BotonClickeado();

        #if UNITY_EDITOR
        // Dentro del Editor, Application.Quit() no hace nada.
        // Esto detiene el modo Play para simular el "cierre" del juego.
        EditorApplication.isPlaying = false;
        #else
        // En una build real (Windows, Android, etc), esto sí cierra la aplicación.
        Application.Quit();
        #endif
    }
}
