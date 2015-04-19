using UnityEngine;
using System.Collections;

public class AmmoStorage : MonoBehaviour
{
    public int AmmoCount = 0;
    public Transform ActiveAmmoPrefab;

    void FixedUpdate()
    {
        if (Input.GetButton("Jump")
            && AmmoCount > 0)
        {
            AmmoCount--;

            Transform p = (Transform)Instantiate(ActiveAmmoPrefab, transform.position + new Vector3(0, 0, -3), transform.rotation);
        }
    }

    public void AddAmmo()
    {
        AmmoCount++;
    }
}
