using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    private Button button;

    protected virtual void Awake()
    {
        if (!TryGetComponent<Button>(out button))
        {
            Debug.LogError("No Button component found on the GameObject.");
        }
    }

    protected virtual void OnEnable()
    {
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    protected virtual void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }

    protected abstract void OnButtonClick();
}
