using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const float moveSpeed = 6.5f;

    public bool cannotMove;

    private float horizontal;
    private float vertical;

    private Rigidbody2D rb;   
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        cannotMove = false;

        horizontal = 0f;
        vertical = 0f;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);


        rb.velocity = cannotMove ? Vector3.zero : new Vector3(horizontal * moveSpeed, vertical * moveSpeed, 0);
    }
}
