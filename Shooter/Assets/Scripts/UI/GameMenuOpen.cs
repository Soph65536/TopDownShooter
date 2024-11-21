using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuOpen : MonoBehaviour
{
    [SerializeField] private GameObject gameMenuObject;

    private void Start()
    {
        gameMenuObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameManager.Instance.inGameMenu = !GameManager.Instance.inGameMenu; //switch bool after pressing esc
        }

        gameMenuObject.SetActive(GameManager.Instance.inGameMenu);
    }
}
