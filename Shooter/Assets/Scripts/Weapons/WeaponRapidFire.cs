using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRapidFire : MonoBehaviour
{
    [SerializeField] private GameObject AmmoObject;
    [SerializeField] private float FireRate;

    private bool isFiring;

    //for minigun
    [SerializeField] private float InitialFireDelay;
    [SerializeField] private bool CanOverheat;
    [SerializeField] private float OverheatDelay;
    [SerializeField] private int MaxFireBeforeOverheat;
    private int CurrentOverheat;

    private void Start()
    {
        isFiring = false;

        CurrentOverheat = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //minigun initial delay
        if(!isFiring & Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Delay", InitialFireDelay);
        }

        //minigun overheat check
        if (CanOverheat && CurrentOverheat >= MaxFireBeforeOverheat)
        {
            Debug.Log("Overheat");
            StartCoroutine("Delay", OverheatDelay);
            CurrentOverheat = 0;
        }

        //shoot!!
        if (!isFiring & Input.GetMouseButton(0))
        {
            StartCoroutine("Shoot");
        }
    }

    private IEnumerator Shoot()
    {
        isFiring = true;

        Instantiate(AmmoObject, transform);
        yield return new WaitForSeconds(FireRate);

        if (CanOverheat) { CurrentOverheat++; }

        isFiring = false;
    }

    private IEnumerator Delay(float delay)
    {
        isFiring = true;
        yield return new WaitForSeconds(delay);
        isFiring = false;
    }
}
