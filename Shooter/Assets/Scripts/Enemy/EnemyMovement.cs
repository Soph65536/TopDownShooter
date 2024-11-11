using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    const float moveSpeed = 0.05f;
    const int damageAmount = 1;

    private Rigidbody rb;
    private GameObject playerObject;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerObject = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(Vector3.Lerp(transform.position, playerObject.transform.position, moveSpeed));
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject == playerObject)
        {
            playerObject.GetComponent<PlayerDamage>().TakeDamage(damageAmount);
        }
    }
}
