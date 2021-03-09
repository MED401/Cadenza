using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract int id { get; }
    public abstract void OnTarget(int id);
    public abstract void OnRemoveTarget(int id);
    public abstract void OnInteract(int id);
}
