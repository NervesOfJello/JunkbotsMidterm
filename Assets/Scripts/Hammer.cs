using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {


    //use ratios to set the rotation of the hammer comparatively to the pull of the trigger

    private string hammerInputAxis;
    private float hammerInput;
    private Junkbot parentJunkbot;
    private Quaternion hammerCurrentRotation;

    //audio variabls
    private AudioSource audioSource;
    private bool canWhoosh = true;
    [SerializeField]
    private float upperEffectThreshold = 0.8f;
    [SerializeField]
    private float lowerEffectThreshold = 0.3f;

    [SerializeField]
    private float maxRotationAngle;
    [SerializeField]
    private float hammerLerpSpeed;
	// Use this for initialization
	void Start ()
    {
        parentJunkbot = GetComponentInParent<Junkbot>();
        hammerInputAxis = "Hammer" + parentJunkbot.PlayerNumber;
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (parentJunkbot.IsControlEnabled)
        {
            GetInput();
        }

        Whoosh();
	}

    private void FixedUpdate()
    {
        SetHammer();       
    }

    //play the hammer swing sound when conditions are met
    public void Whoosh()
    {
        if (canWhoosh && hammerInput > upperEffectThreshold)
        {
            canWhoosh = false;
            audioSource.Play();
        }
        if (!canWhoosh && hammerInput < lowerEffectThreshold)
        {
            canWhoosh = true;
        }
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
