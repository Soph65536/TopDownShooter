using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuButtons : MonoBehaviour
{
    public void PressContinue()
    {
        GameManager.Instance.inGameMenu = false;
    }

    public void PressMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Cursor.lockState = CursorLockMode.None;
    }

    public void PressQuit()
    {
        Application.Quit();
    }
}
