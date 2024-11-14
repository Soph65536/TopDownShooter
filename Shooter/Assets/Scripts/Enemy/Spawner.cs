using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float SpawnPositionRange;
    [SerializeField] private float SpawnDelay;
    [SerializeField] private GameObject SpawnObject;

    private bool CurrentlySpawning;

    private void Start()
    {
        CurrentlySpawning = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!CurrentlySpawning) { StartCoroutine(Spawn()); }
    }

    private IEnumerator Spawn()
    {
        CurrentlySpawning = true;

        //create a random location within range to spawn
        Vector3 randomPosition = transform.position + 
            Vector3.right * Random.Range(-SpawnPositionRange, SpawnPositionRange) +
            Vector3.up * Random.Range(-SpawnPositionRange, SpawnPositionRange);

        Instantiate(SpawnObject, randomPosition, Quaternion.identity, transform);
        yield return new WaitForSeconds(SpawnDelay);

        CurrentlySpawning = false;
    }
}
