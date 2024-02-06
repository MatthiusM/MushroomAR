using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class PlacementButton : BaseButton
{
    private Image image;
    bool canPlace = false;

    public event Action<bool> OnCanPlaceChanged;

    private void Start()
    {
        image = GetComponent<Image>();
        ChangeColour();
    }

    protected override void OnButtonClick()
    {
        canPlace = !canPlace;
        ChangeColour();
        OnCanPlaceChanged?.Invoke(canPlace);
    }

    private void ChangeColour()
    {
        image.color = canPlace ? Color.cyan : Color.white;
    }
}
