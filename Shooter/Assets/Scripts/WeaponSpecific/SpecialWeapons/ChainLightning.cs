using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    [SerializeField] private float weaponRange;
    [SerializeField] private SpriteRenderer ChainLightningImg;

    private GameObject hitObject = null;

    private void Start()
    {
        ChainLightningImg = GetComponent<SpriteRenderer>();
        ChainLightningImg.enabled = false;
    }

    private void FixedUpdate()
    {
        LineCast();

        if(Input.GetMouseButtonUp(0))
        {
            ChainLightningImg.enabled = false; //disable image after mouse up
        }
    }

    private void LineCast()
    {
        RaycastHit2D hitobjinfo = Physics2D.Linecast(transform.position, transform.parent.transform.parent.up * weaponRange);
        if (hitobjinfo)
        {
            Debug.Log(hitobjinfo.ToString());
            hitObject = hitobjinfo.transform.gameObject;

            EnemyDamage enemyDamage = hitObject.GetComponent<EnemyDamage>();

            if (Input.GetMouseButtonDown(0) && enemyDamage != null)
            {
                ChainLightningImg.enabled = true;
                enemyDamage.ChainLightningEnabled = true;
            }
        }
    }
}
