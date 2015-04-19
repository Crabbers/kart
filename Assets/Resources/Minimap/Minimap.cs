using UnityEngine;
using System.Collections;

/**
 * Define a Mini-Map for our Kart thing.
 * Based on: http://answers.unity3d.com/questions/20676/how-do-i-create-a-minimap.html.
 */
public class Minimap : MonoBehaviour
{
    // Self Reference.
    private Camera _minimapCamera;

    // Set mini-map relative to the map/level.
    public Transform Map;

    // Set Mini-map camera relative to the Player camera.
    public Camera PlayerCamera;

    void Start()
    {
        // Retrieve attached camera component.
        _minimapCamera = this.GetComponent<Camera>();

        // Set relative to map.
        transform.parent = Map;
        
        // Now at origin of map.
        _minimapCamera.transform.localPosition = new Vector3(0f, 300f, 0f);
        _minimapCamera.rect = new Rect(0.5f, -0.6f, 1f, 1f);
        _minimapCamera.fieldOfView = 40f;

        // Ensure mini map camera depth is heavier than the player's camera.
        _minimapCamera.depth = PlayerCamera.depth + 1;

        // Clear Flags: Depth only.
        _minimapCamera.clearFlags = CameraClearFlags.Depth;
        _minimapCamera.transform.rotation = Quaternion.Euler(90, -90, 0);
    }

    void Update()
    {
    }
}
