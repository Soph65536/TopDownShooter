using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbOfFire : MonoBehaviour
{
    public int damage;

    //zigzag stuff

    [SerializeField] private float ZigZagDelay;
    private bool isRotation;
    private float currentRotation;
    private Projectile projectileScript;

    public int enemiesAttacking;

    void Awake()
    {
        isRotation = false;
        currentRotation = 90;
        projectileScript = GetComponent<Projectile>();

        //offset rotation initially by 45 degrees
        transform.localEulerAngles = new Vector3 (
            transform.localEulerAngles.x, 
            transform.localEulerAngles.y, 
            transform.localEulerAngles.z - 45);

        enemiesAttacking = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isRotation) { StartCoroutine("ZigZagRotate"); }
    }

    private IEnumerator ZigZagRotate()
    {
        isRotation = true;

        //rotate 45 degrees and update velocity
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z + currentRotation);
        projectileScript.UpdateCurrentVelocity();
        //wait
        yield return new WaitForSeconds(ZigZagDelay);
        //then reverse rotation
        currentRotation = -currentRotation;

        isRotation = false;
    }


    //up to 3 enemies attack
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<EnemyDamage>() != null)
        {
            if(enemiesAttacking < 3)
            {
                enemiesAttacking++;
                collision.GetComponent<EnemyDamage>().OrbOfFireEnabled = true;
            }
        }
    }
}
