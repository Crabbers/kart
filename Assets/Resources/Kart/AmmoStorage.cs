using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class AmmoStorage : MonoBehaviour
{
    public int AmmoCount = 0;
    public Transform ActiveAmmoPrefab;
    public float FireRate = 1;

    private bool firing = false;

    private PlayerType type = PlayerType.Drone;

    void Start()
    {
        CarUserControl car = transform.parent.GetComponent<CarUserControl>();
        if(car)
        {
            type = car.m_playerType;
        }
    }

    void FixedUpdate()
    {
        if (!firing
            && AmmoCount > 0
            && (
                    (type == PlayerType.Player1 && Input.GetButton("P1 Fire1"))
                || (type == PlayerType.Player2 && Input.GetButton("P2 Fire1"))
                )
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

        yield return new WaitForSeconds(FireRate);
        firing = false;
    }

    public void AddAmmo()
    {
        AmmoCount++;
    }
}
