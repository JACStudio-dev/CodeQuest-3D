using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            if (results.Count > 0)
            {
                Debug.Log($"<color=green>[UI Hit]</color> El mouse tocó: <b>{results[0].gameObject.name}</b>");
            }
            else
            {
                Debug.Log("<color=yellow>[UI Miss]</color> El mouse no tocó ningún elemento de la UI.");
            }
        }
    }
}
