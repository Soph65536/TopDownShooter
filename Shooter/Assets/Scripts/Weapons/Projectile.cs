using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool hasTimeDespawn;
    [SerializeField] private float deathTime;

    private Rigidbody2D rb;
    public Vector3 CurrentVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //set velocity initial
        UpdateCurrentVelocity();

        transform.parent = null;

        if (hasTimeDespawn) { Destroy(gameObject, deathTime); }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = CurrentVelocity;
    }

    public void UpdateCurrentVelocity()
    {
        CurrentVelocity = transform.up * speed * Time.deltaTime;
    }
}
