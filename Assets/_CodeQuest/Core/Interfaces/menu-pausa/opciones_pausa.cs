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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("estoy detectando la tecla escape");

            if (!juegoPausado)
            {
                Debug.Log("estoy pausando el juego");
                Pausa();
            }
            else
            {
                Debug.Log("estoy reanudando el juego");
                Reanudar();
            }
        }
    }

    private void Pausa()
    {
        Debug.Log("estoy en pausa");

        Debug.Log(menuPausa);

        menuPausa.SetActive(true);
        opcionesPausa.SetActive(true);
        opcionesAjustes.SetActive(false);

        juegoPausado = true;

        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Reanudar()
    {

        Debug.Log("estoy reanudando el juego");
        juegoPausado = false;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        Time.timeScale = 1f;

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
        SceneManager.LoadScene("menu-principal");
    }
}
