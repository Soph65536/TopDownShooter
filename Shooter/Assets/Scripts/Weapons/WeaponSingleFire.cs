using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSingleFire : MonoBehaviour
{
    [SerializeField] private AudioClip shootSound;
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
        if (!isFiring & (Input.GetMouseButtonUp(0) || Input.GetMouseButtonDown(0)))
        {
            StartCoroutine("Shoot");
        }
    }

    private IEnumerator Shoot()
    {
        isFiring = true;

        SoundManager.Instance.PlaySound(true, shootSound);
        Instantiate(AmmoObject, transform);
        yield return new WaitForSeconds(FireRate);

        isFiring = false;
    }
}
