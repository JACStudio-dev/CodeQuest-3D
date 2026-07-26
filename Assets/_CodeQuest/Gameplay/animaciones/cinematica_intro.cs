using UnityEngine;
using UnityEngine.Playables;

public class cinematica_intro : MonoBehaviour
{
    public PlayableDirector cinematica;
    public float tiempoCinematica = 10f;
    float tiempo = 0;

    public GameObject objetoAnimacion;
    public GameObject objetoJugable;

    void Start()
    {
        tiempoCinematica = (float)cinematica.duration;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo >= tiempoCinematica)
        {
            objetoAnimacion.SetActive(false);
            objetoJugable.SetActive(true);
        }
    }
}
