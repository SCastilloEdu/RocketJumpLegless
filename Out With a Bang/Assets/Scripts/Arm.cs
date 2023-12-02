using UnityEngine;

public class Arm: MonoBehaviour {
    public float firerate = 1.0f;
    private float cooldown;
    public GameObject bullet;
    public GameObject body;
    public GameObject bodySprite;
    private Rigidbody2D rb;
    private bool isSplat;

    private void Start() {
        rb=this.GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (cooldown>0) {
            cooldown-=1*Time.deltaTime;
        }
        isSplat=body.GetComponent<Player>().IsSplat();
        // Shooting rocket
        if (Input.GetMouseButton(0)&&cooldown<=0&&!isSplat) {
            GameObject rocket = Instantiate(bullet,gameObject.transform.position,transform.rotation);
            CopyVelocity(rb,rocket.GetComponent<Rigidbody2D>());
            cooldown=firerate;
        }
    }

    void FixedUpdate() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y,difference.x)*Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(0f,0f,rotationZ-90);
        if (!isSplat) {
            SpriteDirection();
        }
    }

    public bool SpriteDirection() {
        // Faces right if false, left if true
        bool doFlip = false;
        if (this.transform.rotation.eulerAngles.z<=180) {
            doFlip=true;
        }
        bodySprite.GetComponent<SpriteRenderer>().flipX=doFlip;
        this.GetComponent<SpriteRenderer>().flipX=doFlip;
        return doFlip;
    }

    void CopyVelocity(Rigidbody2D from,Rigidbody2D to) {
        Vector2 vFrom = from.velocity;
        Vector2 vTo = to.velocity;

        vTo.x=vFrom.x;
        vTo.y=vFrom.y;

        to.velocity=vTo;
        //https://discussions.unity.com/t/transfer-velocity-from-one-object-to-another/210795
    }

}
