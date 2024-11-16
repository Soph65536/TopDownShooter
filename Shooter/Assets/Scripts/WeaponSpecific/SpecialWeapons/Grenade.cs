using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int damage;

    private Animator animator;
    private CircleCollider2D triggerArea;

    // Start is called before the first frame update
    void Awake()
    {
        transform.parent = null;

        animator = GetComponent<Animator>();
        triggerArea = GetComponent<CircleCollider2D>();
        triggerArea.enabled = false;

        StartCoroutine("GrenadeExplosion");
    }

    private IEnumerator GrenadeExplosion()
    {
        yield return new WaitForSeconds(3f); //delkay after spawning

        //explosion and delayed death for animation
        triggerArea.enabled = true;
        animator.SetTrigger("Explosion");
        Destroy(gameObject, 0.6f);
    }
}
