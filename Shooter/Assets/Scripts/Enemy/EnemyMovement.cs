using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    const int damageAmount = 1;

    public float maxMoveSpeed = 0.05f;
    public float moveSpeed = 0.05f;

    private Rigidbody2D rb;
    private GameObject playerObject;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(Vector3.Lerp(transform.position, playerObject.transform.position, moveSpeed));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == playerObject)
        {
            playerObject.GetComponent<PlayerDamage>().TakeDamage(damageAmount);
        }
    }
}
