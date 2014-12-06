using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoSingleton<PlayerController> {

    public float speed = 5f;
    public float jumpForce = 10f;

    public bool isLanded = true;

    private void FixedUpdate() {
        isLanded = false;
    }

    private void OnCollisionStay2D(Collision2D coll) {
        if (coll.contacts[0].normal.y > 0.5f) isLanded = true;
    }

    public void Jump() {
        if (!isLanded) return;

        rigidbody2D.velocity = Vector2.up * jumpForce;
        isLanded = false;
    }

    private void Update() {
        rigidbody2D.velocity = Vector2.right * Input.GetAxis("Horizontal") * speed;

        if (Input.GetButtonDown("Jump")) Jump();
    }
}
