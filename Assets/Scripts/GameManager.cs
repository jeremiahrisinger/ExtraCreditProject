using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	bool gameHasEnded = false;
	public float pause;
	public GameObject redWins;
	public GameObject blueWins;
	public GameObject score;

	public void EndGame()
	{
		if (!gameHasEnded)
		{
			Debug.Log("GAME OVER");
			gameHasEnded = true;

			Invoke("Restart", pause);
		}

	}

	public void CompleteLevel()
	{
		//CompleteLevelUI.SetActive(true);
		//Invoke("next", pause);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void next()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void Respawn()
	{
		Restart();//filler for now
	}

	public void BlueWins()
	{
		//SceneManager.LoadScene(1);
		blueWins.SetActive(true);
	}
	public void RedWins()
	{
		//SceneManager.LoadScene(2);
		//score.SetActive(false);
		redWins.SetActive(true);
		
	}

	public void Quit()
	{
		Application.Quit();
		SceneManager.LoadScene(3);
	}
	public void ToMainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
