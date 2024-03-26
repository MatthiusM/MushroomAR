using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFact : MonoBehaviour
{
    private readonly string[] texts = {
        "The mushroom cap's primary role is to safeguard the spores.",
        "Mushroom caps never take on a cubical shape; conical, umbonate, and campanulate are more common forms.",
        "Some mushroom caps are adorned with scales, adding to their diverse textures.",
        "The underside of a mushroom cap often features gills, crucial for the mushroom's structure.",
        "The underside of a mushroom cap can help differentiate between edible and poisonous varieties.",
        "Mushroom gills can sometimes be attached to the stem, or completely free from it.",
        "The ultimate goal of mushroom spores is to reproduce, giving rise to new mushrooms.",
        "Mushroom spores can travel through air and hitch rides on insects to spread far and wide.",
        "Mushrooms don't rely on growing towards sunlight as a means to disperse spores, unlike plants.",
        "The mushroom stem, or 'stipe', plays a pivotal role in supporting both the cap and the spores.",
        "The annulus on a mushroom stem is actually a ring left from the mushroom's partial veil.",
        "The term 'stipe' refers specifically to the stem of a mushroom, distinguishing it from other parts like the mycelium or hyphae."
    };


    private TextMeshProUGUI textDisplay;

    private void Awake()
    {
        if (!TryGetComponent<TextMeshProUGUI>(out textDisplay))
        {
            Debug.LogWarning("TextMeshProUGUI component not found.");
        }
    }

    private void OnEnable()
    {
        DisplayRandomText();
    }

    void DisplayRandomText()
    {
        if (texts.Length > 0 && textDisplay != null)
        {
            string selectedText = texts[Random.Range(0, texts.Length)];
            textDisplay.text = selectedText;
        }
    }
}
