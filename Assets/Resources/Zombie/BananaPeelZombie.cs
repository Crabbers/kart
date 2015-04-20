using UnityEngine;
using System.Collections;

public class BananaPeelZombie : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        AmmoStorage container = col.transform.GetComponent<AmmoStorage>();
        if (container != null)
        {
            Destroy(this.gameObject);
            //Explode?
        }
    }
}
