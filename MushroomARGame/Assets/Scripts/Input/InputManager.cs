using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Controls.ITouchControlsActions
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    public Action<Vector2> OnTapEvent;

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
        controls.TouchControls.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.TouchControls.Enable();
    }

    private void OnDisable()
    {
        controls.TouchControls.Disable();
    }

    public void OnTap(InputAction.CallbackContext context)
    {
        Debug.Log("tap");
        Vector2 tapPosition = context.ReadValue<Vector2>();
        OnTapEvent?.Invoke(tapPosition);
    }
}
