using UnityEngine;
using System.Collections;
using TMPro;
public class Dialogue : MonoBehaviour
{
    // Este script se encarga del dialogo entre el jugador y el NPC.
    // Se activa cuando el jugador entra en el rango del NPC y se desactiva cuando el jugador sale del rango.

    // Variables

    //Esta el jugador en el rango de dialogo?
    private bool isPlayerInRange;
    //El dialogo empezo?
    private bool didDialogueStart;
    //Que linea de dialogo se esta mostrando?
    private int lineIndex;
    //Velocidad de escritura del dialogo
    private float typingTime = 0.07f;
    //Crea una referencia al objeto de interaccion del NPC
    [SerializeField] private GameObject dialogueMark;
    //Crear referencia del panel de dialogo
    [SerializeField] private GameObject dialoguePanel;
    //Crear referencia al texto del dialogo
    [SerializeField] private TMP_Text dialogueText;
    //Se guardan las lineas de dialogo del NPC
    [SerializeField, TextArea(4, 6)]private string[] dialogueLines;


    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            //Validamos que el dialogo no haya empezado para iniciar el dialogo
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
        }
    }
    //Funcion para iniciar el dialogo
    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }
    //Cambio de linea de dialogo
    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart=false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
        }
    }

    //Corrutina para mostrar el dialogo
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        //Se concatenan cada carecter uno por uno para dar el efecto de escritura
        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            // Activar el dialogo
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
            // Desactivar el dialogo
        }
    }
}
