using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float jumpForce = 12f;
    public float chaseRange = 10f;

    [Header("Checks")]
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform edgeCheck;

    [Header("Layer")]
    public LayerMask groundLayer;

    [Header("Check Settings")]
    public float groundRadius = 0.2f;
    public float wallDistance = 0.8f;
    public float edgeDistance = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // supaya enemy tidak muter
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (player == null)
            return;

        // =========================
        // CEK TANAH
        // =========================
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );

        // =========================
        // CEK RANGE PLAYER
        // =========================
        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distance <= chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            StopEnemy();
        }
    }

    // =========================
    // KEJAR PLAYER
    // =========================
    void ChasePlayer()
    {
        float direction;

        // PLAYER DI KANAN
        if (player.position.x > transform.position.x)
        {
            direction = 1f;

            // flip kanan
            sr.flipX = false;
        }
        // PLAYER DI KIRI
        else
        {
            direction = -1f;

            // flip kiri
            sr.flipX = true;
        }

        // =========================
        // GERAK
        // =========================
        rb.velocity = new Vector2(
            direction * moveSpeed,
            rb.velocity.y
        );

        // =========================
        // CEK JURANG
        // =========================
        bool hasGroundAhead = Physics2D.Raycast(
            edgeCheck.position,
            Vector2.down,
            edgeDistance,
            groundLayer
        );

        // kalau depan jurang → lompat
        if (!hasGroundAhead && isGrounded)
        {
            rb.velocity = new Vector2(
                direction * moveSpeed,
                jumpForce
            );
        }

        // =========================
        // CEK TEMBOK
        // =========================
        RaycastHit2D wallHit = Physics2D.Raycast(
            wallCheck.position,
            Vector2.right * direction,
            wallDistance,
            groundLayer
        );

        // kalau ada tembok → lompat
        if (wallHit.collider != null && isGrounded)
        {
            rb.velocity = new Vector2(
                direction * moveSpeed * 1.5f,
                jumpForce
            );
        }
    }

    // =========================
    // BERHENTI
    // =========================
    void StopEnemy()
    {
        rb.velocity = new Vector2(
            0,
            rb.velocity.y
        );
    }

    // =========================
    // GIZMOS
    // =========================
    void OnDrawGizmosSelected()
    {
        // RANGE PLAYER
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            chaseRange
        );

        // GROUND CHECK
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(
                groundCheck.position,
                groundRadius
            );
        }

        // WALL CHECK
        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawLine(
                wallCheck.position,
                wallCheck.position +
                Vector3.right * wallDistance
            );
        }

        // EDGE CHECK
        if (edgeCheck != null)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(
                edgeCheck.position,
                edgeCheck.position +
                Vector3.down * edgeDistance
            );
        }
    }
}