using UnityEngine;
using UnityEngine.UI;

public class opciones_menu_ajustes : MonoBehaviour
{
    public configuracion_global configuracion_Global;
    public efectos_botones efectosBotones;
    public efectos_sliders efectosSliders;

    public Slider sliderVolumen;
    public Slider sliderBrillo;

    public void Volumen()
    {
        Debug.Log("configurando volumen");
        Debug.Log("Volumen actual: " + sliderVolumen.value);
    }

    public void Brillo()
    {
        Debug.Log("configurando brillo");
        Debug.Log("Brillo actual: " + sliderBrillo.value);
    }

    public void Idiomas()
    {
        Debug.Log("configurando idiomas");
        efectosBotones.BotonClickeado();
    }
    public void VolverMenuPrincipal()
    {
        Debug.Log("Volviendo al menu principal");
        efectosBotones.BotonClickeado();
        configuracion_Global.Principal();
    }
}
