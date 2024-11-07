using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const float moveSpeed = 5f;

    private float horizontal;
    private float vertical;

    private Rigidbody2D rb;    

    // Start is called before the first frame update
    void Awake()
    {
        horizontal = 0f;
        vertical = 0f;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(horizontal * moveSpeed, vertical * moveSpeed, 0);
    }
}
