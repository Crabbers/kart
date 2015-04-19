using UnityEngine;
using System.Collections;

public class AmmoInactive : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        AmmoStorage container = col.transform.GetComponentInChildren<AmmoStorage>();
        if(container != null)
        {
            container.AddAmmo();
            Destroy(this.gameObject);
        }
    }
}
