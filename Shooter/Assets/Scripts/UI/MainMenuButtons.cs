using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private AudioClip pressStart;

    public void PressStart()
    {
        SoundManager.Instance.PlaySound(true, pressStart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.Instance.inGameMenu = false;
    }

    public void PressQuit()
    {
        Application.Quit();
    }
}
