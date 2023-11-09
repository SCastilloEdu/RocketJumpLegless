using UnityEngine;

public class Arm: MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition=Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x=transform.position.x,
            mousePosition.y=transform.position.y
        );

        transform.up=direction;
    }
}
