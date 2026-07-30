using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class cambio_escena_trigger : MonoBehaviour
{
    public string nombreEscena;
    //public bool cambio = false;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrando al trigger de cambio de escena");
        SceneManager.LoadScene(nombreEscena);
    }
}
