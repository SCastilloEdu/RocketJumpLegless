using UnityEngine;

public class Arm: MonoBehaviour {
    public float firerate = 1.0f;
    private float cooldown;
    public GameObject bullet;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (cooldown>0) {
            cooldown-=1*Time.deltaTime;
        }

        // Shooting rocket
        if (Input.GetMouseButtonDown(0) && cooldown <= 0)
        {
            GameObject rocket = Instantiate(bullet, gameObject.transform.position, transform.rotation);
            CopyVelocity(rb, rocket.GetComponent<Rigidbody2D>());
            cooldown = firerate;
        }
    }

    void FixedUpdate() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y,difference.x)*Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(0f,0f,rotationZ-90);

    }

    void CopyVelocity(Rigidbody2D from,Rigidbody2D to) {
        Vector2 vFrom = from.velocity;
        Vector2 vTo = to.velocity;

        vTo.x=vFrom.x;
        vTo.y = vFrom.y;

        to.velocity=vTo;
        //https://discussions.unity.com/t/transfer-velocity-from-one-object-to-another/210795
    }

}
