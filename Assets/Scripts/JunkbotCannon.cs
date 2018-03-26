using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code for this script makes use of heavy reference from the Unity Tanks Tutorial, Found below:
// https://unity3d.com/learn/tutorials/s/tanks-tutorial

public class JunkbotCannon : MonoBehaviour {

    private Junkbot parentJunkbot;
    private AudioSource audio;
    private float cannonInput;
    private string cannonInputButton;
    [SerializeField]
    private float launchForce;
    private bool canFire = true;

    //limited ammunition variables
    public int maxShots;
    private int ShotsFired;
    //have a minimum time between shots
    [SerializeField]
    private float cannonCooldownSeconds;

    [SerializeField]
    private Transform fireTransform;
    [SerializeField]
    private Rigidbody projectile;

    [SerializeField]
    private ParticleSystem launchParticles;

	// Use this for initialization
	void Start () 
	{
        parentJunkbot = GetComponentInParent<Junkbot>();
        cannonInputButton = "Cannon" + parentJunkbot.PlayerNumber;

        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        //if the bumper is pressed, and the ability is off cooldown, and control is enabled, and you've fired fewer than your maximum number of shots
        if (Input.GetButtonDown(cannonInputButton) && canFire && parentJunkbot.IsControlEnabled && ShotsFired < maxShots)
        {
            Fire();
        }
	}

    void Fire()
    {
        //start cooldown so you can't just chain shots together, also increment shots fired
        canFire = false;
        ShotsFired++;
        StartCoroutine(CannonCooldownCoroutine());

        //create the projectile and give it a velocity
        //TODO Fix projectile spawn positioning bug
        Rigidbody projectileInstance = Instantiate(projectile, fireTransform.position, fireTransform.rotation);
        projectileInstance.velocity = launchForce * fireTransform.forward;

        //play the toaster sound effect
        audio.Play();

        //TODO: Figure out how to launch the particle effect
        ////Play launch particle cloud (Shell explosion)
        //launchParticles.transform.parent = null;
        //launchParticles.Play();
        //Debug.Log("Cue Particle Effect");
        ////Destroy(launchParticles.gameObject, launchParticles.duration);
    }

    private IEnumerator CannonCooldownCoroutine()
    {
        yield return new WaitForSeconds(cannonCooldownSeconds);
        canFire = true;
    }

    public void ResetShots()
    {
        ShotsFired = 0;
    }
}
