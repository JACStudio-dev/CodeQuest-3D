using BrewedInk.CRT;
using UnityEngine;

public class MainMenuPanelSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject panelMenu;
    [SerializeField]
    private GameObject panelCustom;

    [SerializeField]
    private GameObject CharacterEnviroment;

    [SerializeField]
    private GameObject MainCamera;


    private void Awake()
    {
        panelMenu.SetActive(true);
        panelCustom.SetActive(false);
        CharacterEnviroment.SetActive(false);
    }

    public void ChangeCustomPanel() { 
        panelMenu.SetActive(false);
        panelCustom.SetActive(true);
        CharacterEnviroment.SetActive(true);
        MainCamera.GetComponent<CRTCameraBehaviour>().enabled = false;
    }

    public void ChangeMenuPanel()
    {
        panelMenu.SetActive(true);
        panelCustom.SetActive(false);
        CharacterEnviroment.SetActive(false);
        MainCamera.GetComponent<CRTCameraBehaviour>().enabled = true;
    }
}
