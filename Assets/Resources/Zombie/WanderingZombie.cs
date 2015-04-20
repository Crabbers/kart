using UnityEngine;
using System.Collections;

public class WanderingZombie : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        AmmoStorage container = col.transform.GetComponent<AmmoStorage>();
        if(container != null)
        {
            container.AddAmmo();
            Destroy(this.gameObject);
        }
    }
}
