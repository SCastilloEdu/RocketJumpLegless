using UnityEngine;

public class Player: MonoBehaviour {
    public float fireRate = 1.0f;
    public float health = 10;
    Vector2 mousePos;
    public GameObject bullet;
    public Camera camera;

    private void Update() {
        mousePos=camera.ScreenToWorldPoint(Input.mousePosition);
    }
    void FixedUpdate() {
        if (Input.GetMouseButton(0)) {
            Instantiate(bullet,gameObject.transform.position,transform.rotation);

        }
    }
}
//https://youtu.be/BupOVbzijJM
