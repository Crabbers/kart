using UnityEngine;
using System.Collections;

public class GreenShellZombie : MonoBehaviour
{
    public float Speed = 2f;
    public AmmoStorage Shooter;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Linecast(
            transform.position,
            transform.position + transform.forward * Speed,
            out hit))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Karts"))
            {
                AmmoStorage hitStorage = hit.transform.GetComponentInChildren<AmmoStorage>() as AmmoStorage;
                if (!ReferenceEquals(hitStorage, Shooter))
                {
                    Shooter.AddScore();
                }

                hitStorage.DealDamage();

                Destroy(gameObject);
            }
            else
            {
                transform.position = hit.point;
                Vector3 newForward = Vector3.Reflect(transform.forward, hit.normal);
                newForward.y = 0f;
                transform.forward = Vector3.Normalize(newForward);
            }
        }

        transform.position += transform.forward * Speed;
    }
}
