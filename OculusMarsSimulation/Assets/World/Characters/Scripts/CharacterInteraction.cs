using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public float range = 4.0F;

    public Transform controllerCenter;
    public Transform controllerLeft;
    public GUIButton controllerLeftButton;
    public Transform controllerRight;
    public GUIButton controllerRightButton;

    public Trigger triggerAimed;
    public Item itemAimed;
    public Item itemGrabbed;

    private Transform itemGrabbedParent;

    private bool isTriggeredLeft = false;
    private bool isTriggeredRight = false;

    public void Update()
    {
        if(controllerCenter != null)
        {
            GetAimed(controllerLeft);
            if(itemAimed == null && triggerAimed == null)
            {
                GetAimed(controllerRight);
            }

            bool triggerLeft = (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0 && !isTriggeredLeft);
            isTriggeredLeft = (triggerLeft) ? true : (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0);
            bool isLeftPressed = (Input.GetButtonDown("InteractLeft") || triggerLeft) && controllerLeftButton != null && !controllerLeftButton.isAnimating;
            if (isLeftPressed)
            {
                controllerLeftButton.Animate();
            }

            bool triggerRight = (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0 && !isTriggeredRight);
            isTriggeredRight = (triggerRight) ? true : (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0);
            bool isRightPressed = (Input.GetButtonDown("InteractRight") || triggerRight) && controllerRightButton != null && !controllerRightButton.isAnimating;
            if (isRightPressed)
            {
                controllerRightButton.Animate();
            }
            
            if (isLeftPressed || isRightPressed)
            {
                if(triggerAimed != null)
                {
                    triggerAimed.isActivated = true;
                }
                else if(itemGrabbed == null)
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
        if (origin != null && itemGrabbed == null)
        {
            RaycastHit hit;
            Ray ray = new Ray(origin.position, origin.forward);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance <= range)
                {
                    triggerAimed = hit.collider.gameObject.GetComponent<Trigger>();
                    itemAimed = hit.collider.gameObject.GetComponent<Item>();
                }
                else
                {
                    triggerAimed = null;
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
