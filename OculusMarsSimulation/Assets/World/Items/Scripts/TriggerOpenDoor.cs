using UnityEngine;

public class TriggerOpenDoor : Trigger
{
    public SettlementDoor door;

    public void Start()
    {
        animatables.Add(door);
    }

    public override void OnActivation()
    {
        if (door != null)
        {
            if (!door.isOpened)
            {
                door.Animate("open");
            }
            else
            {
                door.Animate("close");
            }
        }
    }
}
