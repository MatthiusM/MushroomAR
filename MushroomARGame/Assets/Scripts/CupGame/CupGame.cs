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


    private void OnEnable()
    {
        CupGameManager.Instance.AddOnEnter(CupGameStates.Start, StartGame);
        CupGameManager.Instance.AddOnEnter(CupGameStates.PlayRound, PlayRound);
        CupGameManager.Instance.AddOnEnter(CupGameStates.Picking, RemoveMushroomParent);
    }

    private void OnDisable()
    {
        CupGameManager.Instance.AddOnExit(CupGameStates.Start, StartGame);
        CupGameManager.Instance.AddOnExit(CupGameStates.PlayRound, PlayRound);
        CupGameManager.Instance.AddOnExit(CupGameStates.Picking, RemoveMushroomParent);
    }

    private void RemoveMushroomParent()
    {
        StartCoroutine(ParentMushroomToCup(false));
    }

    private void StartGame()
    {
        StartCoroutine(StartSequence());
    }
    private void PlayRound()
    {
        StartCoroutine(PlayRoundSequence());
    }

    private IEnumerator StartSequence()
    {
        // Lift cups
        yield return MoveCups(Vector3.up * liftHeight);
        yield return MoveMushroomToCup();
        // Lower cups
        yield return MoveCups(Vector3.down * liftHeight);
        yield return ParentMushroomToCup(true);

        CupGameManager.Instance.SwitchState(CupGameStates.PlayRound);
    }
    private IEnumerator PlayRoundSequence()
    {
        yield return ParentMushroomToCup(true);

        yield return SwapCupsInCircle(UnityEngine.Random.Range(6, 9), 0.5f);

        CupGameManager.Instance.SwitchState(CupGameStates.Picking);
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
        targetCup.GetComponent<IMushroomParent>().SetAsMushroomParent(true);
        yield return null;
    }    

    private IEnumerator SwapCupsInCircle(int swaps, float swapDuration)
    {
        int lastFirstCupIndex = -1; 
        int lastSecondCupIndex = -1;

        for (int i = 0; i < swaps; i++)
        {
            // Randomly select two different cups to swap, avoiding the last swapped pair
            int firstCupIndex, secondCupIndex;
            do
            {
                firstCupIndex = UnityEngine.Random.Range(0, cups.Length);
                secondCupIndex = UnityEngine.Random.Range(0, cups.Length);
            } while (firstCupIndex == secondCupIndex ||
                     (firstCupIndex == lastFirstCupIndex && secondCupIndex == lastSecondCupIndex) ||
                     (firstCupIndex == lastSecondCupIndex && secondCupIndex == lastFirstCupIndex));

            GameObject firstCup = cups[firstCupIndex];
            GameObject secondCup = cups[secondCupIndex];

            lastFirstCupIndex = firstCupIndex;
            lastSecondCupIndex = secondCupIndex;

            Vector3 midpoint = (firstCup.transform.position + secondCup.transform.position) / 2 + Vector3.up * 0.5f;

            float elapsedTime = 0;
            Vector3 startFirstCupPos = firstCup.transform.position;
            Vector3 startSecondCupPos = secondCup.transform.position;

            while (elapsedTime < swapDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / swapDuration;

                // Calculate the angular position for each cup
                float angle = Mathf.Lerp(0, Mathf.PI, t);

                // Calculate new positions for each cup around the midpoint
                Vector3 firstCupNewPos = midpoint + Quaternion.Euler(0, -Mathf.Rad2Deg * angle, 0) * (startFirstCupPos - midpoint);
                Vector3 secondCupNewPos = midpoint + Quaternion.Euler(0, -Mathf.Rad2Deg * angle, 0) * (startSecondCupPos - midpoint);

                // Apply new positions
                firstCup.transform.position = firstCupNewPos;
                secondCup.transform.position = secondCupNewPos;

                yield return null;
            }
        }
    }

}
