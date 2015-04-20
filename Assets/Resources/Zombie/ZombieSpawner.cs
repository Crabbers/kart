using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public Transform WanderingZombiePrefab;
    public float SpawnRate = 20;

    private bool spawning = false;

    void Start()
    {
        float initialDelay = Random.Range(0f, SpawnRate);

        StartCoroutine(CoSpawn(initialDelay));
    }

    void Update()
    {
        if(!spawning)
        {
            StartCoroutine(CoSpawn(SpawnRate));
        }
    }

    IEnumerator CoSpawn(float delay)
    {
        spawning = true;

        yield return new WaitForSeconds(delay);

        Spawn();
        spawning = false;
    }

    void Spawn()
    {
        Instantiate(WanderingZombiePrefab, transform.localPosition, transform.localRotation);
    }
}
