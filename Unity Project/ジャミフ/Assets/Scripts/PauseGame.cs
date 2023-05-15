using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
	public bool gameIsPaused = false;
	public GameObject PauseMenuUI;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (gameIsPaused)
			{
				StartTheGame();
			}
			else
			{
				PauseTheGame();
			}
		}
	}


	public void PauseTheGame()
	{
		Time.timeScale = 0;
		gameIsPaused = true;
		Debug.Log("Paused Game");
		PauseMenuUI.SetActive(true);
	}

	public void StartTheGame()
	{
		Time.timeScale = 1;
		gameIsPaused = false;
		Debug.Log("Started Game");
		PauseMenuUI.SetActive(false);

	}
}
