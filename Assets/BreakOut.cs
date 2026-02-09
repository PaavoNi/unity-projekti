using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    [Header("Bind this to a controller action (e.g., LeftHand/PrimaryButton)")]
    public InputActionReference action;

    [Header("What to move (usually XR Origin / XR Rig)")]
    public Transform playerRig;

    [Header("Teleport targets")]
    public Transform insidePoint;
    public Transform outsidePoint;

    [Header("If true, also match rotation when teleporting")]
    public bool matchRotation = true;

    private bool isOutside = false;

    private void OnEnable()
    {
        if (action != null && action.action != null)
        {
            action.action.Enable();
            action.action.performed += OnActionPerformed;
        }
    }

    private void OnDisable()
    {
        if (action != null && action.action != null)
        {
            action.action.performed -= OnActionPerformed;
            action.action.Disable();
        }
    }

    private void OnActionPerformed(InputAction.CallbackContext ctx)
    {
        if (playerRig == null || insidePoint == null || outsidePoint == null)
        {
            Debug.LogError("BreakOut: Assign playerRig, insidePoint, and outsidePoint in the Inspector.");
            return;
        }

        Transform target = isOutside ? insidePoint : outsidePoint;

        // Move
        playerRig.position = target.position;

        // Optionally rotate
        if (matchRotation)
            playerRig.rotation = target.rotation;

        // Toggle state for next press
        isOutside = !isOutside;
    }
}
