using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    const float lerpingSpeed = 0.5f;
    const float distanceCheck = 1f;

    public static GrapplingHook Instance { get { return _instance; } }
    private static GrapplingHook _instance;

    private Projectile projectile;
    private GameObject playerObject;

    private GameObject currentEnemyObject;

    private bool lerpingPlayer;
    private bool lerpingEnemy;

    // Start is called before the first frame update
    void Awake()
    {
        //makes sure there is only one grapplinghook instance and sets that instance to this
        if (_instance != null && _instance != this) 
        { Destroy(gameObject); }
        else 
        { _instance = this; }

        projectile = GetComponent<Projectile>();
        playerObject = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;

        playerObject.GetComponent<PlayerMovement>().cannotMove = true; //disable player movement so grapple is attached to them

        lerpingPlayer = false;
        lerpingEnemy = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            lerpingPlayer = true;
        }
        else if (collision.GetComponent<EnemyDamage>())
        {
            collision.GetComponent<EnemyDamage>().StartCoroutine("Stun", 1f);
            currentEnemyObject = collision.gameObject;
            lerpingEnemy = true;
        }
    }

    private void FixedUpdate()
    {
        projectile.CurrentVelocity = (lerpingPlayer || lerpingEnemy) ? Vector3.zero : projectile.CurrentVelocity;

        if (lerpingPlayer)
        {
            if(Vector3.Distance(playerObject.transform.position, transform.position) <= distanceCheck) //if reached destination reset external variables and destroy this
            { DestroyObject(); }
            else
            { playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, transform.position, lerpingSpeed); }
        }
        else if(lerpingEnemy)
        {
            if(Vector3.Distance(currentEnemyObject.transform.position, playerObject.transform.position) <= distanceCheck)
            { DestroyObject(); }
            else
            { currentEnemyObject.transform.position = Vector3.Lerp(currentEnemyObject.transform.position, playerObject.transform.position, lerpingSpeed); }
        }
    }

    private void DestroyObject()
    {
        playerObject.GetComponent<PlayerMovement>().cannotMove = false;
        Destroy(gameObject);
    }
}
