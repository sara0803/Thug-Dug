using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Back() {
        Debug.Log("Back");
        Application.Quit();
    }
    public void GameOver(){
         SceneManager.LoadScene(2);
    }
    public void MainMenu(){
         SceneManager.LoadScene(0);
    }
    public void Win(){
         SceneManager.LoadScene(3);
    }
}