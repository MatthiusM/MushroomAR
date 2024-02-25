using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Controls.IPlayerActions
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    public delegate void TapAction(Vector2 position);
    public event TapAction OnTapEvent;

    private Controls controls;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        controls = new Controls();
        controls.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    public void OnTap(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 tapPosition = context.ReadValue<Vector2>();
            OnTapEvent?.Invoke(tapPosition);
        }
    }
}
