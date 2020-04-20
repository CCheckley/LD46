using UnityEngine.Events;

public class Patient : Interactable
{
    public UnityEvent winGame;

    protected override void Interact()
    {
        if (GameManager.hasPower)
        {
            winGame?.Invoke();
        }
    }
}
