using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBullet : MonoBehaviour
{
    public int damage;

    private int numOfHits;
    private Projectile projectileScript;

    // Start is called before the first frame update
    void Awake()
    {
        numOfHits = 0;
        projectileScript = GetComponent<Projectile>();
    }

    private void FixedUpdate()
    {
        if (numOfHits >= 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if hit something that isnt player
        if (collision.gameObject.GetComponent<EnemyDamage>() != null ||
            collision.gameObject.CompareTag("Wall"))
        {
            //reverse direction
            projectileScript.CurrentVelocity = -projectileScript.CurrentVelocity;
            numOfHits++;
        }
    }
}
