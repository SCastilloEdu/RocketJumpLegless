using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float distance = 5.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(distance > Vector3.Distance(transform.position, player.transform.position))
        {
            Debug.Log("Close!");
            //If the player is close

            if (Physics2D.Raycast(transform.position, player.transform.position).collider.gameObject.CompareTag("Player") == true)
                //Check if the enemy has line of sight
            {
                Debug.DrawLine(transform.position, player.transform.position, Color.red);
                Debug.Log("Seen!");
            }
        } else
        {
            //If the player is far
            Debug.Log("Far!");
        }
    }
}
