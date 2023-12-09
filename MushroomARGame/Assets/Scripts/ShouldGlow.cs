using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldGlow : MonoBehaviour
{
    [SerializeField]
    private Material glowTexture;
    [SerializeField]
    private Material regularTexture;

    public bool isGlowing;

    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        objectRenderer.material = isGlowing ? glowTexture : regularTexture;
    }
}
