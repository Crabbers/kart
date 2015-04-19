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

            Transform p = (Transform)Instantiate(ActiveAmmoPrefab, transform.localPosition + new Vector3(2, 0, 0), transform.localRotation);
        }
    }

    public void AddAmmo()
    {
        AmmoCount++;
    }
}
