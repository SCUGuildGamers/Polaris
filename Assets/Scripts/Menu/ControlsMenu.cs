using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public GameObject controlMenuUI;
    public GameObject inGameMenuUI;

    // Close the controls menu
    public void CloseMenu() {
        controlMenuUI.SetActive(false);
        inGameMenuUI.SetActive(true);
    }

    public void ShowMenu()
    {
        controlMenuUI.SetActive(true);
        inGameMenuUI.SetActive(false);
    }
}
