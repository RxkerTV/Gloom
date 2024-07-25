using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNoteData", menuName = "ScriptableObjects/NoteData", order = 1)]
public class NoteData : Item
{
    public string name;
    public string[] sentences;
}
