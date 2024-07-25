using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.U2D.ScriptablePacker;

public class Paper : MonoBehaviour
{
    public GameObject Paperr;
    public AudioSource PickUpPaper;
    private bool inReach;
    public NoteData noteData;
    public LayerMask interactableLayer;


    // Start is called before the first frame update
    void Start()
    {
        noteData = ScriptableObject.CreateInstance<NoteData>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            UI.Instance.interactText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            UI.Instance.interactText.SetActive(false);
        }

    }
    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact") && Paperr.activeInHierarchy == true)
        {
            Paperr.SetActive(false);
            UI.Instance.interactText.SetActive(false);
            PickUpPaper.Play();
        }
    }
}
