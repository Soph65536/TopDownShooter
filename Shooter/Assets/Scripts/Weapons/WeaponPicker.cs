using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPicker : MonoBehaviour
{
    [SerializeField] private GameObject[] Weapons;

    private KeyCode[] KeyCodes = {
        KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
        KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, 
        KeyCode.Alpha8, KeyCode.Alpha9,
    };

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < KeyCodes.Length; i++)
        {
            if (Input.GetKey(KeyCodes[i]))
            {
                //set so add 10 if pressing control and within limit
                int Weapon = (Input.GetKey(KeyCode.LeftShift) && i<7) ? i+10 : i;
                ChangeWeapon(Weapon);
            }
        }
    }

    private void ChangeWeapon(int num)
    {
        //disable current weapon
        Weapons[GameManager.Instance.CurrentWeapon].SetActive(false);

        //change to new current weapon and enable
        GameManager.Instance.CurrentWeapon = num;
        Weapons[GameManager.Instance.CurrentWeapon].SetActive(true);
    }
}
