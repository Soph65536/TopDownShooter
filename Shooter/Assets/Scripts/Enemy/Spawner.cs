using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
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

        Instantiate(SpawnObject, transform);
        yield return new WaitForSeconds(SpawnDelay);

        CurrentlySpawning = false;
    }
}
