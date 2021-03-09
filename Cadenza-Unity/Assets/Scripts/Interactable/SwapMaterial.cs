using UnityEngine;

public class SwapMaterial : Interactable
{
    [SerializeField] private Material onTargetMaterial;
    [SerializeField] private Material onInteractMaterial;

    [SerializeField] private int _id;
    private new Renderer renderer;
    private Material baseMaterial;

    private Material currentMaterial;

    public override int id { get => _id; }

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        baseMaterial = renderer.material;
        currentMaterial = renderer.material;

        GameEvents.current.onInteract += OnInteract;
        GameEvents.current.onTarget += OnTarget;
        GameEvents.current.onRemoveTarget += OnRemoveTarget;
    }

    public override void OnInteract(int id)
    {
        if (id == this.id)
        {
            Debug.Log("Activated Target" + this.id);
            if (currentMaterial != onInteractMaterial)
            {
                currentMaterial = onInteractMaterial;
            }
            else
            {
                currentMaterial = baseMaterial;
            }

            renderer.material = currentMaterial;
        }
    }

    public override void OnTarget(int id)
    {
    }
    public override void OnRemoveTarget(int id)
    {
        if (id == this.id)
        {
            renderer.material = currentMaterial;
        }
    }
}
