using System.Collections;
using UnityEngine;

public class Landmine: MonoBehaviour {
    public float lifetime = 2.0f; //How long before the landmine will detonate.
    public float respawn = 5.0f;
    public float explosionForce = 50f;
    public bool active = true;
    public GameObject sprite;
    public ParticleSystem explosion;
    private float explosionRadius;

    private void Start() {
        explosionRadius=GetComponent<CircleCollider2D>().radius;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")&&active) {
            active=false;
            // Wait...
            StartCoroutine(Wait(lifetime));
            // KABOOM
            sprite.GetComponent<SpriteRenderer>().enabled=false;


            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,explosionRadius);
            foreach (Collider2D col in colliders) {
                Rigidbody2D rb_other = col.GetComponent<Rigidbody2D>();

                if (col.GetComponent<Player>()) {
                    col.GetComponent<Player>().SetFreefall();
                    col.GetComponent<Player>().splat=false;
                }

                float explosionMaxRampup = explosionRadius/2;


                if (rb_other!=null) {
                    // Calculate the direction from the explosion point to the object.
                    Vector2 dir = col.transform.position-transform.position;

                    // Get the distance between the two objects.
                    float distance = dir.magnitude; //  Vector2.Distance(this.transform.position,col.transform.position);

                    // If the distance is less than the rampup, apply full force.
                    if (distance/explosionRadius<=explosionMaxRampup) {
                        distance=0;
                    }

                    // Calculate force to the object based on the explosion force and direction, dampened by distance. Add on rampup to begin dampening past that point.
                    var result = (explosionForce*dir.normalized)*(1.0f-(distance/explosionRadius)+explosionMaxRampup);

                    // Check for imploding rocket.
                    if (distance<explosionRadius) {
                        // If the player has little momentum, set it to zero.
                        if (rb_other.velocity.x<=2.0f) {
                            rb_other.velocity=new Vector2(0.0f,rb_other.velocity.y);
                        }

                        if (rb_other.velocity.y<=2.0f) {
                            rb_other.velocity=new Vector2(rb_other.velocity.x,0.0f);
                        }

                        // Apply.
                        rb_other.AddForce(result,ForceMode2D.Impulse);
                        explosion.Emit(20);
                    }
                }
            }


            // Respawn
            StartCoroutine(Wait(respawn));
            sprite.GetComponent<SpriteRenderer>().enabled=true;
            active=true;
        }
    }
    IEnumerator Wait(float time) {
        yield return new WaitForSeconds(time);
    }

}
