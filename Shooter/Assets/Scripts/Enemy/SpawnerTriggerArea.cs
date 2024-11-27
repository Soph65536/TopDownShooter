using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTriggerArea : MonoBehaviour
{
    [SerializeField] private float SpawnerActiveDuration;
    private GameObject SpawnerObject;

    private void Start()
    {
        SpawnerObject = GetComponentInChildren<Spawner>().gameObject;
        SpawnerObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerDamage>() != null)
        {
            SpawnerObject.SetActive(true);
            Destroy(gameObject, SpawnerActiveDuration);
        }
    }
}
