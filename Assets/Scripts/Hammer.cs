using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {


    //use ratios to set the rotation of the hammer comparatively to the pull of the trigger

    private string hammerInputAxis;
    private float hammerInput;
    private Junkbot parentJunkbot;
    private Quaternion hammerCurrentRotation;

    [SerializeField]
    private float maxRotationAngle;
    [SerializeField]
    private float hammerLerpSpeed;
	// Use this for initialization
	void Start ()
    {
        parentJunkbot = GetComponentInParent<Junkbot>();
        hammerInputAxis = "Hammer" + parentJunkbot.PlayerNumber;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetInput();
	}

    private void FixedUpdate()
    {
        SetHammer();       
    }

    void GetInput()
    {
        hammerInput = Input.GetAxis(hammerInputAxis);
    }

    void SetHammer()
    {
        //if the junkbot dies, let the hammer fall down and stay there
        if (parentJunkbot.IsAlive)
        {
            //set target rotation relative to the trigger input axis
            hammerCurrentRotation.eulerAngles = new Vector3(0, 0, maxRotationAngle * hammerInput);
        }
        else
        {
            hammerCurrentRotation.eulerAngles = new Vector3(0, 0, maxRotationAngle);
        }

        //lerp so that it doesn't just snap down
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, hammerCurrentRotation.eulerAngles, hammerLerpSpeed * Time.fixedDeltaTime);
        //Debug.Log("LEA: " + transform.localEulerAngles);
        //transform.localEulerAngles = hammerCurrentRotation.eulerAngles;
    }
}
