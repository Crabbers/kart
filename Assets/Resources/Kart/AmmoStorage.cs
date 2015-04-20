using UnityEngine;
using System.Collections;

public class AmmoStorage : MonoBehaviour
{
    bool firing = false;
    public int AmmoCount = 0;
    public Transform ActiveAmmoPrefab;

    void FixedUpdate()
    {
        if (!firing
            && AmmoCount > 0
            && Input.GetButtonDown("Fire1")
            )
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        firing = true;
        AmmoCount--;

        Vector3 offset = new Vector3(0, 0, -3);
        offset = transform.rotation * offset;

        Transform p = (Transform)Instantiate(ActiveAmmoPrefab, transform.position + offset, transform.rotation);

        yield return new WaitForSeconds(1);
        firing = false;
    }

    public void AddAmmo()
    {
        AmmoCount++;
    }
}
