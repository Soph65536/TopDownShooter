using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryoBlast : MonoBehaviour
{
    private CircleCollider2D triggerArea;
    private void Start()
    {
        triggerArea = GetComponent<CircleCollider2D>();
        triggerArea.enabled = false;
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0) && !triggerArea.enabled) { StartCoroutine("ActivateTriggerArea"); }
    }

    private IEnumerator ActivateTriggerArea()
    {
        triggerArea.enabled = true;
        yield return new WaitForSeconds(0.1f);
        triggerArea.enabled = false;
    }
}
