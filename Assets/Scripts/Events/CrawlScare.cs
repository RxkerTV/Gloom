using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlScare : MonoBehaviour
{
    public Animator MonsterAnimationAndPlayerCam;
    public typewriterUI typewriterUI;
    public AudioSource MonsterSource;
    public AudioClip MonsterScream;

    private void Start()
    {
        if (typewriterUI == null)
        {
            Debug.LogError("TypewriterUI script is not assigned.");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovementAdvanced.Instance.canMove = false;
            PlayerCam.Instance.CanTurn = false;
            Animations(); // Start the animations when the player enters the trigger.
        }
    }

    private void Animations()
    {
        MonsterAnimationAndPlayerCam.SetTrigger("PlayercamTowardsMonster");
        MonsterSource.Play();
    }

    // This method will be called at the end of the animation using an Animation Event
    public void OnAnimationEnd()
    {

        PlayerMovementAdvanced.Instance.canMove = true;
        PlayerCam.Instance.CanTurn = true;
        UI.Instance.RunTXT.SetActive(true);
        StartTypewriterEffect("Run.");
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

    //Lock playermovment(done)
    //start animation of playercam turning towards the monster.(Done)
    //Change the music and play audio to make it more intense. (Done)
    //Show the monster running at you and start making the playercam pan back to the start(done)
    //Unlock movement and prompt the player to run(done)
    //After the player runs for a second play an audio of a object falling
    //Player turns around and sees that hes been caved in
    //An audio prompts saying something like "What the fuck was that"


    //If at anypoint the monster catches the player do a jumpscare(do last since its the biggest?)

}