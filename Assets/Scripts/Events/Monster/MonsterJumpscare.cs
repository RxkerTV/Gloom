using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterJumpscare : MonoBehaviour
{
    public bool jumpscareHasPlayed;
    void Start()
    {
        jumpscareHasPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&jumpscareHasPlayed==false)
        {
            CrawlScare.Instance.TriggerJumpscare();
            jumpscareHasPlayed=true;
            
        }
    }

}
