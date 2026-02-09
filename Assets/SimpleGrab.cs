using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleGrab : MonoBehaviour
{
    [Header("Input (Quest 2)")]
    public InputActionReference grabAction;   // esim. Grip

    [Header("Detection")]
    public string handTag = "Hand";

    private Transform nearbyHand = null;
    private Transform originalParent = null;
    private bool isGrabbed = false;

    void Start()
    {
        originalParent = transform.parent;
    }

    private void OnEnable()
    {
        if (grabAction != null && grabAction.action != null)
        {
            grabAction.action.Enable();
            grabAction.action.performed += OnGrabPerformed;
            grabAction.action.canceled += OnGrabCanceled;
        }
    }

    private void OnDisable()
    {
        if (grabAction != null && grabAction.action != null)
        {
            grabAction.action.performed -= OnGrabPerformed;
            grabAction.action.canceled -= OnGrabCanceled;
            grabAction.action.Disable();
        }
    }

    private void OnGrabPerformed(InputAction.CallbackContext ctx)
    {
        if (isGrabbed) return;
        if (nearbyHand == null) return;

        Grab(nearbyHand);
    }

    private void OnGrabCanceled(InputAction.CallbackContext ctx)
    {
        if (!isGrabbed) return;

        Release();
    }

    void Grab(Transform hand)
    {
        isGrabbed = true;
        transform.SetParent(hand, worldPositionStays: true);
    }

    void Release()
    {
        isGrabbed = false;
        transform.SetParent(originalParent, worldPositionStays: true);
        nearbyHand = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(handTag))
        {
            nearbyHand = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(handTag) && other.transform == nearbyHand && !isGrabbed)
        {
            nearbyHand = null;
        }
    }
}
