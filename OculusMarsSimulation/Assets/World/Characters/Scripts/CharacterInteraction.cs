﻿using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public float range = 4.0F;

    public Transform controllerCenter;
    public Transform controllerLeft;
    public GUIButton controllerLeftButton;
    public Transform controllerRight;
    public GUIButton controllerRightButton;

    public Item itemAimed;
    public Item itemGrabbed;

    private Transform itemGrabbedParent;

    public void Update()
    {
        if(controllerCenter != null)
        {
            GetAimed(controllerLeft);
            if(itemAimed == null)
            {
                GetAimed(controllerRight);
            }

            bool isLeftPressed = Input.GetButtonDown("InteractLeft") && controllerLeftButton != null && !controllerLeftButton.isAnimating;
            if (isLeftPressed)
            {
                controllerLeftButton.Animate();
            }

            bool isRightPressed = Input.GetButtonDown("InteractRight") && controllerRightButton != null && !controllerRightButton.isAnimating;
            if (isRightPressed)
            {
                controllerRightButton.Animate();
            }
            
            if (isLeftPressed || isRightPressed)
            {
                if (itemGrabbed == null)
                {
                    Grab();
                }
                else
                {
                    Throw();
                }
            }
        }
    }

    public void GetAimed(Transform origin)
    {
        if(origin != null && itemGrabbed == null)
        {
            RaycastHit hit;
            Ray ray = new Ray(origin.position, origin.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance <= range)
                {
                    itemAimed = hit.collider.gameObject.GetComponent<Item>();
                }
                else
                {
                    itemAimed = null;
                }
            }
        }
    }
    
    public void Grab()
    {
        if(itemAimed != null)
        {
            itemGrabbed = itemAimed;
            itemGrabbedParent = itemGrabbed.transform.parent;

            itemGrabbed.transform.SetParent(controllerCenter);
            itemGrabbed.transform.localPosition = new Vector3(0, 0, 0.5F);

            Rigidbody rigidbody = itemGrabbed.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
            }

            itemAimed = null;
        }
    }

    public void Throw()
    {
        if (itemGrabbed != null)
        {
            itemGrabbed.transform.SetParent(itemGrabbedParent);
            itemGrabbed.forceThrow = transform.forward * 20;

            Rigidbody rigidbody = itemGrabbed.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
            }

            itemGrabbed = null;
        }
    }
}