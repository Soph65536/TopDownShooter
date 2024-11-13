using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAtPlayer : MonoBehaviour
{
    [SerializeField] private float FollowPlayerSpeed;
    private GameObject PlayerObject;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerObject = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //lerp to players position with -10z
        transform.position = Vector3.Lerp(transform.position, PlayerObject.transform.position + (Vector3.back*10), FollowPlayerSpeed);
    }
}
