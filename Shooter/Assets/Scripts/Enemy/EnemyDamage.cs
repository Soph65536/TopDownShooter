using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float knockback;
    private bool TakingDamage;
    private Animator animator;

    public bool OrbOfFireEnabled; //controls how many enemies can be attacked by orboffire at once
    private bool ChainsawDamage; //check for if last damage was from a chainsaw
    private EnemyMovement enemyMovement; //to reference movespeed for frost gun
    private void Awake()
    {
        TakingDamage = false;
        animator = GetComponent<Animator>();

        OrbOfFireEnabled = false;
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
        //missile
        else if (collision.GetComponent<MissileShockwave>() != null)
        {
            health -= collision.GetComponent<MissileShockwave>().damage;
            Destroy(collision.gameObject.GetComponentInParent<Missile>().gameObject, 1f);
        }
        //ricochet
        else if (collision.GetComponent<RicochetBullet>() != null)
        {
            health -= collision.GetComponent<RicochetBullet>().damage;
        }
        //orb of fire
        else if (collision.GetComponent<OrbOfFire>() != null && OrbOfFireEnabled)
        {
            health -= collision.GetComponent<OrbOfFire>().damage;
            StartCoroutine("TakeDamageDelay", 1f);
        }
        //punching glove
        else if (collision.GetComponent<PunchingGlove>() != null)
        {
            health -= collision.GetComponent<PunchingGlove>().damage;
            StartCoroutine("Stun", 1f);
            transform.position += collision.gameObject.transform.parent.transform.parent.transform.up * knockback;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        //reset if tagged as an attackable enemy by orb of fire
        if (collision.GetComponent<OrbOfFire>() != null)
        {
            collision.GetComponent<OrbOfFire>().enemiesAttacking--;
            OrbOfFireEnabled = false;
        }
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

    private IEnumerator Stun(float stunDelay)
    {
        //temporarily disables movement for stun
        enemyMovement.moveSpeed = 0.01f;
        yield return new WaitForSeconds(stunDelay);
        enemyMovement.moveSpeed = enemyMovement.maxMoveSpeed;
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
