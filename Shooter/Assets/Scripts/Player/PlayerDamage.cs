using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    const int maxHealth = 100;
    const float DamageCooldown = 1f;

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

        if(Health <= 0)
        {
            transform.position = Vector3.zero;
            Health = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if(!TakingDamage) { StartCoroutine("DamageCoroutine", damage); }
    }

    private IEnumerator DamageCoroutine(int damage)
    {
        TakingDamage = true;

        Health -= damage;
        yield return new WaitForSeconds(DamageCooldown);

        TakingDamage = false;
    }
}
