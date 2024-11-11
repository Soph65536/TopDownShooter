using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstAttack : MonoBehaviour
{
    //for flamethrower only
    [SerializeField] private bool isFlameThrower;
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

            //flamethrower
            if (isFlameThrower)
            {
                if (fuel <= 0) { AmmoObject.SetActive(false); }
                else { fuel--; }
            }
        }
        else
        {
            AmmoObject.SetActive(false);

            //flamethrower
            if (isFlameThrower)
            {
                if (fuel < maxFuel) { fuel++; }
            }
        }
    }
}
