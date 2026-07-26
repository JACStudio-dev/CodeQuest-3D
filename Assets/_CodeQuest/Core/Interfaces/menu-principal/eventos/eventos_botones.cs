using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class eventos_botones : MonoBehaviour, IPointerEnterHandler
{
    public efectos_botones efectos_botones;
    public void OnPointerEnter(PointerEventData evento)
    {
        efectos_botones.SobreBoton();
    }
}
