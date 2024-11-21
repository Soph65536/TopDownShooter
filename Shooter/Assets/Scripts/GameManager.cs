using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //creates the gamemanager instance
    public static GameManager Instance { get { return _instance; } }
    private static GameManager _instance;

    //variables

    //ui
    public bool inGameMenu;

    //player
    public Vector3 SpawnPosition;
    public int CurrentWeapon;

    void Awake()
    {
        //makes sure there is only one gamemanager instance and sets that instance to this
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        inGameMenu = false;

        SpawnPosition = Vector3.zero;
        CurrentWeapon = 0;
    }

    private void Update()
    {
        //checks to make sure current weapon doesnt exceed limits
        if(CurrentWeapon > 16) { CurrentWeapon = 16; }
        if (CurrentWeapon < 0) {  CurrentWeapon = 0; }

        Time.timeScale = inGameMenu ? 0 : 1;
    }
}
