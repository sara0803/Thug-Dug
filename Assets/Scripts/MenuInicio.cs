using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{


    public void MainMenu(){
         SceneManager.LoadScene(0);
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void History()
    {
        SceneManager.LoadScene(4);
    }
    public void God()
    {
        SceneManager.LoadScene(1);
    }
    public void Back()
    {

        Application.Quit();
    }
    public void Win(){
         SceneManager.LoadScene(3);
    }
    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }
}