using UnityEngine;
using System.Collections;

public class Player: MonoBehaviour {
    public float health = 10;
    public float drag = 50.0f;
    public double splatTime = 3;
    public GameObject bodySprite;
    public GameObject arm;
    private Rigidbody2D rb;
    private double freefall = 0;
    public bool splat;
    private bool isSplatted = false;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }


    private void Update() {
        // Tilts the player during a splat
        if (isSplatted) {
            //bodySprite.GetComponent<Rigidbody2D>().MovePosition(new Vector2(0,-0.5f));
            //bodySprite.GetComponent<Rigidbody2D>().MoveRotation(90);

        }
    }
    private void FixedUpdate()
    {
        // Prevent player from moving too fast
        if (rb.velocity.magnitude > drag)
        {
            rb.velocity = rb.velocity.normalized * drag;
        }

        // Checks if the player should splat
        if (Time.fixedTimeAsDouble - freefall >=splatTime) {
            splat=true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (splat) {
            isSplatted=true;
            bodySprite.GetComponent<RelativeJoint2D>().correctionScale=0.6f;
            int dir = 1;
            if (arm.GetComponent<Arm>().SpriteDirection()) { // So the player always lands on their face
                dir*=-1;
            }
            bodySprite.GetComponent<RelativeJoint2D>().angularOffset=90*dir;
            bodySprite.GetComponent<RelativeJoint2D>().linearOffset=new Vector2(-0.6f*dir,0);
            arm.GetComponent<SpriteRenderer>().enabled=false;
            StartCoroutine(DoSplat());
            splat=false;
        }
        SetFreefall();
    }

    private void OnCollisionStay2D(Collision2D collision) {
        SetFreefall();
    }

    public void SetFreefall() {
        freefall=Time.fixedTimeAsDouble;
    }

    public bool IsSplat() {
        return isSplatted;
    }

    IEnumerator DoSplat() {
        yield return new WaitForSeconds(2.0f);
        bodySprite.GetComponent<RelativeJoint2D>().correctionScale=0.3f;
        bodySprite.GetComponent<RelativeJoint2D>().angularOffset=0;
        bodySprite.GetComponent<RelativeJoint2D>().linearOffset=new Vector2(0,0);
        yield return new WaitForSeconds(0.3f);
        arm.GetComponent<SpriteRenderer>().enabled=true;
        bodySprite.GetComponent<RelativeJoint2D>().correctionScale=0.6f;
        isSplatted=false;
    }
}
