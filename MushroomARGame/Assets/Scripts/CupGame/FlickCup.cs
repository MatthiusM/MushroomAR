using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private static FlickCup currentActiveCup = null;
    public static FlickCup CurrentActiveCup
    {
        get { return currentActiveCup; }
        private set { currentActiveCup = value; }
    }

    private CupGameManager cupGameManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cupGameManager = DebugUtility.GetComponentFromName<CupGameManager>("CupGame");
    }

    public void ActivateCup(Action onComplete)
    {
        if (currentActiveCup != null) return;

        currentActiveCup = this;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        FlickCupAction(onComplete);
    }

    void FlickCupAction(Action onComplete)
    {
        rb.isKinematic = false;
        rb.AddForce(flickDirection.normalized * flickForce, ForceMode.Impulse);
        rb.AddTorque(torqueDirection * torqueForce, ForceMode.Impulse);
        rb.useGravity = true;

        StartCoroutine(RespawnCup(onComplete));
    }

    IEnumerator RespawnCup(Action onComplete)
    {
        yield return new WaitForSeconds(1);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.useGravity = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        currentActiveCup = null;

        onComplete?.Invoke();
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
