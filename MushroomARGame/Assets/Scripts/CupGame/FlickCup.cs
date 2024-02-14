using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickCup : MonoBehaviour
{
    private readonly float flickForce = 3f;
    private readonly float torqueForce = 0.5f;
    private Vector3 flickDirection = new(0f, 300f, 300f);
    private Vector3 torqueDirection = new(0, 0, -1);
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody rb;
    public System.Action onFlickAction;

    private static FlickCup currentActiveCup = null;
    public static FlickCup CurrentActiveCup
    {
        get { return currentActiveCup; }
        private set { currentActiveCup = value; }
    }

    void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();      
    }

    private void OnEnable()
    {
        onFlickAction += ActivateCup;
    }

    private void OnDisable()
    {
        onFlickAction -= ActivateCup;
    }

    public void ActivateCup()
    {
        currentActiveCup = this;
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
    }
}
