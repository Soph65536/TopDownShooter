using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    const float deathTime = 1f;

    [SerializeField] private int health;
    [SerializeField] private float knockback;
    private bool TakingDamage;
    private Animator animator;

    //chainlightning
    public bool ChainLightningEnabled;
    private EnemyChainLightning enemyChainLightning;
    //this weapon doesn't use a ontrigger to attack so easier to store its damage here than
    //finding the gameobject every time
    [SerializeField] private int ChainLightningDamage;
    public int ChainLightningIterations;//num of previous chain lightnings before this one

    public bool OrbOfFireEnabled; //controls how many enemies can be attacked by orboffire at once
    private bool isIceCube; //for cryo blast
    private bool ChainsawDamage; //check for if last damage was from a chainsaw
    private EnemyMovement enemyMovement; //to reference movespeed
    private void Awake()
    {
        TakingDamage = false;
        animator = GetComponent<Animator>();

        ChainLightningEnabled = false;
        enemyChainLightning = GetComponentInChildren<EnemyChainLightning>();
        ChainLightningIterations = 0;

        OrbOfFireEnabled = false;
        ChainsawDamage = false;
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void FixedUpdate()
    {
        if(!TakingDamage && ChainLightningEnabled)
        {
            TakeDamage(ChainLightningDamage - (int)((float)ChainLightningDamage * 0.15f * (float)ChainLightningIterations));
            enemyChainLightning.StartCoroutine("SpreadLightning", ChainLightningIterations + 1);

            StartCoroutine("TakeDamageDelay", 1f); //prevents receiving chain damage from another enemy
            animator.SetTrigger("ChainLightning");

            //if chaindamage then reset after the chain
            ChainLightningEnabled = false;
            ChainLightningIterations = 0;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bullet
        if(collision.GetComponent<Bullet>() != null)
        {
            //deplete health and destroy bullet on impact
            TakeDamage(collision.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }
        //missile
        else if (collision.GetComponent<MissileShockwave>() != null)
        {
            TakeDamage(collision.GetComponent<MissileShockwave>().damage);
            Destroy(collision.gameObject.GetComponentInParent<Missile>().gameObject, 1f);
        }
        //ricochet
        else if (collision.GetComponent<RicochetBullet>() != null)
        {
            TakeDamage(collision.GetComponent<RicochetBullet>().damage);
        }
        //orb of fire
        else if (collision.GetComponent<OrbOfFire>() != null && OrbOfFireEnabled)
        {
            TakeDamage(collision.GetComponent<OrbOfFire>().damage);
            StartCoroutine("TakeDamageDelay", 1f);
        }
        //punching glove
        else if (collision.GetComponent<PunchingGlove>() != null)
        {
            TakeDamage(collision.GetComponent<PunchingGlove>().damage);
            StartCoroutine("Stun", 1f);
            transform.position += collision.gameObject.transform.parent.transform.parent.transform.up * knockback;
        }
        //ice cube
        else if (collision.GetComponent<CryoBlast>() != null)
        {
            if (!isIceCube) { StartCoroutine("BecomeIceCube"); }
        }
        //grenade
        else if (collision.GetComponent<Grenade>() != null)
        {
            TakeDamage(collision.GetComponent<Grenade>().damage);
        }
        //mind blast emission
        else if (collision.GetComponent<MindBlastEmission>() != null)
        {
            TakeDamage(collision.GetComponent<MindBlastEmission>().damage);
            StartCoroutine("BecomeFriendly");
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
                TakeDamage(collision.GetComponent<FlameThrower>().damage);
                StartCoroutine("TakeDamageDelay", 0.5f);
            }
            //chainsaw
            else if (collision.GetComponent<Chainsaw>() != null)
            {
                ChainsawDamage = true;
                TakeDamage(collision.GetComponent<Chainsaw>().damage);
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

    public void TakeDamage(int damage)
    {
        if (isIceCube)
        {
            animator.SetTrigger("IceCubeShatter");
            Destroy(gameObject, deathTime);
        }
        health -= damage;
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
        yield return new WaitForSeconds(1.5f);
        //restores movespeed after 1 second
        enemyMovement.moveSpeed += enemyMovement.maxMoveSpeed/10;
    }

    private IEnumerator Stun(float stunDelay)
    {
        //temporarily disables movement for stun
        enemyMovement.stunned = true;
        enemyMovement.moveSpeed = 0.01f;

        yield return new WaitForSeconds(stunDelay);

        enemyMovement.moveSpeed = enemyMovement.maxMoveSpeed;
        enemyMovement.stunned = false;
    }

    private IEnumerator BecomeFriendly()
    {
        enemyMovement.friendly = true;
        yield return new WaitForSeconds(10f);
        enemyMovement.friendly = false;
    }

    private IEnumerator BecomeIceCube()
    {
        //set bool for other attack checks
        isIceCube = true;

        //set animator and temp disable movement
        animator.SetBool("IceCube", true);
        enemyMovement.moveSpeed = 0f;

        yield return new WaitForSeconds(3f);

        animator.SetBool("IceCube", false);
        enemyMovement.moveSpeed = enemyMovement.maxMoveSpeed;

        isIceCube = false;
    }

    private void CheckForDeath()
    {
        if (health <= 0)
        {
            //animate death based on if chainsaw or not
            animator.SetTrigger(ChainsawDamage ? "ChainsawDeath" : "NormalDeath");
            Destroy(gameObject, deathTime);
        }
    }
}
