using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code for this script makes use of heavy reference from the Unity Tanks Tutorial, Found below:
// https://unity3d.com/learn/tutorials/s/tanks-tutorial

public class JunkbotCannon : MonoBehaviour {

    private Junkbot parentJunkbot;
    private float cannonInput;
    private string cannonInputButton;
    [SerializeField]
    private float launchForce;
    private bool canFire = true; //rename? bool tracks if the ability is off of cooldown but "isOffCooldown" is maybe vague? ask

    // make limited ammunition count now just in case you want to limit it (conceptually 1-2 shots per toaster
    [SerializeField]
    private int maxShots;
    private int shotsFired;

    [SerializeField]
    private Transform fireTransform;
    [SerializeField]
    private Rigidbody projectile;
    [SerializeField]
    private float cannonCooldownSeconds;

	// Use this for initialization
	void Start () 
	{
        parentJunkbot = GetComponentInParent<Junkbot>();
        cannonInputButton = "Cannon" + parentJunkbot.PlayerNumber;
	}
	
	// Update is called once per frame
	void Update () 
	{
        //if the bumper is pressed, and the ability is off cooldown, and you've fired fewer than your maximum number of shots
        if (Input.GetButtonDown(cannonInputButton) && canFire && shotsFired < maxShots)
        {
            Fire();
        }
	}

    void Fire()
    {
        //start cooldown so you can't just chain shots together, also increment shots fired
        canFire = false;
        //shotsFired++;
        StartCoroutine(CannonCooldownCoroutine());

        //create the projectile and give it a velocity
        //TODO Fix projectile spawn positioning bug
        Rigidbody projectileInstance = Instantiate(projectile, fireTransform.position, fireTransform.rotation);

        projectileInstance.velocity = launchForce * fireTransform.forward;
    }

    private IEnumerator CannonCooldownCoroutine()
    {
        yield return new WaitForSeconds(cannonCooldownSeconds);
        canFire = true;
    }
}
