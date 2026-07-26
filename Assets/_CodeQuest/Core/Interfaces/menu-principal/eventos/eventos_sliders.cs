using UnityEngine;
using UnityEngine.EventSystems;

public class eventos_sliders : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public efectos_sliders efectosSliders;
    public void OnPointerDown(PointerEventData evento)
    {
        efectosSliders.ActivadoSlider();
    }
    public void OnPointerUp(PointerEventData evento)
    {
        efectosSliders.DesactivadoSlider();
    }

}
