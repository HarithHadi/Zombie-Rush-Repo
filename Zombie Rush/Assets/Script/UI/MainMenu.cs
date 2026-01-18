using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton() 
    {
        SceneManager.LoadScene("Default Scene");
        Time.timeScale = 1.0f;
    }

    public void QuitButton() 
    {
        Application.Quit();
    }
}
