using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSingleFire : MonoBehaviour
{
    [SerializeField] private GameObject AmmoObject;
    [SerializeField] private float FireRate;

    private bool isFiring;

    private void Start()
    {
        isFiring = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //shoot!!
        if (!isFiring & Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Shoot");
        }
    }

    private IEnumerator Shoot()
    {
        isFiring = true;

        Instantiate(AmmoObject, transform);
        yield return new WaitForSeconds(FireRate);

        isFiring = false;
    }
}
