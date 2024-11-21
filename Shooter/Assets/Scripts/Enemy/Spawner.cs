using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float SpawnPositionRangeX;
    [SerializeField] private float SpawnPositionRangeY;
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
            Vector3.right * Random.Range(-SpawnPositionRangeX, SpawnPositionRangeX) +
            Vector3.up * Random.Range(-SpawnPositionRangeY, SpawnPositionRangeY);

        Instantiate(SpawnObject, randomPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)), transform); ;
        yield return new WaitForSeconds(SpawnDelay);

        CurrentlySpawning = false;
    }
}
