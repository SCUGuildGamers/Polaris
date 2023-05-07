using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
	public SceneController sceneController;
	public PlayerMovement playerMovement;
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape) && !playerMovement.Get_showTrajectory())
		{
			Pause();
		}
	}
	
	public void Resume ()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	void Pause ()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
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

	
	// public void LoadOptions() must be implemented, same as MainMenu Options
}
