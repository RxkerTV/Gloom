using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNoteComponent : MonoBehaviour
{
    void Start()
    {
        Note note = GetComponent<Note>();
        if (note != null)
        {
            Debug.Log("Note component found.");
        }
        else
        {
            Debug.Log("No Note component found.");
        }
    }
}