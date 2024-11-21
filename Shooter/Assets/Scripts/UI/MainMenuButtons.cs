using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void PressStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.Instance.inGameMenu = false;
    }

    public void PressQuit()
    {
        Application.Quit();
    }
}
