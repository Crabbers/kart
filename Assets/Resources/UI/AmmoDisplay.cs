using UnityEngine;
using System.Collections;

public class AmmoDisplay : MonoBehaviour
{
    private AmmoStorage storage;
    private UnityEngine.UI.Text t;

    void Start()
    {
        t = GetComponent<UnityEngine.UI.Text>();
    }

    void Update()
    {
        t.text = storage.AmmoCount.ToString();
    }

    public void SetAmmoStorage(AmmoStorage kart)
    {
        storage = kart;
    }
}
