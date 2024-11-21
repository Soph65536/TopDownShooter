using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int damageAmount;

    public float maxMoveSpeed = 0.07f;
    public float moveSpeed = 0.07f;
    public bool stunned = false;
    public bool friendly = false;

    private Rigidbody2D rb;
    private GameObject playerObject;

    private GameObject targetObject;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObject = GameObject.FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetObject = friendly ? findClosestEnemy() : playerObject;

        //check incase moving to enemy object and there arent any
        if (targetObject != null ) { rb.MovePosition(Vector3.Lerp(transform.position, targetObject.transform.position, moveSpeed)); }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == playerObject && !stunned && !friendly)
        {
            playerObject.GetComponent<PlayerDamage>().TakeDamage(damageAmount);
        }
        else if (collision.gameObject.GetComponent<EnemyDamage>() != null && friendly)
        {
            collision.gameObject.GetComponent<EnemyDamage>().TakeDamage(damageAmount);
        }
    }

    private GameObject findClosestEnemy()
    {
        EnemyDamage[] enemies = GameObject.FindObjectsOfType<EnemyDamage>();

        float closestDistance = Mathf.Infinity;
        GameObject ClosestEnemy = null;

        foreach (EnemyDamage enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance < closestDistance)
            {
                ClosestEnemy = enemy.gameObject;
            }
        }

        return ClosestEnemy;
    }
}
