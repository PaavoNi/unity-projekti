using UnityEngine;
using UnityEngine.InputSystem;

public class Quit : MonoBehaviour
{
    [Header("Bind this to a controller action (e.g., RightHand/PrimaryButton)")]
    public InputActionReference action;

    private void OnEnable()
    {
        if (action != null && action.action != null)
        {
            action.action.Enable();
            action.action.performed += OnQuitPerformed;
        }
    }

    private void OnDisable()
    {
        if (action != null && action.action != null)
        {
            action.action.performed -= OnQuitPerformed;
            action.action.Disable();
        }
    }

    private void OnQuitPerformed(InputAction.CallbackContext ctx)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
