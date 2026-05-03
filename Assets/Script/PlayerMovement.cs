using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;

    private float move = 0f;
    private bool facingRight = true;

    [Header("Jump")]
    public int maxJump = 2;
    private int jumpCount = 0;

    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // =====================
    // INPUT GERAK
    // =====================
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        move = input.x;

        // Paksa 0 kalau input kecil
        if (Mathf.Abs(move) < 0.1f)
            move = 0f;

        // Flip karakter
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    // =====================
    // INPUT LOMPAT
    // =====================
    public void OnJump(InputValue value)
    {
        if (!value.isPressed) return;

        if (jumpCount < maxJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;

            anim.SetBool("isJumping", true);

            if (jumpCount == 2)
                anim.SetTrigger("DoubleJump");
        }
    }

    // =====================
    // UPDATE (ANIMASI & LOGIC)
    // =====================
    void Update()
    {
        // Speed animator (0 atau 1 biar stabil)
        float speedValue = Mathf.Abs(move);
        speedValue = speedValue < 0.1f ? 0f : 1f;

        anim.SetFloat("Speed", speedValue);

        // Ground check sederhana
        isGrounded = Mathf.Abs(rb.velocity.y) < 0.01f;
        anim.SetBool("isGrounded", isGrounded);

        // Reset jump saat menyentuh tanah
        if (isGrounded)
        {
            jumpCount = 0;
            anim.SetBool("isJumping", false);
        }
    }

    // =====================
    // PHYSICS
    // =====================
    void FixedUpdate()
    {
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }

    // =====================
    // FLIP KARAKTER
    // =====================
    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }
}