using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;

    private Rigidbody2D rb;
    private Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointB;
    }

    void FixedUpdate()
    {
        float direction = Mathf.Sign(target.position.x - transform.position.x);

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        // FLIP OTOMATIS BERDASARKAN ARAH
        if (direction > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // ganti target kalau sudah sampai
        if (Mathf.Abs(transform.position.x - target.position.x) < 0.1f)
        {
            target = target == pointA ? pointB : pointA;
        }
    }
}