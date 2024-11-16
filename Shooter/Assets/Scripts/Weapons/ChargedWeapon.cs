using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedWeapon : MonoBehaviour
{
    [SerializeField] private float MaxYPos;
    [SerializeField] private GameObject ObjectToInstantiate;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Down");
            transform.localPosition = Vector3.zero;
        }
        else if (Input.GetMouseButton(0) && transform.localPosition.y < MaxYPos)
        {
            transform.localPosition += Vector3.up/10;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Up");
            Instantiate(ObjectToInstantiate, transform);
        }
    }
}
