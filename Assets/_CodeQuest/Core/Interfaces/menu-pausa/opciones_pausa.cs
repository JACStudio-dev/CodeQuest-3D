using UnityEngine;
using UnityEngine.SceneManagement;

public class opciones_pausa : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject opcionesPausa;
    public GameObject opcionesAjustes;

    private bool juegoPausado = false;

    void Start()
    {
        Debug.Log("estoy iniciando el menu de pausa");
        menuPausa.SetActive(false);
        opcionesPausa.SetActive(false);
        opcionesAjustes.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }

    private void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f;

        // Liberar y mostrar el cursor correctamente
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuPausa.SetActive(true);
        opcionesPausa.SetActive(true);
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;

        // Si tu juego requiere bloquear el mouse al jugar:
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        menuPausa.SetActive(false);
        opcionesPausa.SetActive(false);
        opcionesAjustes.SetActive(false);
    }

    public void Opciones()
    {
        opcionesPausa.SetActive(false);
        opcionesAjustes.SetActive(true);
    }

    public void Volver()
    {
        opcionesAjustes.SetActive(false);
        opcionesPausa.SetActive(true);
    }

    public void MenuPrincipal()
    {
        Time.timeScale = 1f; // IMPORTANTE: siempre restaurar el tiempo antes de cambiar de escena
        SceneManager.LoadScene("menu-principal");
    }
}
