using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuButtons : MonoBehaviour
{
    [SerializeField] private AudioClip pressContinue;
    [SerializeField] private AudioClip pressMainMenu;

    public void PressContinue()
    {
        SoundManager.Instance.PlaySound(true, pressContinue);
        GameManager.Instance.inGameMenu = false;
    }

    public void PressMainMenu()
    {
        SoundManager.Instance.PlaySound(true, pressMainMenu);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Cursor.lockState = CursorLockMode.None;
    }

    public void PressQuit()
    {
        Application.Quit();
    }
}
