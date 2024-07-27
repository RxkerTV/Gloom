using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    int index = 0; // Start from the first page
    private bool Canrotate = true; // Initially, allow rotation

    public void RotateForward()
    {
        if (!Canrotate || index >= pages.Count - 1) return; // If rotation is not allowed or index is out of bounds, return immediately

        index++;
        float angle = 180;
        StartCoroutine(Rotate(angle));
    }

    public void RotateBack()
    {
        if (!Canrotate || index <= 0) return; // If rotation is not allowed or index is out of bounds, return immediately

        index--;
        float angle = 0;
        StartCoroutine(Rotate(angle));
    }

    IEnumerator Rotate(float angle)
    {
        Canrotate = false; // Disable further rotations while one is in progress

        float value = 0f;
        Quaternion initialRotation = pages[index].rotation;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

        while (value < 1f)
        {
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(initialRotation, targetRotation, value);
            yield return null;
        }

        pages[index].rotation = targetRotation; // Ensure final rotation is exact
        Canrotate = true; // Re-enable rotation after the current one completes
    }

    private void Update()
    {
        if (index < pages.Count)
        {
            // Update logic here if needed
        }
    }
}
