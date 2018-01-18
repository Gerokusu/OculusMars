using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSound : MonoBehaviour {

    private AudioSource ambientSound;

    void Start()
    {
        ambientSound = GetComponent<AudioSource>();
    }

    public void PlayAmbientSound ()
    {
        ambientSound.Play();
    }
}
