using UnityEngine;

public class RotateY : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second.")]
    public float degreesPerSecond = 30f;

    private void Update()
    {
        // Rotate around local Y axis, frame-rate independent
        transform.Rotate(0f, degreesPerSecond * Time.deltaTime, 0f, Space.Self);
    }
}
