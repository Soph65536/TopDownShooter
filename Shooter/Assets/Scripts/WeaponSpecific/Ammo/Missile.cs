using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private GameObject Shockwave;
    private Projectile projectileScript;

    // Start is called before the first frame update
    void Awake()
    {
        Shockwave.SetActive(false);
        projectileScript = GetComponent<Projectile>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if hit something that isnt player
        if(collision.gameObject.GetComponent<PlayerMovement>() == null)
        {
            Shockwave.SetActive(true);
            projectileScript.CurrentVelocity = Vector3.zero;
        }
    }
}
