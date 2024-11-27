using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryoBlast : MonoBehaviour
{
    private CircleCollider2D triggerArea;
    private SpriteRenderer triggerImg;
    private void Start()
    {
        triggerArea = GetComponent<CircleCollider2D>();
        triggerArea.enabled = false;

        triggerImg = GetComponent<SpriteRenderer>();
        triggerImg.enabled = false;
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0) && !triggerArea.enabled) { StartCoroutine("ActivateTriggerArea"); }
    }

    private IEnumerator ActivateTriggerArea()
    {
        triggerArea.enabled = true;
        triggerImg.enabled = true;

        yield return new WaitForSeconds(0.1f);

        triggerArea.enabled = false;
        triggerImg.enabled = false;
    }
}
