using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Awake()
    {
        mousePos = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rotate to mousepos for aim
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(mousePos);
    }
}
