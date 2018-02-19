﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junkbot : MonoBehaviour 
{
    [SerializeField]
    private float MoveForce = 100;
    [SerializeField]
    private float TurnSpeed = 100;
    new Rigidbody rigidbody;
    float XInput;
    float YInput;
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
        Turn();
        Move();
    }
    private void GetInput()
    {
        XInput = Input.GetAxis("Horizontal");
        YInput = Input.GetAxis("Vertical");
    }
    private void Move()
    {
        rigidbody.AddRelativeForce(new Vector3(0, 0, YInput * MoveForce * Time.fixedDeltaTime));
    }
    private void Turn()
    {
        transform.Rotate(new Vector3(0, XInput * TurnSpeed * Time.fixedDeltaTime,0));
    }
}
