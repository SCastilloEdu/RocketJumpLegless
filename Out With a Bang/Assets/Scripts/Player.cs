using UnityEngine;

public class Player: MonoBehaviour {
    public float health = 10;
    public float drag = 50.0f;
    public GameObject bodySprite;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Prevent player from moving too fast
        if (rb.velocity.magnitude > drag)
        {
            rb.velocity = rb.velocity.normalized * drag;
        }

        if (rb.velocity.x < 1) {
            bodySprite.GetComponent<SpriteRenderer>().flipX=true;
        } else if (rb.velocity.x > 1) {
            bodySprite.GetComponent<SpriteRenderer>().flipX=false;
        }
    }
}
