using UnityEngine;

public class Arm: MonoBehaviour {
    public int firerate = 1;
    private float cooldown;
    public GameObject bullet;

    void Update() {
        if (cooldown>0) {
            cooldown-=1*Time.deltaTime;
        }
    }

    void FixedUpdate() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y,difference.x)*Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(0f,0f,rotationZ-90);

        if (Input.GetMouseButton(0)&&cooldown<=0) {
            GameObject rocket = Instantiate(bullet,gameObject.transform.position,transform.rotation);
            CopyVelocity(this.GetComponent<Rigidbody2D>(),rocket.GetComponent<Rigidbody2D>());
            cooldown=firerate;
        }
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
