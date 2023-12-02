using UnityEngine;
using System.Collections;

public class Landmine : MonoBehaviour
{
    public float lifetime = 2.0f; //How long before the landmine will detonate.
    public float respawn = 5.0f;
    public bool active = true;
    public GameObject sprite;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active){
            active=false;
            // Wait...
            StartCoroutine(Wait(lifetime));
            // KABOOM
            sprite.GetComponent<SpriteRenderer>().enabled=false;
            // Respawn
            StartCoroutine(Wait(respawn));
            sprite.GetComponent<SpriteRenderer>().enabled=false;
            active=true;
        }
    }
    IEnumerator Wait(float time) {
        yield return new WaitForSeconds(time);
    }

}
