using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Interactable : MonoBehaviour
{
    protected new Rigidbody2D rigidbody2D;
    protected PlayerController player;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player = null;
        }
    }

    protected abstract void Interact();
}
