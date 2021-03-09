using System;
using UnityEngine;
public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> onTarget;
    public void TakeTarget(int id)
    {
        if (onTarget != null)
        {
            onTarget(id);
        }
    }

    public event Action<int> onRemoveTarget;
    public void RemoveTarget(int id)
    {
        if (onRemoveTarget != null)
        {
            onRemoveTarget(id);
        }
    }

    public event Action<int> onInteract;
    public void Interact(int id)
    {
        if (onInteract != null)
        {
            onInteract(id);
        }
    }
}
