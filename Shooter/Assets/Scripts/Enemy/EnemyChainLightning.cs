using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChainLightning : MonoBehaviour
{
    private EnemyDamage enemyDamage;
    private void Start()
    {
        enemyDamage = GetComponentInParent<EnemyDamage>();
    }

    private EnemyDamage[] FindClosestEnemies()
    {
        EnemyDamage[] closestEnemies = { null, null, null }; //set to null so can be returned as empty list if no enemies
        float[] closestDistances = { 10f, 10f, 10f }; //this is initially set to the max distance that counts as close by

        EnemyDamage[] enemies = GameObject.FindObjectsOfType<EnemyDamage>();

        foreach(EnemyDamage enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            for(int i = 0; i < 2; i++)
            {
                if(distance < closestDistances[i])
                {
                    if (i != 2) // if not last item check
                    {
                        //move current value into next value
                        closestDistances[i + 1] = closestDistances[i];
                        closestEnemies[i + 1] = closestEnemies[i];
                    }
                    //set current value to new value
                    closestDistances[i] = distance;
                    closestEnemies[i] = enemy;
                    break; //stops setting everything as the one enemy
                }
            }
        }

        return closestEnemies;
    }

    public IEnumerator SpreadLightning(int iterations)
    {
        //only allows to chain 3 times
        if (iterations <= 3)
        {
            EnemyDamage[] closestEnemies = FindClosestEnemies();
            foreach (EnemyDamage enemy in closestEnemies)
            {
                if (enemy != null)//incase there are less than 3 enemies
                {
                    enemy.ChainLightningEnabled = true;
                    enemy.ChainLightningIterations = iterations;
                }
            }
        }
        
        yield return new WaitForEndOfFrame();
    }
}
