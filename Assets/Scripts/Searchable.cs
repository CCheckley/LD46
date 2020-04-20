using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Searchable : Interactable
{
    SpriteRenderer spriteRenderer;
    public bool hasComponent = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Reset();
    }

    public void Reset()
    {
        hasComponent = false;
        spriteRenderer.color = new Color(0, 1, 0);
    }

    protected override void Interact()
    {
        if (player.hasComponent) { return; }

        if (hasComponent) { player.hasComponent = true; }

        hasComponent = false;
        spriteRenderer.color = new Color(0, 0, 1);
    }
}
