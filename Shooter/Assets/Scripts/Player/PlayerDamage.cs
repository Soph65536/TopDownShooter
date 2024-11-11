using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    const int maxHealth = 100;
    const float DamageCooldown = 1f;
    const float DeathCooldown = 2f;

    public int Health;
    private bool TakingDamage;

    private void Awake()
    {
        Health = maxHealth;
        TakingDamage = false;
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
            transform.position = GameManager.Instance.SpawnPosition;
            yield return new WaitForSeconds(DeathCooldown);
            Health = maxHealth;
        }

        TakingDamage = false;
    }
}
