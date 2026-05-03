using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform player;
    public float speed = 2f;

    private bool facingRight = true;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        float direction = player.position.x - transform.position.x;

        Vector2 target = new Vector2(player.position.x, transform.position.y);

        transform.position = Vector2.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        // 🔥 FLIP DOANG (tanpa animasi)
        if (direction > 0 && !facingRight)
            Flip();
        else if (direction < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}