using UnityEngine;
using System.Collections;

public class AmmoSpawner : MonoBehaviour
{
    public Transform InctiveAmmoPrefab;
    private Transform Ammo;

    void Start()
    {
        Ammo = (Transform)Instantiate(InctiveAmmoPrefab, transform.localPosition, transform.localRotation);
    }

    void Update()
    {
        //delay respawning when Ammo removed
    }
}
