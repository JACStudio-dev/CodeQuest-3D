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
            if (juegoPausado)
            {
                Debug.Log("estoy reanudando el juego");
                Reanudar();
            }
            else
            {
                Debug.Log("estoy pausando el juego");
                Pausa();
            }
        }
    }

    private void Pausa()
    {
        juegoPausado = true;

        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        menuPausa.SetActive(true);
        opcionesPausa.SetActive(true);
    }

    public void Reanudar()
    {
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
