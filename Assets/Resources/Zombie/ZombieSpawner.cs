using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public Transform WanderingZombiePrefab;
    public float SpawnRate = 5;

    private bool spawning = false;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        if(!spawning)
        {
            StartCoroutine(CoSpawn());
        }
    }

    IEnumerator CoSpawn()
    {
        spawning = true;

        yield return new WaitForSeconds(SpawnRate);

        Spawn();
        spawning = false;
    }

    void Spawn()
    {
        Instantiate(WanderingZombiePrefab, transform.localPosition, transform.localRotation);
    }
}
