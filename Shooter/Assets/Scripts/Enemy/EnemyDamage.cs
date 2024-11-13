using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int health;
    private bool TakingDamage;
    private Animator animator;

    private bool ChainsawDamage; //check for if last damage was from a chainsaw
    private EnemyMovement enemyMovement; //to reference movespeed for frost gun
    private void Awake()
    {
        TakingDamage = false;
        animator = GetComponent<Animator>();

        ChainsawDamage = false;
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bullet
        if(collision.GetComponent<Bullet>() != null)
        {
            //deplete health and destroy bullet on impact
            health -= collision.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject);
        }

        CheckForDeath();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!TakingDamage)
        {
            //reset chainsawdamage
            ChainsawDamage = false;

            //flamethrower
            if (collision.GetComponent<FlameThrower>() != null)
            {
                health -= collision.GetComponent<FlameThrower>().damage;
                StartCoroutine("TakeDamageDelay", 0.5f);
            }
            //chainsaw
            else if (collision.GetComponent<Chainsaw>() != null)
            {
                ChainsawDamage = true;
                health -= collision.GetComponent<Chainsaw>().damage;
                StartCoroutine("TakeDamageDelay", 0.2f);
            }
            //frost gun
            else if (collision.GetComponent<FrostGun>() != null)
            {
                StartCoroutine("SlowEffect");
                StartCoroutine("TakeDamageDelay", 0.5f); //this acts as a delay for the effect stack
            }
        }

        CheckForDeath();
    }

    private IEnumerator TakeDamageDelay(float delay)
    {
        TakingDamage = true;
        yield return new WaitForSeconds(delay);
        TakingDamage = false;
    }

    private IEnumerator SlowEffect()
    {
        //depletes movespeed by 10 percent
        if (enemyMovement.moveSpeed > enemyMovement.maxMoveSpeed/2) { enemyMovement.moveSpeed -= enemyMovement.maxMoveSpeed/10; }
        yield return new WaitForSeconds(1f);
        //restores movespeed after 1 second
        enemyMovement.moveSpeed += enemyMovement.maxMoveSpeed/10;
    }

    private void CheckForDeath()
    {
        if (health <= 0)
        {
            //animate death based on if chainsaw or not
            animator.SetTrigger(ChainsawDamage ? "ChainsawDeath" : "NormalDeath");
            Destroy(gameObject, 1f);
        }
    }
}
