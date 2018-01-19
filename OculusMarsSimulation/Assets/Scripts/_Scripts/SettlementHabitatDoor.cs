using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementHabitatDoor : MonoBehaviour {

    public Transform player;
    public float distanceOpen;

    private Animator animator;

	void Start ()
    {
        animator = GetComponent<Animator>();
    }
	
	void Update ()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= distanceOpen)
        {
            animator.SetBool("PlayerIsNearby", true);
        }
        else
        {
            animator.SetBool("PlayerIsNearby", false);
        }
    }
}
