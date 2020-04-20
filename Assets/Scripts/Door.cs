using UnityEngine;

public class Door : Interactable
{
    bool isOpen = false;

    [SerializeField] Transform pivot;

    public void Reset()
    {
        if (isOpen) { transform.RotateAround(pivot.position, Vector3.forward, 90); }
        isOpen = false;
    }

    protected override void Interact()
    {
        float angle = (isOpen) ? 90 : -90;
        transform.RotateAround(pivot.position, Vector3.forward, angle);
        isOpen = !isOpen;
    }
}
