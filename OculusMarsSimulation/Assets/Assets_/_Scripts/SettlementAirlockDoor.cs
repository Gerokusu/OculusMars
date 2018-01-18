using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementAirlockDoor : MonoBehaviour {

    public DoorState state;

    private Animator animator;
    private AudioSource audioSource;
    private bool isLocked;
    
	void Start ()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        animator.speed = 0.28f;
        state = DoorState.Close;
    }

    void OnTriggerStay (Collider other)
    {
        if (Input.GetKeyDown(KeyCode.A) && other.CompareTag("Player"))
        { 
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (!isLocked && state == DoorState.Close)
        {
            state = DoorState.Open;
            animator.SetBool("Open", true);
            audioSource.Play();
        }
    }

    public void CloseDoor()
    {
        if (!isLocked && state == DoorState.Open)
        {
            animator.SetBool("Open", false);
            audioSource.Play();
        }
    }

    public void LockDoor()
    {
        isLocked = true;
    }
}

public enum DoorState { Open, Close };
