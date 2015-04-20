using UnityEngine;
using System.Collections;

public class AmmoSpawner : MonoBehaviour
{
    public Transform InctiveAmmoPrefab;
    private Transform Ammo;
    private bool spawning = false;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        if(!spawning
            && Ammo == null)
        {
            StartCoroutine(CoSpawn());
        }
    }

    IEnumerator CoSpawn()
    {
        spawning = true;

        yield return new WaitForSeconds(1);

        Spawn();
        spawning = false;
    }

    void Spawn()
    {
        Ammo = (Transform)Instantiate(InctiveAmmoPrefab, transform.localPosition, transform.localRotation);
    }
}
