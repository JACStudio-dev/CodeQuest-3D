using UnityEngine;

public class animacion_personaje : MonoBehaviour
{
    public float velocidadCorrer = 7;
    public float velocidadCaminar = 3;
    public float velocidadRotacion = 100;


    public Animation animacion;

    private float ejeX, ejeY;

    // Update is called once per frame
    void Update()
    {
        ejeX = Input.GetAxis("Horizontal");
        ejeY = Input.GetAxis("Vertical");
    }
}
