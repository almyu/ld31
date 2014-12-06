using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoSingleton<PlayerController> {

    public float speed = 5f;
    public float jumpForce = 10f;

    [System.Serializable]
    public class VelocityChangeEvent : UnityEvent<string, float> {}
    public VelocityChangeEvent onVelocityChange;

    public bool isLanded = true;

    private void FixedUpdate() {
        var axis = Input.GetAxis("Horizontal");
        var vel = Mathf.Approximately(axis, 0f) ? 0f : axis * speed; // axis < 0f ? -speed : speed;

        rigidbody2D.velocity = rigidbody2D.velocity.WithX(vel);
        onVelocityChange.Invoke("Velocity", rigidbody2D.velocity.x);

        if (vel != 0f) {
            var facingRight = Quaternion.Angle(transform.rotation, Quaternion.identity) < 15f;
            if (facingRight != vel > 0f)
                transform.rotation = Quaternion.AngleAxis(facingRight ? 180f : 0f, Vector3.up);
        }

        if (Input.GetButtonDown("Jump")) Jump();

        isLanded = false;
    }

    private void OnCollisionStay2D(Collision2D coll) {
        if (coll.contacts[0].normal.y > 0.5f) isLanded = true;
    }

    public void Jump() {
        if (!isLanded) return;

        rigidbody2D.velocity = rigidbody2D.velocity.WithY(jumpForce);
        isLanded = false;
    }
}
