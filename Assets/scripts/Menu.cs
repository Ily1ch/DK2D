using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{ 
    public void ExitGame()
    {
        Debug.Break();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void Settings()
    {
        SceneManager.LoadScene(2);
    }
    public void ExitSettings()
    {
        SceneManager.LoadScene(1);
    }
}
