using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour {

    private bool hasCollided = false;
    private AudioSource audioSource;

    [SerializeField]
    private bool canRepeat;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canRepeat)
        {
            audioSource.Play();
        }
        else if (!hasCollided)
        {
            audioSource.Play();
            Debug.Log(this.name + " has collided with " + collision);
            hasCollided = true;
        }
    }
}
