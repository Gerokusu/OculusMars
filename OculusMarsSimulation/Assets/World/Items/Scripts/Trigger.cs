public abstract class Trigger : Interactable
{
    public bool isActivated = false;

    public void LateUpdate()
    {
        if (isActivated)
        {
            isActivated = false;
            if(CanInteract())
            {
                OnActivation();
            }
        }
    }

    public abstract void OnActivation();
}
