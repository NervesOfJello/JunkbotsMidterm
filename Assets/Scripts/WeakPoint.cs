using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour {

    //will need reference to the Junkbot script so it can check whether it's Alive or not
    //All I want this to do is when the weakpoint collides with an object tagged "Weapon," it sets Junkbot.isAlive to "false"
    //Actually handle what it means to be dead in the junkbot script itself
    //This way, this script can be attached to any object

    private Junkbot parentJunkbot;
    private AudioSource audioSource;
	// Use this for initialization
	void Start ()
    {
        parentJunkbot = GetComponentInParent<Junkbot>();
        audioSource = GetComponent<AudioSource>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon" && parentJunkbot.IsAlive)
        {
            parentJunkbot.IsAlive = false;
            audioSource.Play();
        }
    }
}
