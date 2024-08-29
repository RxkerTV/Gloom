using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CrawlScare : SingletonMonoBehaviour<CrawlScare>
{
    public Animator MonsterAnimationAndPlayerCam;
    public typewriterUI typewriterUI;
    public AudioSource MonsterSource;
    public AudioClip MonsterScream;
    public AudioSource RocksfallingSound;
    public bool HasntPlayed;
    public GameObject TheRake;
    public Camera mainCamera;
    public Camera secondaryCamera;
    public Camera thirdCamera;
    public GameObject RocksAndDust;

   // public Collider monsterCollider;

    private void Start()
    {
        Debug.Log("CrawlScare script started.");
        HasntPlayed = true;
        PlayerMovementAdvanced.Instance.canMove = true;
        TheRake.SetActive(false);
        MonsterAnimationAndPlayerCam.SetBool("hasntPlayed", true);

        if (typewriterUI == null)
        {
            Debug.LogError("TypewriterUI script is not assigned.");
        }

        // Initialize camera settings
        SwitchToMainCamera(); // Ensure main camera is active initially

        Debug.Log("Initial setup complete.");
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (HasntPlayed)
            {
                PlayerMovementAdvanced.Instance.canMove = false;
                TheRake.SetActive(true);
                Animations(); // Start the animations when the player enters the trigger.
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TextDeleteDelay());
        }

        // if player runs into the collider of the monster trigger jumpscare. HOW IS IT SO DAM HARD TO DO THIS
    }

    private void Animations()
    {
        MonsterAnimationAndPlayerCam.SetTrigger("PlayercamTowardsMonster");
        MonsterSource.Play();
        SwitchToSecondaryCamera(); // Switch to secondary camera when the animation starts
    }

    public void TriggerJumpscare()
    {
        Debug.Log("TriggerJumpscare called.");
        SwitchToThirdCamera();
        MonsterSource.mute = false;
        Debug.Log("Player entered monster's collider, playing jumpscare animation");
        PlayerMovementAdvanced.Instance.canMove = false; // Disable player movement
        MonsterAnimationAndPlayerCam.SetTrigger("jumpscareTrigger");
        MonsterSource.PlayOneShot(MonsterScream); // Play scream sound for the jumpscare
        StartCoroutine(EndJumpscare());
        MonsterAnimationAndPlayerCam.SetBool("hasPlayedJumpScare", true);
    }

    public IEnumerator EndJumpscare()
    {
        yield return new WaitForSeconds(3f); // Wait for the jumpscare animation to play out
        PlayerMovementAdvanced.Instance.canMove = true; // Re-enable player movement
        SwitchToMainCamera();
        // Additional logic can be added here
    }

    public void OnAnimationEnd()
    {
        PlayerMovementAdvanced.Instance.canMove = true;
        PlayerCam.Instance.CanTurn = true;
        UI.Instance.RunTXT.SetActive(true);
        StartTypewriterEffect("Run.");
        HasntPlayed = false;
        MonsterAnimationAndPlayerCam.SetBool("hasntPlayed", false);
        MonsterAnimationAndPlayerCam.SetTrigger("ChaseAnim");
        MonsterSource.mute = true;
        StartCoroutine(SwitchToMainCameraAfterDelay());

    }

    public void OnChaseAnimEnd()
    {
        MonsterSource.mute = true;
        RocksfallingSound.Play();
        TheRake.SetActive(false);
        RocksAndDust.SetActive(true);
    }

    private IEnumerator SwitchToMainCameraAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // Delay before switching back to the main camera
        SwitchToMainCamera();
    }

    private void SwitchToSecondaryCamera()
    {
        Debug.Log("Switching to Secondary Camera");
        mainCamera.enabled = false;
        secondaryCamera.enabled = true;
        thirdCamera.enabled = false;
    }

    private void SwitchToThirdCamera()
    {
        Debug.Log("Switching to Third Camera");
        mainCamera.enabled = false;
        secondaryCamera.enabled = false;
        thirdCamera.enabled = true;
    }

    private void SwitchToMainCamera()
    {
        Debug.Log("Switching to Main Camera");
        mainCamera.enabled = true;
        secondaryCamera.enabled = false;
        thirdCamera.enabled = false;
    }

    private IEnumerator TextDeleteDelay()
    {
        yield return new WaitForSeconds(1.5f);
        UI.Instance.RunTXT.SetActive(false);
    }

    private void StartTypewriterEffect(string message)
    {
        if (typewriterUI != null)
        {
            typewriterUI.writer = message; // Set new message
            typewriterUI.StartTypewriter(); // Start typing effect
        }
    }
}
