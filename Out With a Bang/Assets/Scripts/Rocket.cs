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

                    // Get the distance between the two objects.
                    float distance =  Vector2.Distance(this.transform.position,col.transform.position);

                    // Force to the object based on the direction and explosion force, dampened by distance.
                    var result = (explosionForce*dir.normalized)*(1.0f-(distance/explosionRadius));

                    // Check for imploding rocket.
                    if (distance<explosionRadius) {
                        // Apply.
                        rb_other.AddForce(result,ForceMode2D.Impulse);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}
