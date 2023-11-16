using UnityEngine;

public class Landmine : MonoBehaviour
{
    public float lifetime = 2.0f; //How long before the landmine will detonate.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            // Wait...

            // KABOOM
            Destroy(gameObject);
        }
    }
}
