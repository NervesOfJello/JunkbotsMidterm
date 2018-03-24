using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junkbot : MonoBehaviour 
{
    [SerializeField]
    private float moveForce = 100;
    [SerializeField]
    private float turnSpeed = 100;
    new Rigidbody rigidbody;
    private float xInput;
    private float yInput;

    //variables for groundcheck
    //isOnGround is changed in the groundcheck script
    public bool isOnGround = true;

    //very important boolean
    //Since it's a binary value, it really doesn't need a modified setter and it needs to be accessible outside of the script
    //So making it public is probably okay? Maybe ask D.A. later. Definitely don't want it exposed to the editor
    [HideInInspector]
    public bool isAlive = true;

    //input variables/axes
    //TODO Manage input axes in code as variables of the player number so they aren't hard-coded into the Junkbot Script
    //As in axes based on player order in a manager-generated list. Will become more relevant as the gamemanager script is made
    [SerializeField]
    public string PlayerNumber;

    private string horizontalInputAxis
    {
        get { return "Horizontal" + PlayerNumber; }
    }

    private string verticalInputAxis
    {
        get { return "Vertical" + PlayerNumber; }
    }

    // Use this for initialization
    void Start () 
	{
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        GetInput();
	}

    //handle physics calculations
    private void FixedUpdate()
    {
        //TODO Handle the state "isAlive" more elegantly than this. This version only for testing
        if (isAlive)
        {
            Turn();
            Move();
        }
    }
    private void GetInput()
    {
        xInput = Input.GetAxis(horizontalInputAxis);
        yInput = Input.GetAxis(verticalInputAxis);
    }
    //moves the junkbot forward as long as it's on the ground
    private void Move()
    {
        if (isOnGround)
        {
            rigidbody.AddRelativeForce(new Vector3(0, 0, yInput * moveForce * Time.fixedDeltaTime));
        }
    }
    //turns the junkbot
    private void Turn()
    {
        transform.Rotate(new Vector3(0, xInput * turnSpeed * Time.fixedDeltaTime,0));
    }
}
