using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public GameObject controlMenuUI;
    public GameObject inGameMenuUI;

    private PlayerMovement playerMovement;
    private PlayerSwing playerSwing;

    public static bool isPopup = false;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerSwing = FindObjectOfType<PlayerSwing>();
    }

    void Update()
    {
        if (isPopup)
        {
            playerMovement.CanPlayerMove = false;
            playerMovement.CanPlayerGlide = false;
            playerSwing.CanPlayerSwing = false;
        }
    }

    // Close the controls menu
    public void CloseMenu() 
    {
        controlMenuUI.SetActive(false);

        if (!isPopup)
            inGameMenuUI.SetActive(true);
        else
        {
            isPopup = false;
            playerMovement.CanPlayerMove = true;
            playerMovement.CanPlayerGlide = true;
            playerSwing.CanPlayerSwing = true;
        }
    }

    public void ShowMenu()
    {
        controlMenuUI.SetActive(true);
        inGameMenuUI.SetActive(false);
    }

    public void setPopup(bool value)
    {
        isPopup = value;
    }
}
