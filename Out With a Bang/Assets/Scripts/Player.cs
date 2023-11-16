using UnityEngine;

public class Player: MonoBehaviour {
    public float health = 10;
    public float drag = 50.0f;
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
    }
}
