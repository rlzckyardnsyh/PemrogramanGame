using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private float move;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // dipanggil dari Player Input (Move)
    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>().x;

        // flip tetap jalan
        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();
    }

    // dipanggil dari Player Input (Jump)
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}