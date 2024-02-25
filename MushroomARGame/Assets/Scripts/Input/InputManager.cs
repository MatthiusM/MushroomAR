using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Controls.ITouchControlsActions
{
    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();

                if (instance == null)
                {
                    GameObject inputManagerObject = new("InputManager");
                    instance = inputManagerObject.AddComponent<InputManager>();
                }
            }

            return instance;
        }
    }

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
        if (context.performed)
        {
            Vector2 tapPosition = context.ReadValue<Vector2>();
            OnTapEvent?.Invoke(tapPosition);
        }
    }
}
