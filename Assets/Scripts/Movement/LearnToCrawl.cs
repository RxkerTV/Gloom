//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LearnToCrawl : MonoBehaviour
//{
//    public typewriterUI typewriterUI;

//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//                Debug.Log("Doesn't have rope");
//                UI.Instance.NoRope.SetActive(true);
//                StartTypewriterEffect("You need a rope to use this.");
            
//        }
//    }
//    private IEnumerator TextDeleteDelay()
//    {
//        yield return new WaitForSeconds(2.5f);
//        UI.Instance.NoRope.SetActive(false);
//    }

//    private void StartTypewriterEffect(string message)
//    {
//        if (typewriterUI != null)
//        {
//            typewriterUI.writer = message; // Set new message
//            typewriterUI.StartTypewriter(); // Start typing effect
//        }
//    }

//    //Lock playermovment(done)
//    //start animation of playercam turning towards the monster.(Done)
//    //Change the music and play audio to make it more intense. (Done)
//    //Show the monster running at you and start making the playercam pan back to the start(done)
//    //Unlock movement and prompt the player to run(done)
//    //After the player runs for a second play an audio of a object falling
//    //Player turns around and sees that hes been caved in
//    //An audio prompts saying something like "What the fuck was that"


//    //If at anypoint the monster catches the player do a jumpscare(do last since its the biggest?)
//}
