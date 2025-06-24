using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("Home");
    }
    public void Help()
    {
        SceneManager.LoadScene("HowtoPlay");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainPlay");
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
