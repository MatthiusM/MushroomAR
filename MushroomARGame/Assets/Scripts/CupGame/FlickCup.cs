using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlickCup : MonoBehaviour, IFlickable, IMushroomParent
{
    private readonly float flickForce = 3f;
    private readonly float torqueForce = 0.5f;
    private Vector3 flickDirection = new(0f, 300f, 300f);
    private Vector3 torqueDirection = new(0, 0, -1);
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody rb;
    private bool mushroomParent = false;
    private Camera arCamera;
    private LayerMask layerMask;

    private static FlickCup currentActiveCup = null;
    public static FlickCup CurrentActiveCup => currentActiveCup;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ActivateCup(Action onComplete)
    {
        if (currentActiveCup != null) return;

        onComplete?.Invoke();
        currentActiveCup = this;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        FlickCupAction();
    }

    void FlickCupAction()
    {
        rb.isKinematic = false;
        rb.AddForce(flickDirection.normalized * flickForce, ForceMode.Impulse);
        rb.AddTorque(torqueDirection * torqueForce, ForceMode.Impulse);
        rb.useGravity = true;

        StartCoroutine(RespawnCup());
    }

    IEnumerator RespawnCup()
    {
        yield return new WaitForSeconds(1);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        currentActiveCup = null;

        if (mushroomParent)
        {
            CupGameManager.Instance.SwitchState(CupGameStates.End);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            CupGameManager.Instance.SwitchState(CupGameStates.PlayRound);
        }
    }

    public void Flick(Action onComplete)
    {
        ActivateCup(onComplete);
    }

    public void SetAsMushroomParent(bool b)
    {
        mushroomParent = b;
    }
}
