using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public static bool CanPause = false;
	public GameObject pauseMenuUI;
	public GameObject optionsMenuUI;
	public GameObject controlsMenuUI;

	// For reference
	private SceneController sceneController;
	private PlayerMovement playerMovement;
	private ControlsMenu controlsMenu;
	private ChoiceButtonManager[] choiceButtonManager;
	private DialogueManager dialogueManager;

    private void Start()
    {
		// Instantiate references
		sceneController = FindObjectOfType<SceneController>();
		playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
	{
		// If no menu is open
		if (!pauseMenuUI.activeSelf && !optionsMenuUI.activeSelf && !controlsMenuUI.activeSelf)
		{
			if (Input.GetKeyDown(KeyCode.Escape) && CanPause)
			{
				Pause();
			}
		}
		// If Options Menu is open
		else if (optionsMenuUI.activeSelf)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				optionsMenuUI.SetActive(false);
				pauseMenuUI.SetActive(true);
			}
		}
		// If Controls Menu is open
		else if (controlsMenuUI.activeSelf) {
			if (Input.GetKeyDown(KeyCode.Escape)) 
			{
				controlsMenu = FindObjectOfType<ControlsMenu>();
				controlsMenu.CloseMenu();
			}
		}
		// If Pause Menu is open
		else
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Resume();
			}
		}
		
	}

	public void Resume (){
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;

		ChoiceButtonCheck(!GameIsPaused);
	}

	void Pause (){
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;

		ChoiceButtonCheck(!GameIsPaused);
	}
	
	public void LoadMenu()
	{
		Resume();
		SceneManager.LoadScene("TitleProduction");
	}

	public void Restart()
	{
		sceneController.Reload();
		Resume();
	}

	public void SetCanPause(bool isActive)
    {
		CanPause = isActive;
    }

	private void ChoiceButtonCheck(bool value)
    {
		if (dialogueManager = FindObjectOfType<DialogueManager>())
		{
			if (dialogueManager.GetChoosing())
			{
				//Hide clone choice buttons
				choiceButtonManager = FindObjectsOfType<ChoiceButtonManager>();
				for (int i = 0; i < choiceButtonManager.Length; i++)
				{
					if (choiceButtonManager[i].flag != "original")
						choiceButtonManager[i].SetVisibility(value);
				}
			}
		}
	}


	// public void LoadOptions() must be implemented, same as MainMenu Options
}
