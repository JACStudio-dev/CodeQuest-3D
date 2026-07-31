using UnityEngine;
using UnityEngine.SceneManagement;

public class opciones_pausa : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject opcionesPausa;
    public GameObject opcionesAjustes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
            Debug.Log("estoy pausando el juego");
            Pausa();
        }
    }

    private void Pausa()
    {
        menuPausa.SetActive(true);
        opcionesPausa.SetActive(true);
    }

    public void Reanudar()
    {
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
