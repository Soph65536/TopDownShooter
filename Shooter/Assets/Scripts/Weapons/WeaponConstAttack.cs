using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstAttack : MonoBehaviour
{
    //for fuel stuff only
    [SerializeField] private bool isFuel;
    [SerializeField] private int maxFuel;
    private int fuel;
    void Start()
    {
        fuel = maxFuel;
    }

    //actual script
    [SerializeField] private GameObject AmmoObject;

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            AmmoObject.SetActive(true);

            //fuel
            if (isFuel)
            {
                if (fuel <= 0) { AmmoObject.SetActive(false); }
                else { fuel--; }
            }
        }
        else
        {
            AmmoObject.SetActive(false);

            //fuel
            if (isFuel)
            {
                if (fuel < maxFuel) { fuel++; }
            }
        }
    }
}
