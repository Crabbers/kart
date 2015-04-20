using UnityEngine;
using System.Collections;

public class GreenShellZombie : MonoBehaviour
{
    public float Speed = 2f;

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
            transform.position = hit.point;
            Vector3 newForward = Vector3.Reflect(transform.forward, hit.normal);
            newForward.y = 0f;
            transform.forward = Vector3.Normalize(newForward);
        }

        transform.position += transform.forward * Speed;
    }
}
