using UnityEngine;

public class Rocket: MonoBehaviour {
    public float lifetime = 2.0f; // How long before the rocket will expire.
    public float speed = 2.0f; // Rocket speed.
    public float explosionForce = 10f;
    public float explosionRadius = 2f;
    public float explosionMaxRampup = 0.4f; // Distance between player and explosion for full force
    public float velocityLenienceX = 2.0f; // Reset X velocity if below this speed
    public float velocityLenienceY = 2.0f; // Reset Y velocity if below this speed
    private Rigidbody2D rb;
    //private double birthTime; // Time rocket has spawned
    public ParticleSystem trail;
    public ParticleSystem flame;
    public ParticleSystem explosion;

    private void Start() {
        rb=gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject,lifetime);
        //birthTime=Time.fixedTimeAsDouble;
    }

    private void FixedUpdate() {
        rb.velocity=transform.up*speed;
        trail.Emit(1);
        
        /*// Delays the particles of the rocket
        if (Time.fixedTimeAsDouble - birthTime >=0.015) {
            ps.Play();
        }*/
    }


        private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Platform")||collision.gameObject.CompareTag("Enemy")) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,explosionRadius);
            foreach (Collider2D col in colliders) {
                Rigidbody2D rb_other = col.GetComponent<Rigidbody2D>();

                if (col.GetComponent<Player>()) {
                    col.GetComponent<Player>().SetFreefall();
                    col.GetComponent<Player>().splat = false;
                }

                // Debug
                

                if (rb_other!=null) {
                    // Calculate the direction from the explosion point to the object.
                    Vector2 dir = col.transform.position-transform.position;

                    // Get the distance between the two objects.
                    float distance = dir.magnitude; //  Vector2.Distance(this.transform.position,col.transform.position);

                    // If the distance is less than the rampup, apply full force.
                    if (distance/explosionRadius <=explosionMaxRampup) {
                        distance=0;
                    }

                    // Calculate force to the object based on the explosion force and direction, dampened by distance. Add on rampup to begin dampening past that point.
                    var result = (explosionForce*dir.normalized)*(1.0f-(distance/explosionRadius)+explosionMaxRampup);

                    // Check for imploding rocket.
                    if (distance<explosionRadius) {
                        // If the player has little momentum, set it to zero.
                        if (rb_other.velocity.x <=2.0f) {
                            rb_other.velocity = new Vector2 (0.0f,rb_other.velocity.y);
                        }

                        if (rb_other.velocity.y<=2.0f) {
                            rb_other.velocity=new Vector2(rb_other.velocity.x,0.0f);
                        }

                        // Apply.
                        rb_other.AddForce(result,ForceMode2D.Impulse);
                    }
                }
            }
            // Remove particles from parent so they don't immediately destroy themselves
            trail.Stop();
            trail.transform.parent=null;
            flame.Stop();
            flame.transform.parent=null;
            explosion.Emit(20);
            explosion.transform.parent=null;
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius*explosionMaxRampup);
        Gizmos.color=Color.yellow;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
