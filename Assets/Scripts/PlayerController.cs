using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;

    [SerializeField] float sprintTime = 2.0f;
    [SerializeField] float sprintMultiplier = 1.2f;
    [SerializeField] float movementSpeed = 100.0f;
    [SerializeField] float jumpForce = 500.0f;
    [SerializeField] LayerMask floorLayer;

    public static List<HouseHoldItem> items;

    float sprintUsed = 0.0f;

    bool isFacingRight;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigidbody2D.isKinematic = false;
        rigidbody2D.angularDrag = 0.0f;

        isFacingRight = (transform.localScale.x > 0);
    }

    void Update()
    {
        Move(Input.GetAxisRaw("Horizontal"), Input.GetButtonDown("Jump"), Input.GetButton("Sprint"));
    }

    public void Move(float xInput, bool isJumping, bool isSprinting)
    {
        float xDelta = (xInput * movementSpeed) * Time.deltaTime;
        float yDelta = (isJumping && HasLanded()) ? jumpForce * Time.deltaTime : rigidbody2D.velocity.y;

        if (isSprinting)
        {
            xDelta *= sprintMultiplier;
            sprintUsed += Time.deltaTime;
        }
        else { sprintUsed -= Time.deltaTime; }

        Mathf.Clamp(sprintUsed, 0.0f, sprintTime);

        rigidbody2D.velocity = new Vector2(xDelta, yDelta);

        if ((xDelta > 0 && !isFacingRight) || (xDelta < 0 && isFacingRight)) { FlipHorizontal(); }
    }

    public void FlipHorizontal()
    {
        Vector3 targetScale = transform.localScale;
        targetScale.x = -transform.localScale.x;
        transform.localScale = targetScale;

        isFacingRight = !isFacingRight;
    }

    public bool HasLanded()
    {
        return rigidbody2D.IsTouchingLayers(floorLayer);
    }
}