using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    [Header("Bind this to a controller action (e.g., RightHand/SecondaryButton)")]
    public InputActionReference action;

    [Header("Light colors")]
    public Color colorA = Color.white;
    public Color colorB = Color.red;

    private Light pointLight;
    private bool isColorA = true;

    private void Start()
    {
        pointLight = GetComponent<Light>();

        if (pointLight == null)
        {
            Debug.LogError("LightSwitch requires a Light component on the same GameObject.");
        }
    }

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
        if (pointLight == null) return;

        if (isColorA)
        {
            pointLight.color = colorB;
        }
        else
        {
            pointLight.color = colorA;
        }

        isColorA = !isColorA;
    }
}
