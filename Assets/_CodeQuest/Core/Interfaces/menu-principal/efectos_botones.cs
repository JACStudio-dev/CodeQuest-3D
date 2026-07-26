using UnityEngine;
using UnityEngine.EventSystems;

public class efectos_botones : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip efecttoEntrar;
    public AudioClip efecttoHover;

    //Efectos de sonido
    public void BotonClickeado()
    {
        Debug.Log("reproduciendo sonido Entrar");
        audioSource.PlayOneShot(efecttoEntrar);
    }

    public void SobreBoton()
    {
        Debug.Log("reproduciendo sonido Hover");
        audioSource.PlayOneShot(efecttoHover);
    }
}