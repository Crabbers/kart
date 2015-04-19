using UnityEngine;
using System.Collections;

public class AmmoActive : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        AmmoStorage container = col.transform.GetComponentInChildren<AmmoStorage>();
        if (container != null)
        {
            Destroy(this.gameObject);
            //Explode?
        }
    }
}
