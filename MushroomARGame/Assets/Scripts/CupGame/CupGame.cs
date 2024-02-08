using System;
using System.Collections;
using UnityEngine;

public class CupGame : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cups = new GameObject[3];

    [SerializeField]
    private GameObject mushroom;

    private readonly float liftHeight = 0.6f;
    private readonly float liftDuration = 1.0f;
    private readonly float moveMushroomDuration = 2.0f;
    private GameObject targetCup;

    private CupGameManager cupGameManager;

    public Action StartFinished;

    private void Start()
    {
        cupGameManager = GetComponent<CupGameManager>();
        cupGameManager.onGameStart += StartGame;
    }

    private void StartGame()
    {
        StartCoroutine(GameSequence());
    }

    private IEnumerator GameSequence()
    {
        yield return LiftCupsToHeight();
        yield return MoveMushroomToCup();
        yield return LowerCups();
        yield return ParentMushroomToCup(true);
    }

    private IEnumerator LiftCupsToHeight()
    {
        yield return MoveCups(Vector3.up * liftHeight);
    }

    private IEnumerator LowerCups()
    {
        yield return MoveCups(Vector3.down * liftHeight);
    }

    private IEnumerator MoveCups(Vector3 moveDirection)
    {
        Vector3[] originalPositions = new Vector3[cups.Length];
        Vector3[] targetPositions = new Vector3[cups.Length];

        // Record original and target positions
        for (int i = 0; i < cups.Length; i++)
        {
            if (cups[i] != null)
            {
                originalPositions[i] = cups[i].transform.position;
                targetPositions[i] = originalPositions[i] + moveDirection * liftHeight;
            }
        }

        // Smoothly move cups to target positions
        float elapsedTime = 0;
        while (elapsedTime < liftDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsedTime / liftDuration);
            for (int i = 0; i < cups.Length; i++)
            {
                if (cups[i] != null)
                {
                    cups[i].transform.position = Vector3.Lerp(originalPositions[i], targetPositions[i], t);
                }
            }
            yield return null;
        }
    }

    private IEnumerator MoveMushroomToCup()
    {
        // Choose a random cup
        targetCup = cups[UnityEngine.Random.Range(0, cups.Length)];

        Vector3 targetPosition = targetCup.transform.position;
        targetPosition.y = mushroom.transform.position.y;

        // Smoothly move the mushroom to the target position
        Vector3 startPosition = mushroom.transform.position;
        float elapsedTime = 0;
        while (elapsedTime < moveMushroomDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveMushroomDuration;
            mushroom.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
    }
    private IEnumerator ParentMushroomToCup(bool shouldParent)
    {
        mushroom.transform.SetParent(shouldParent ? targetCup.transform : null);
        yield return null;
    }
}
