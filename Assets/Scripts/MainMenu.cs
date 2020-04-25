using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    float volume = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayRaceTrack()
    {
        SceneManager.LoadScene(6);
    }

    public void PlayCliffTrack()
    {
        SceneManager.LoadScene(5);
    }

    public void PlayCityTrack()
    {
        SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
