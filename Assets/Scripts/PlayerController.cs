using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;

    [SerializeField] FieldOfView fov;

    public float movementSpeed = 1000.0f;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.isKinematic = false;
        rigidbody2D.angularDrag = 0.0f;
        rigidbody2D.gravityScale = 0.0f;
    }

    void Update()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Move(targetVelocity);

        Vector3 targetDirection = GetTargetDirection();
        Rotate(targetDirection);

        fov.SetAimDirection(targetDirection);
        fov.SetOrigin(transform.position);
    }

    void Move(Vector2 targetVelocity)
    {
        rigidbody2D.velocity = (targetVelocity * movementSpeed) * Time.deltaTime;
    }

    Vector3 GetTargetDirection()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    void Rotate(Vector2 targetDirection)
    {
        rigidbody2D.rotation = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
    }
}