using UnityEngine;

public class Item : Interactable
{
    public uint mass;

    public Vector3 forceThrow;

    public void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if(rigidbody != null)
        {
            rigidbody.AddForce(forceThrow, ForceMode.Impulse);
            if (forceThrow != new Vector3(0, 0, 0))
            {
                forceThrow /= 2.0F;
            }
        }
    }
}
