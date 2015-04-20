using UnityEngine;
using System.Collections;

public class WeaponZombie : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        AmmoStorage container = col.transform.GetComponent<AmmoStorage>();
        if (container != null)
        {
            //whatever
        }

        Destroy(this.gameObject);
    }
}
