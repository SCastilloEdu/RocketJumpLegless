using UnityEngine;

public class Rocket: MonoBehaviour {
    public float lifetime = 2.0f; //How long before the rocket will expire.
    public float speed = 2.0f; //Rocket speed.
    public float explosionForce = 10f;
    public float explosionRadius = 2f;
    private Rigidbody2D rb;

    private void Start() {
        rb=gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject,lifetime);
    }

    private void FixedUpdate() {
        rb.velocity=transform.up*speed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Platform")||collision.gameObject.CompareTag("Enemy")) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,explosionRadius);
            foreach (Collider2D col in colliders) {
                Rigidbody2D rb_other = col.GetComponent<Rigidbody2D>();

                if (rb_other!=null) {
                    // Calculate the direction from the explosion point to the object.
                    Vector2 dir = col.transform.position-transform.position;

                    // Apply a force to the object based on the direction and explosion force.
                    rb_other.AddForce(dir.normalized*explosionForce,ForceMode2D.Impulse);
                }
            }
            Destroy(gameObject);
        }
    }
}
