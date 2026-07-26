using UnityEngine;

public class configuracion_global : MonoBehaviour
{
    public GameObject panelPrincipal;
    public GameObject panelAjustes;

    void Start()
    {
        panelAjustes.SetActive(false);
        panelPrincipal.SetActive(true);
    }

    public void Ajustes()
    {
        panelPrincipal.SetActive(false);
        panelAjustes.SetActive(true);
    }

    public void Principal()
    {
        panelPrincipal.SetActive(true);
        panelAjustes.SetActive(false);
    }
}
