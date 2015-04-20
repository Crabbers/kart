using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        Application.LoadLevel(1);
    }

    public void ReplayGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
