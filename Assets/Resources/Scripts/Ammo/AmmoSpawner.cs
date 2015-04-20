using UnityEngine;
using System.Collections;

public class AmmoSpawner : MonoBehaviour
{
    public Transform InctiveAmmoPrefab;
    private Transform Ammo;
    private bool spawning = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(!spawning
            && Ammo == null)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        spawning = true;

        Ammo = (Transform)Instantiate(InctiveAmmoPrefab, transform.localPosition, transform.localRotation);

        yield return new WaitForSeconds(1);
        spawning = false;
    }
}
