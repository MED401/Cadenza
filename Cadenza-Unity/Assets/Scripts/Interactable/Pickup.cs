using UnityEngine;

public class Pickup : Interactable
{
    [SerializeField] private int _id;
    public override int id => id;

    public override void OnInteract(int id)
    {
        throw new System.NotImplementedException();
    }

    public override void OnRemoveTarget(int id)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTarget(int id)
    {
        throw new System.NotImplementedException();
    }
}
