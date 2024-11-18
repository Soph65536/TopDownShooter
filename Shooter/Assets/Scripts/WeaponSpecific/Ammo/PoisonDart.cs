using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDart : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 TargetPosition;

    // Start is called before the first frame update
    void Awake()
    {
        TargetPosition = transform.parent.position;
        transform.position = transform.parent.transform.parent.position;

        transform.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, speed); //move towards the max distance
        Debug.Log(transform.position);
        Debug.Log(TargetPosition);
        if(Vector3.Distance(transform.position, TargetPosition) <= 1f) { Destroy(gameObject); } //destroy after moved max distance
    }
}
