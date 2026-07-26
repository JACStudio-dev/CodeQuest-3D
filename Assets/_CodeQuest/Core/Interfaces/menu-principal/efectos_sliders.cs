using UnityEngine;
using UnityEngine.EventSystems;

public class efectos_sliders : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip efectoSlider;

    public void ActivadoSlider( )
    {
        Debug.Log("reproduciendo sonido Sliders");

        audioSource.clip = efectoSlider;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void DesactivadoSlider()
    {
        Debug.Log("deteniendo sonido Sliders");

        audioSource.Stop();
        audioSource.loop = false;
    }
}
