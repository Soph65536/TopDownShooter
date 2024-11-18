using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDart : MonoBehaviour
{
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Awake()
    {
        transform.position -= transform.parent.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, transform.parent.position, speed); //move towards the max distance
        if(Vector3.Distance(transform.position, transform.parent.position) <= Vector3.kEpsilon) { Destroy(gameObject); } //destroy after moved max distance
    }
}
