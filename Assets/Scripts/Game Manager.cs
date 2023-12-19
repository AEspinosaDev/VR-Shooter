using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //static GameManager instance;

    //private void Awake()
    //{
    //    if(instance != null)
    //    {
    //        Destroy(this);
    //    }
    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}



    public void StartGame()
    {

        //Time.timeScale = 1;
        SceneManager.LoadScene("GameScene",LoadSceneMode.Single);

    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
    public void Quit()
    {
        Application.Quit();

    }
}
