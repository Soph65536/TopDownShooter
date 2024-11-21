using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    const int maxHealth = 100;
    const float DamageCooldown = 1f;
    const float DeathCooldown = 2f;

    public int Health;
    private bool TakingDamage;

    private Animator animator;

    private void Awake()
    {
        Health = maxHealth;
        TakingDamage = false;

        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //make sure health doesn't go over max
        if(Health>maxHealth) { Health = maxHealth; }
    }

    public void TakeDamage(int damage)
    {
        if(!TakingDamage) { StartCoroutine("DamageCoroutine", damage); }
    }

    private IEnumerator DamageCoroutine(int damage)
    {
        TakingDamage = true;

        //take damage
        Health -= damage;
        yield return new WaitForSeconds(DamageCooldown);

        //death check
        if (Health <= 0)
        {
            animator.SetTrigger("Death");
            yield return new WaitForSeconds(DeathCooldown);
            transform.position = GameManager.Instance.SpawnPosition;
            Health = maxHealth;
        }

        TakingDamage = false;
    }
}
