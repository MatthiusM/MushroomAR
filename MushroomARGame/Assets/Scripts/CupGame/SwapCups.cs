using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCups : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cups; 

    [SerializeField]
    private float swapDuration = 2f; 

    void Start()
    {
        StartCoroutine(Swap()); 
    }

    IEnumerator Swap()
    {
        while (true)
        {
            GameObject cup1 = cups[Random.Range(0, cups.Length)];
            GameObject cup2;
            do
            {
               
                cup2 = cups[Random.Range(0, cups.Length)];
            } while (cup1 == cup2);

            yield return StartCoroutine(SwapCupsOverTime(cup1, cup2, swapDuration));

            // Wait for 1 second before starting the next swap.
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SwapCupsOverTime(GameObject cup1, GameObject cup2, float duration)
    {
        Vector3 cup1Pos = cup1.transform.position;
        Vector3 cup2Pos = cup2.transform.position;
        Vector3 center = (cup1Pos + cup2Pos) / 2;

        // Record the start time of the animation.
        float startTime = Time.time;

        // Continue the animation for the specified duration.
        while (Time.time - startTime < duration)
        {

            float t = (Time.time - startTime) / duration;
            float angle = Mathf.Lerp(0, Mathf.PI, t);

            // Calculate the new positions of the cups around the pivot.
            Vector3 pos1 = RotatePointAroundPivot(cup1Pos, center, new Vector3(0, angle * Mathf.Rad2Deg, 0));
            Vector3 pos2 = RotatePointAroundPivot(cup2Pos, center, new Vector3(0, angle * Mathf.Rad2Deg, 0));

            // Update the positions of the cups.
            cup1.transform.position = pos1;
            cup2.transform.position = pos2;

            // Yield until the next frame.
            yield return null;
        }

        // Swap the cups' positions at the end of the animation.
        cup1.transform.position = cup2Pos;
        cup2.transform.position = cup1Pos;
    }

    // rotate a point around a pivot by a given angle.
    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }
}
