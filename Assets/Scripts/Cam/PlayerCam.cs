using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : SingletonMonoBehaviour<PlayerCam>
{
    [Header("Floats")]
    public float sensX;
    public float sensY;
    public float fadeDuration = 1f; // Duration of the fade

    public LayerMask interactableLayer;

    [Header("Transforms")]
    public Transform orientation;
    public Transform PlayerWhole;
    public Transform rockDropOffLocation;
    public Transform inventoryParent; // The parent object of the inventory item slots

    public object FlashLight;
    public Ray ray;
    float xRotation;
    float yRotation;

    [Header("Bools")]
    public bool InventoryOn;
    public bool inReach;
    public bool HasRope;
    public bool unlocked;
    public bool locked;
    public bool hasKey;
    public bool doorOpen;

    public Image fadeImage; // Reference to the UI Image for fading

    [Header("ObjectsForInteraction")]
    public GameObject flashlight;
    public GameObject Rope;
    public GameObject RockWithoutRope;
    public GameObject RockWithRope;
    public GameObject Key;
    public GameObject[] doors;
    public GameObject[] doorLocked;

    public Animator door;
    public AudioSource doorSoundOpen;
    public AudioSource doorSoundClose;

    private void Start()
    {
        UI.Instance.interactText.SetActive(false);
        InventoryOn = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ReachCheck();
        inReach = false;

        if (!InventoryOn && LookMode.Instance.PauseMenuOn == false)
        {
            // get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, 2.5f, interactableLayer))
        {
            #region doors
            foreach (GameObject doorObject in doors)
            {
                if (hit.collider.gameObject == doorObject)
                {
                    inReach = true;

                    if (inReach && Input.GetButtonDown("Interact") && doorOpen == false)
                    {
                        DoorOpens();
                        doorOpen = true;
                        Debug.Log("Open");
                    }
                    else if (inReach && Input.GetButtonDown("Interact") && doorOpen == true)
                    {
                        DoorCloses();
                        doorOpen = false;
                        Debug.Log("Closed");
                    }
                    break; // Exit the loop once the door is found
                }
            }

            foreach (GameObject doorObject in doorLocked)
            {
                if (hit.collider.gameObject == doorObject)
                {
                    inReach = true;

                    if (inReach && Input.GetButtonDown("Interact") && !doorOpen && hasKey)
                    {
                        DoorOpens();
                        doorOpen = true;
                        locked = false;
                        unlocked = true;
                    }
                    else if (inReach && Input.GetButtonDown("Interact") && doorOpen && unlocked)
                    {
                        DoorCloses();
                        doorOpen = false;
                    }
                    else if (inReach && Input.GetButtonDown("Interact") && !hasKey && !doorOpen)
                    {
                        Debug.Log("Locked");
                    }
                }
            }

            if (hit.collider.gameObject == Key)
            {
                inReach = true;
                if (inReach && Input.GetButtonDown("Interact") && Key.activeInHierarchy == true)
                {
                    Key.SetActive(false);
                    DoorsLocked.Instance.hasKey = true;
                    UI.Instance.interactText.SetActive(false);
                }
            }
            #endregion

            if (hit.collider.gameObject == flashlight)
            {
                inReach = true;
                if (Input.GetButtonDown("Interact"))
                {
                    flashlight.SetActive(false);
                    LookMode.Instance.flashlightinInv = true;
                    UI.Instance.interactText.SetActive(false);
                }
            }

            if (hit.collider.gameObject == Rope)
            {
                inReach = true;
                if (Input.GetButtonDown("Interact"))
                {
                    HasRope = true;
                    Rope.SetActive(false);
                    UI.Instance.interactText.SetActive(false);
                }
            }

            if (hit.collider.gameObject == RockWithoutRope)
            {
                inReach = true;
                if (Input.GetButtonDown("Interact") && HasRope == true)
                {
                    RockWithoutRope.SetActive(false);
                    RockWithRope.SetActive(true);
                    UI.Instance.interactText.SetActive(false);
                }
            }

            if (hit.collider.gameObject == RockWithRope)
            {
                inReach = true;
                if (Input.GetButtonDown("Interact") && HasRope == true)
                {
                    UI.Instance.interactText.SetActive(false);
                    StartCoroutine(FadeAndMovePlayer());
                }
            }

           
        }
    }

        void DoorOpens()
        {
            // Debug.Log("It Opens");
            door.SetBool("Open", true);
            door.SetBool("Closed", false);
            doorSoundOpen.Play();
        }

        void DoorCloses()
        {
            // Debug.Log("It Closes");
            door.SetBool("Open", false);
            door.SetBool("Closed", true);
            doorSoundClose.Play();
        }

        void ReachCheck()
        {
            if (inReach == true)
            {
                UI.Instance.interactText.SetActive(true);
            }
            if (inReach == false)
            {
                UI.Instance.interactText.SetActive(false);
            }
        }

        private IEnumerator FadeToBlack()
        {
            if (fadeImage == null)
                yield break;

            float elapsedTime = 0f;
            Color color = fadeImage.color;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                fadeImage.color = color;
                yield return null; 
            }

            // Ensure it is fully black
            color.a = 1f;
            fadeImage.color = color;
        }

        private IEnumerator FadeAndMovePlayer()
        {
            // Fade to black
            yield return StartCoroutine(FadeToBlack());

            // Move player to the new position after fading to black
            PlayerWhole.position = rockDropOffLocation.position;
            Debug.Log("Player moved to: " + PlayerWhole.position);

            // Fade back to transparent
            yield return StartCoroutine(FadeBackSequence());
        }

        private IEnumerator FadeBackSequence()
        {
            yield return new WaitForSeconds(1f); // Wait 1 second after fading to black
            StartCoroutine(FadeToTransparent());
        }

        private IEnumerator FadeToTransparent()
        {
            if (fadeImage == null)
                yield break;

            float elapsedTime = 0f;
            Color color = fadeImage.color;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
                fadeImage.color = color;
                yield return null;
            }

            // Ensure it is fully transparent
            color.a = 0f;
            fadeImage.color = color;
        }
    }
