using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private float TeleportDistance;
    private bool CooldownActive;
    private void Start()
    {
        CooldownActive = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !CooldownActive)
        {
            transform.parent.transform.parent.position += transform.parent.transform.up * TeleportDistance;
            StartCoroutine("Cooldown");
        }
    }

    private IEnumerator Cooldown()
    {
        CooldownActive = true;
        yield return new WaitForSeconds(5f);
        CooldownActive = false;
    }
}
