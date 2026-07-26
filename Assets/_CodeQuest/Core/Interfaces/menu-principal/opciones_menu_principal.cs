using UnityEngine;
using UnityEngine.SceneManagement;

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
        efectosBotones.BotonClickeado();
        configuracion_Global.Ajustes();
    }

    public void CreditosJuego()
    {
        Debug.Log("Entrando a creditos");
        efectosBotones.BotonClickeado();
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego");
        efectosBotones.BotonClickeado();
        Application.Quit();
    }
}
