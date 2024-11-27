using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPicker : MonoBehaviour
{
    [SerializeField] private AudioClip changeWeaponSound;
    [SerializeField] private GameObject[] Weapons;
    [SerializeField] private TextMeshProUGUI CurrentWeaponText; 

    private KeyCode[] KeyCodes = {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3,
        KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, 
        KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0,
    };

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < KeyCodes.Length; i++)
        {
            if (Input.GetKey(KeyCodes[i]))
            {
                //set so add 10 if pressing control and within limit
                int Weapon = (Input.GetKey(KeyCode.LeftControl) && i<6) ? i+10 : i;
                ChangeWeapon(Weapon);
            }
        }
    }

    private void ChangeWeapon(int num)
    {
        //if weapon isnt already the one selected
        if(num != GameManager.Instance.CurrentWeapon)
        {
            SoundManager.Instance.PlaySound(true, changeWeaponSound);

            //disable current weapon
            Weapons[GameManager.Instance.CurrentWeapon].SetActive(false);
            Weapons[GameManager.Instance.CurrentWeapon].GetComponent<WeaponName>().UIIcon.GetComponent<Outline>().enabled = false;

            //change to new current weapon and enable
            GameManager.Instance.CurrentWeapon = num;
            Weapons[GameManager.Instance.CurrentWeapon].SetActive(true);
            //change UI
            CurrentWeaponText.text = Weapons[GameManager.Instance.CurrentWeapon].GetComponent<WeaponName>().weaponName;
            Weapons[GameManager.Instance.CurrentWeapon].GetComponent<WeaponName>().UIIcon.GetComponent<Outline>().enabled = true;
        }
    }
}
