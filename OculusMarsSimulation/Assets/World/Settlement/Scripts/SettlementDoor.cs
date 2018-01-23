using UnityEngine;
using UnityEngine.AI;

public class SettlementDoor : Animatable
{
    public bool isOpened = false;

    private AudioSource audioSource;
    private Animator animator;
    
	public void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        if(animator != null)
        {
            animator.speed = 0.28F;
        }

        if (isOpened)
        {
            isOpened = false;
            Animate("open");
        }
    }

    public void LateUpdate()
    {
        if(isAnimating)
        {
            if(animationType == "open" && !isOpened)
            {
                isOpened = true;
                if (animator != null && audioSource != null)
                {
                    animator.SetBool("Open", true);
                    audioSource.Play();
                }
            }
            else if(animationType == "close" && isOpened)
            {
                isOpened = false;
                if (animator != null && audioSource != null)
                {
                    animator.SetBool("Open", false);
                    audioSource.Play();
                }
            }
        }

        foreach(NavMeshObstacle obstacle in GetComponentsInChildren<NavMeshObstacle>())
        {
            obstacle.enabled = !isOpened;
        }
    }
}
