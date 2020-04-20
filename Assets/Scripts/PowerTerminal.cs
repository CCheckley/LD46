using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class PowerTerminal : Interactable
{
    public UnityEvent powerOn;

    public static int requiredComponentCount = 4;
    int componentCount = 0;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Reset();
    }

    public void Reset()
    {
        componentCount = 0;
        spriteRenderer.color = new Color(1, 1, 0);
    }

    protected override void Interact()
    {
        if (player.hasComponent)
        {
            componentCount++;
            player.hasComponent = false;
        }

        if (componentCount == requiredComponentCount)
        {
            powerOn?.Invoke();
            spriteRenderer.color = new Color(1, 1, 1);
        }
    }
}
