using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private Junkbot junkbotParent;

	// Use this for initialization
	void Awake () 
	{
        junkbotParent = GetComponentInParent<Junkbot>();
	}

    //as long as the collider is in contact with the ground, isOnGround is set to true
    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Ground"))
        {
            junkbotParent.isOnGround = true;
        }
    }

    //when the collider leaves the ground, isOnGround is set to false
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            junkbotParent.isOnGround = false;
        }
    }
}
