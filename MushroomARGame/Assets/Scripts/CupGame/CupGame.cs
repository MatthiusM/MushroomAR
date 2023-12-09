using System.Collections;
using UnityEngine;

public class CupGame : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cups; 
    [SerializeField]
    private GameObject mushroom; 

    private GameObject selectedCup; 
    private Vector3 yChange = new Vector3(0, 0.5f, 0); 
    private Vector3 originalMushroomPos; 
    private readonly float moveDuration = 1f; 

    void Start()
    {

        originalMushroomPos = mushroom.transform.position;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {

        yield return StartCoroutine(LiftCups());
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(MoveMushroomCup());
        yield return StartCoroutine(LowerCups());
    }

    IEnumerator LiftCups()
    {
        foreach (var cup in cups)
        {
            StartCoroutine(MoveGameObject(cup, cup.transform.position + yChange, moveDuration));
        }

        // Wait 1 second
        yield return new WaitForSeconds(1f);
    }

    IEnumerator MoveMushroomCup()
    {
        selectedCup = cups[Random.Range(0, cups.Length)];
        Vector3 targetPosition = new(selectedCup.transform.position.x, mushroom.transform.position.y, selectedCup.transform.position.z);
        yield return StartCoroutine(MoveGameObject(mushroom, targetPosition, moveDuration));
    }

    IEnumerator LowerCups()
    {
        foreach (var cup in cups)
        {
            // Move each cup downwards to its original position.
            StartCoroutine(MoveGameObject(cup, new Vector3(cup.transform.position.x, originalMushroomPos.y, cup.transform.position.z), moveDuration));
        }

        // Wait 1 second
        yield return new WaitForSeconds(1f);

        // Parent the mushroom to the selected cup.
        mushroom.transform.SetParent(selectedCup.transform);
    }

    // General coroutine for moving a GameObject to a target position over a specified duration.
    IEnumerator MoveGameObject(GameObject cup, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = cup.transform.position;
        float time = 0;

        while (time < duration)
        {
            // Smoothly interpolate the cup's position from its start position to the target position over the given duration.
            cup.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure the cup's position is exactly the target position after the movement is complete.
        cup.transform.position = targetPosition;
    }
}
